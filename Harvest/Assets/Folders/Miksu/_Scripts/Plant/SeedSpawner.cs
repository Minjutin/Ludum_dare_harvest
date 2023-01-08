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

    public void SpawnSeedAt(int x, int y, FertileTile tile)
    {
        Vector3 pos = new Vector3(x, 0, y);

        // Give Tile an Item
        Plant plant = new Plant();
        tile.SetItem(plant);

        // Spawn the GameObject
        GameObject plantGO = Resources.Load<GameObject>("Plant/Plant");
        Instantiate(plantGO, pos, Quaternion.identity);
    }

    #endregion

    public void StartSpawning()
    {
        StartCoroutine(SeedSpawnCoroutine());
    }

    IEnumerator SeedSpawnCoroutine()
    {
        WaitForSeconds wait = new WaitForSeconds(timeBetweenSpawns);

        while (spawningOn)
        {
            yield return wait;

            // Try to spawn a seed
        }

    }
}
