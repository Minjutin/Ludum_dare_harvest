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

    [Header("Pooping")]
    [SerializeField] float timeBetweenPoops = 5f;

    [Header("RAMMING")]
    [SerializeField] float aggroRadius = 3f;
    [SerializeField] float knockBackPower = 5f;
    [Space]
    [SerializeField] float timeBeforePacification = 5f;

    #endregion

    private void Start()
    {
        tileManager = FindObjectOfType<TileManager>();

        if (tileManager)
        TryToPoop();
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

    IEnumerator ManureTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenPoops);

            TryToPoop();
        }
    }
    #endregion

    #region RAMMING

    #endregion
}
