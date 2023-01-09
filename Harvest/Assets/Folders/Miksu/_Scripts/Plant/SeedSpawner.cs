using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpawner : MonoBehaviour
{
    TileManager tileManager;

    [Header("Spawning")]
    public bool spawningOn = true;
    [SerializeField]
    float timeBetweenSpawns = 1f;

    private void Start()
    {
        tileManager = FindObjectOfType<TileManager>();

        // Start coroutine for spawning
        StartSpawning();
    }


    #region SPAWNING

    private bool TrySpawnSeedAt(int x, int y, FertileTile tile)
    {
        // Cancel if the tile already has an Item
        if (tile.HasItem()) { return false; }

        // Cancel if fertility is too low
        //if (tile.fertilityLevel == Enums.Fertility.F0) { return false; }

        Vector3 pos = new Vector3(x, 0, y);

        // Give the Seed Item to the tile
        Seed seed = new Seed();
        tile.SetItem(seed);
        tile.SpawnSeed(Instantiate(Resources.Load("Seed"), pos, Quaternion.identity) as GameObject);

        // Return as succesful
        return true;
    }


    public void StartSpawning()
    {
        StartCoroutine(SeedSpawnCoroutine());
    }

    IEnumerator SeedSpawnCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(timeBetweenSpawns);

        int gridRows = tileManager.gridRows;
        int gridColumns = tileManager.gridColumns;

        while (spawningOn)
        {
            yield return wait;

            // Pick a random tile
            int rand_X = Random.Range(0, gridRows);
            int rand_Y = Random.Range(0, gridColumns);

            TileDaddy candidate = tileManager.GetTileAt(rand_X, rand_Y);
            candidate = candidate as FertileTile;
            if (candidate is FertileTile)
            {
                // Try to spawn a seed
                bool success;
                success = TrySpawnSeedAt(rand_X, rand_Y, candidate as FertileTile);

                //if (!success) { Debug.Log("Failed to spawn plant at: (" + rand_X + "," + rand_Y + ")"); }

            }


        }

    }

    #endregion

}
