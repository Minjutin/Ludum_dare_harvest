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


    [Header("Speeds")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float aggroSpeed = 10f;

    [Header("Moving")]
    [SerializeField] float changeDirectionFrequency = 2f;

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
        StartCoroutine(MovingCoroutine());
    }

    IEnumerator MovingCoroutine()
    {
        
        while (pacifist)
        {
            // Pick a direction
            Vector3 randomDir = ChangeDirection();

            ChangeDirection();

            yield return new WaitForSeconds(changeDirectionFrequency);

        }
    }

    private Vector3 ChangeDirection()
    {
        transform.RotateAround(transform.position, Vector3.up, Random.Range(-180, 180));
        Quaternion rotation = transform.rotation;
        // Reset GO rotation
        //transform.rotation = Quaternion.identity;

        Vector3 direction = rotation * Vector3.forward;
        direction.Normalize();

        return direction;
    }
    #endregion

    #region RAMMING

    #endregion
}
