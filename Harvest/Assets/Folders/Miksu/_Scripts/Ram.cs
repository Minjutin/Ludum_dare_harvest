using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Ram : MonoBehaviour
{
    #region Properties

    // References
    TileManager tileManager;
    Rigidbody rb;
    WaterScript water;


    [Header("Speeds")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float aggroSpeed = 10f;
    float currentSpeed;

    [Header("Moving")]
    Vector3 moveDirection = Vector3.forward;
    float moveTime = 1f;
    [SerializeField] float changeDirectionFrequency = 2f;
    [Space]
    [Tooltip("How long the Ram moves after turning")]
    [SerializeField] float maxMoveTime = 2f;
    [SerializeField] float minMoveTime = 0.5f;
    [Header("Distance from lake")]
    [SerializeField] float maxDistanceFromLake = 3f;
    [SerializeField] float minDistanceFromLake = 1f;

    [Header("Pooping")]
    [SerializeField] float timeBetweenPoops = 5f;

    [Header("Aggroing")]
    [SerializeField] float aggroRadius = 3f;
    [SerializeField] SphereCollider aggroTrigger;
    [Space]
    [SerializeField] float pursueTime = 5f;
    bool aggroed = false;
    GameObject aggroTarget;

    [Header("RAMMING")]
    [SerializeField] float knockBackPower = 5f;


    #endregion

    private void Start()
    {
        tileManager = FindObjectOfType<TileManager>();
        water = FindObjectOfType<WaterScript>();
        rb = GetComponent<Rigidbody>();

        aggroTrigger.radius = aggroRadius;

        StartManureTimer();
        StartMoving();
    }


    #region Pooping
    private void TryToPoop()
    {
        // Check if Ram is on fertile tile

        TileDaddy tile = tileManager.GetTileCreatureIsOn(transform.position);

        if (tile is FertileTile)
        {
            FertileTile fTile = tile as FertileTile;
            if (!fTile.HasItem())
            {
                // If no item, POOP
                Manure manure = new Manure();
                fTile.SetItem(manure);
                GameObject manureGO = Instantiate(Resources.Load("Manure"), tile.position, Quaternion.identity) as GameObject;
                fTile.SpawnManure(manureGO);
            }
        }
    }


    public void StartManureTimer()
    {
        StartCoroutine(ManureTimer());
    }
    IEnumerator ManureTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenPoops);

            TryToPoop();
        }
    }
    #endregion

    #region Moving
    private void StartMoving()
    {
        // Initialize currentSpeed
        currentSpeed = walkSpeed;

        // Start Movement Coroutines
        StartCoroutine(TurningCoroutine());
        StartCoroutine(MovingCoroutine());
    }

    #region Turning
    IEnumerator TurningCoroutine()
    {

        while (true)
        {

            if (!aggroed)
            {

                // Pick a direction

                moveDirection = ChangeDirection();
                // Although check distance to lake too
                CheckDistanceToLake();

                // Add some movetime
                moveTime = Random.Range(minMoveTime, maxMoveTime);

                yield return new WaitForSeconds(changeDirectionFrequency);
            }
            else
            {
                // AGGROED

                // -> Keep turning towards Player
                if (aggroTarget != null)
                { moveDirection = ChangeDirectionTowards(aggroTarget.transform.position); }

                // Movetime
                moveTime = Random.Range(minMoveTime, maxMoveTime);

                yield return new WaitForSeconds(changeDirectionFrequency / 2f); // Divided by two
            }

        }
    }

    private Vector3 ChangeDirection()
    {
        transform.RotateAround(transform.position, Vector3.up, Random.Range(-180, 180));
        Quaternion rotation = transform.rotation;
        // Reset GO rotation
        transform.rotation = Quaternion.identity;

        Vector3 direction = rotation * Vector3.forward;
        direction.Normalize();

        return direction;
    }

    private void CheckDistanceToLake()
    {
        Vector3 difference = (water.gameObject.transform.position - transform.position);
        //Debug.Log("Difference: " + difference);
        float distance = difference.magnitude;
        //Debug.Log("Distance: " + distance);

        // If too much -> Turn TOWARDS lake
        if (distance >= maxDistanceFromLake)
        {
            //Debug.Log("Ram TOO FAR from lake");
            Debug.DrawLine(transform.position, water.gameObject.transform.position, Color.red, 2f);
            moveDirection = difference.normalized;
        }

        // If too close -> Turn AWAY from lake
        else if (distance <= minDistanceFromLake)
        {
            //Debug.Log("Ram TOO CLOSE from lake");
            Debug.DrawLine(transform.position, water.gameObject.transform.position, Color.blue, 2f);
            moveDirection = -difference.normalized;
        }
    }

    private Vector3 ChangeDirectionTowards(Vector3 pos)
    {
        Vector3 dir = pos - transform.position;
        dir.Normalize();
        return dir;
    }

    #endregion

    #region MOVING
    IEnumerator MovingCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(0.5f, 2f));

        while (true)
        {
            if (moveTime > 0)
            {
                // Get next pos
                Vector3 nextPos = transform.position + moveDirection * currentSpeed * Time.deltaTime;

                // Move Forward
                rb.MovePosition(nextPos);

                // Reduce Movetime
                moveTime -= Time.deltaTime;
            }

            yield return new WaitForSeconds(Time.deltaTime);

        }
    }


    #endregion

    #endregion

    #region AGGROING

    // Aggro Trigger
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //Debug.Log("Colliding with Player!");
            Debug.DrawLine(transform.position, other.gameObject.transform.position, Color.magenta, 0.2f);

            BeginAggro(other.gameObject);
        }
    }

    private void BeginAggro(GameObject target)
    {
        // Set target
        aggroTarget = target;

        // Activate Aggro state
        aggroed = true;

        // Set aggro Speed
        currentSpeed = aggroSpeed;

        // Turn Instantly
        moveTime = 3f;
        moveDirection = ChangeDirectionTowards(target.transform.position);

        // Start Aggro timer
        StartCoroutine(AggroTimer());

        //Debug.Log("Aggro begins!");
    }

    IEnumerator AggroTimer()
    {
        float currentAggroTimeLeft = pursueTime;

        while (aggroed)
        {
            // Check if the aggrotime has time left
            if (currentAggroTimeLeft > 0)
            {
                // Reduce time
                currentAggroTimeLeft -= Time.deltaTime;

                // Show AggroLine
                Debug.DrawLine(transform.position, aggroTarget.transform.position, Color.red, 0.2f);
            }
            else
            {
                // Aggro has ended..?
                CheckIfAggroHasEnded();
                break;
            }

            // Wait
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private void CheckIfAggroHasEnded()
    {
        // If a Player is within radius, reignite aggro
        Collider[] players = Physics.OverlapSphere(transform.position, aggroRadius);
        GameObject candidate = null;
        foreach (Collider player in players)
        {
            if (player.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                // Check if it was the current Target
                if (player == aggroTarget)
                {
                    // Remain aggroed, Keep chasing the SAME Player
                    BeginAggro(player.gameObject);
                    candidate = null;
                    break;
                }
                // Else choose them
                candidate = player.gameObject;
            }
        }

        if (candidate != null)
        {
            // Remain Aggroed
            BeginAggro(candidate);
        }
        else
        {
            EndAggro();
        }
    }

    private void EndAggro()
    {
        // Set as false
        aggroed = false;

        // Reset to walkSpeed
        currentSpeed = walkSpeed;

        //Debug.Log("Aggro has ended..");

    }
    #endregion
}
