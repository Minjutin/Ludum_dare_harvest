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
    [Tooltip("How often the Ram changes directions.")]
    [SerializeField] float changeDirectionFrequency = 2f;
    [Space]
    [Tooltip("How long the Ram moves after turning. Randomized between the Min and Max values.")]
    [SerializeField] float maxMoveTime = 2f;
    [Tooltip("How long the Ram moves after turning. Randomized between the Min and Max values.")]
    [SerializeField] float minMoveTime = 0.5f;
    [Tooltip("Distance from lake. The Ram turns back if too far from the Lake")]
    [SerializeField] float maxDistanceFromLake = 3f;
    [Tooltip("Distance from lake. The Ram turns back if too close to the Lake")]
    [SerializeField] float minDistanceFromLake = 1f;

    [Header("Pooping")]
    [Tooltip("How often the Ram tries to 'fertilize' the ground. Requires empty fertile tile.")]
    [SerializeField] float timeBetweenPoops = 5f;

    [Header("Aggroing")]
    [Tooltip("Distance that triggers the aggro. Will stay aggroed while anybody is inside.")]
    [SerializeField] float aggroRadius = 3f;
    [SerializeField] SphereCollider aggroTrigger;
    [Space]
    [Tooltip("After leaving the AggroRadius, how long the Ram will pursue Player.")]
    [SerializeField] float pursueTime = 5f;
    bool aggroed = false;
    GameObject aggroTarget;

    /*[SerializeField]*/ float rammingDistance = 1f; // Unused. Collider checks this.
    [Header("RAMMING")]
    [SerializeField] float stunTime = 1f;
    [Tooltip("How hard the Ram pushes the Player")]
    [SerializeField] float knockBackPower = 5f;
    [Tooltip("The Ram enters a aggro-cooldown after ramming. How long it takes for the Ram to be able to aggro again.")]
    [SerializeField] float cooldownTimeAfterRamming = 3f;
    bool readyToRam = true;

    Animator animator;

    #endregion

    private void Start()
    {
        tileManager = FindObjectOfType<TileManager>();
        water = FindObjectOfType<WaterScript>();
        rb = GetComponent<Rigidbody>();

        animator = transform.GetChild(0).GetChild(0).gameObject.GetComponent<Animator>();

        timeBetweenPoops = FindObjectOfType<Constants>().poopTime;

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
                yield return new WaitForSeconds(changeDirectionFrequency);


                // Pick a direction
                moveDirection = ChangeDirection();
                // Although check distance to lake too
                CheckDistanceToLake();

                // Add some movetime
                moveTime = Random.Range(minMoveTime, maxMoveTime);

            }
            else
            {
                // AGGROED

                // -> Keep turning towards Player
                if (aggroTarget != null)
                { moveDirection = ChangeDirectionTowards(aggroTarget.transform.position); }

                // Movetime
                moveTime = Random.Range(minMoveTime, maxMoveTime);

                yield return new WaitForSeconds(0.3f);
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

            // Alter difference a little bit
            difference = new Vector3(difference.x + Random.Range(-0.4f, 0.4f),
                                                    difference.y,
                                                    difference.z + Random.Range(-0.4f, 0.4f));
            moveDirection = difference.normalized;
        }

        // If too close -> Turn AWAY from lake
        else if (distance <= minDistanceFromLake)
        {
            //Debug.Log("Ram TOO CLOSE from lake");
            Debug.DrawLine(transform.position, water.gameObject.transform.position, Color.blue, 2f);
            // Alter difference a little bit
            difference = new Vector3(difference.x + Random.Range(-0.4f, 0.4f),
                                                    difference.y,
                                                    difference.z + Random.Range(-0.4f, 0.4f));
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
                animator.SetBool("Moving", true);

                // Get next pos
                Vector3 nextPos = transform.position + moveDirection * currentSpeed * Time.fixedDeltaTime;

                // Move Forward
                rb.MovePosition(nextPos);

                // Reduce Movetime
                moveTime -= Time.fixedDeltaTime;
            }
            else
            {
                animator.SetBool("Moving", false);
            }

            yield return new WaitForSeconds(Time.fixedDeltaTime);

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
        // If not ready yet, cancel
        if (!readyToRam) { return; }

        animator.SetBool("Angry", true);

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
                // Check if close enough to Target
                //CheckIfTargetWithinRammingDistance(); // Handled by collision?

                // Reduce time
                currentAggroTimeLeft -= Time.deltaTime;

                // Show AggroLine
                float distance = (transform.position - aggroTarget.transform.position).magnitude;
                if (distance <= aggroRadius) { Debug.DrawLine(transform.position, aggroTarget.transform.position, Color.red, 0.2f); }
                else { Debug.DrawLine(transform.position, aggroTarget.transform.position, Color.yellow, 0.2f); }
                
            }
            else
            {
                // Aggro has ended..?
                CheckNeedToAggroAgain();
                break;
            }

            // Wait
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private void CheckNeedToAggroAgain()
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
        animator.SetBool("Moving", false);
        animator.SetBool("Angry", false);
 
        // Set as false
        aggroed = false;

        // Reset to walkSpeed
        currentSpeed = walkSpeed;

        //Debug.Log("Aggro has ended..");
        Debug.DrawLine(transform.position, aggroTarget.transform.position, Color.white, 1f);

    }
    #endregion

    #region RAMMING
    private void OnCollisionEnter(Collision collision)
    {
        // Check if Player
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Stun them!
            //collision.gameObject.GetComponent<PlayerMovement>().GetRammed(GetRamDirection(), knockBackPower);

            // Move the Ram
            //MoveOnRam(GetRamDirection());

            RamTheTarget();
        }
    }

    private void CheckIfTargetWithinRammingDistance()
    {
        float distance = (transform.position - aggroTarget.gameObject.transform.position).magnitude;

        // Check if distance is within ramming distance
        if (distance <= rammingDistance)
        {
            // Ram the Player
            RamTheTarget();
        }
    }

    private void RamTheTarget()
    {
        Vector3 ramDirection = GetRamDirection();

        // Slam the player
        aggroTarget.GetComponent<PlayerMovement>().GetRammed(ramDirection, knockBackPower);

        // Stun the Player too
        aggroTarget.GetComponent<PlayerStun>().GetStunned(stunTime, ramDirection);

        // Move the Ram
        //MoveOnRam(ramDirection);

        // Enter cooldown
        StartCoroutine(AfterRammingCooldownTimer());
    }

    private Vector3 GetRamDirection()
    {
        // Get the direction
        Vector3 ramDirection = (aggroTarget.gameObject.transform.position - transform.position).normalized;

        return ramDirection;
    }

    private void MoveOnRam(Vector3 ramDirection)
    {
        float distance = (transform.position - aggroTarget.gameObject.transform.position).magnitude;
        Vector3 nextPos = transform.position + -ramDirection * distance * 0.5f;
        Debug.Log("ramDirection: " + -ramDirection);

        // Move the Ram
        rb.MovePosition(nextPos);
        //rb.velocity = nextPos;
    }

    IEnumerator AfterRammingCooldownTimer()
    {
        if (!readyToRam) { yield break; } // Another timer is already active...

        moveTime = 0f;

        readyToRam = false;

        // End chasing
        EndAggro();

        float cooldownTimeLeft = cooldownTimeAfterRamming;

        while (cooldownTimeLeft > 0f)
        {
            cooldownTimeLeft -= Time.deltaTime;

            Debug.DrawLine(transform.position, aggroTarget.transform.position, Color.white, 0.1f);

            yield return new WaitForSeconds(Time.deltaTime);
        }

        // Cooldown has ended
        readyToRam = true;

        // Check the need to aggro again
        CheckNeedToAggroAgain();
    }
    #endregion
}
