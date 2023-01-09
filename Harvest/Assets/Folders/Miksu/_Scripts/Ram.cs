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

    [Header("RAMMING")]
    bool pacifist = true;
    [SerializeField] float aggroRadius = 3f;
    [SerializeField] SphereCollider aggroTrigger;
    [SerializeField] float knockBackPower = 5f;
    [Space]
    [SerializeField] float timeBeforePacification = 5f;

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
        StartCoroutine(TurningCoroutine());
        StartCoroutine(MovingCoroutine());
    }

    #region Turning
    IEnumerator TurningCoroutine()
    {

        while (pacifist)
        {
            // Pick a direction
            //Vector3 randomDir = ChangeDirection();

            moveDirection = ChangeDirection();
            // Although check distance to lake too
            CheckDistanceToLake();

            // Add some movetime
            moveTime = Random.Range(minMoveTime, maxMoveTime);

            yield return new WaitForSeconds(changeDirectionFrequency);

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
        Vector3 dir = transform.position - pos;
        dir.Normalize();
        return dir;
    }

    #endregion

    #region MOVING
    IEnumerator MovingCoroutine()
    {
        while (pacifist)
        {
            if (moveTime > 0)
            {
                // Get next pos
                Vector3 nextPos = transform.position + moveDirection * walkSpeed * Time.deltaTime;

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

    #region RAMMING

    #endregion
}
