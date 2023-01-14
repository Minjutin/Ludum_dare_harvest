using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    #region Properties
    // References
    [Header("PlayerControllers")]
    [SerializeField] GameObject playerController1;
    [SerializeField] GameObject playerController2;

    GameObject player1;
    GameObject player2;

    PlayerStats playerStats1;
    PlayerStats playerStats2;

    [Header("RespawnPoints")]
    [SerializeField] Transform spawnPoint1;
    [SerializeField] Transform spawnPoint2;
    [SerializeField] Transform spawnPoint3;
    [SerializeField] Transform spawnPoint4;

    [Header("Spawn Time")]
    [SerializeField] float spawnTime = 1f;

    [Header("Spawn Distance")]
    [SerializeField] float spawnAfterYValue = -10f;

    #endregion

    #region Setup
    private void Awake()
    {
        // Get references
        playerStats1 = playerController1.GetComponent<PlayerStats>();
        playerStats2 = playerController2.GetComponent<PlayerStats>();

        player1 = playerController1.GetComponent<PlayerInput>().player;
        player2 = playerController2.GetComponent<PlayerInput>().player;
    }

    private void Start()
    {
        // Set up Players to correct spawn positions
        SpawnPlayer(player1, playerStats1);
        SpawnPlayer(player2, playerStats2);


    }
    #endregion

    #region Spawning
    private void SpawnPlayer(GameObject player, PlayerStats stats)
    {
        // Check what God they have

        player.transform.position = GetSpawn(stats);
    }

    public void RespawnPlayer(GameObject player)
    {
        // Check if fallen far enough
        if (player.transform.position.y <= spawnAfterYValue)
        {

            // Connect the stats
            PlayerStats stats = null;
            if      (player == player1) { stats = playerStats1; }
            else if (player == player2) { stats = playerStats2; }



            // Empty Inventory
            stats.gameObject.GetComponent<PlayerInventory>().DropAll();

            SpawnPlayer(player, stats);
        }
    }

    IEnumerator SpawnTimer(GameObject player, PlayerStats stats)
    {


        // Wait
        yield return new WaitForSeconds(spawnTime);

        // Visual Effects here? TODO

        // Move Player to spawn point
        SpawnPlayer(player, stats);
    }

    private Vector3 GetSpawn(PlayerStats stats)
    {
        // Check what God they have
        Enums.God god = stats.god;
        if (god == Enums.God.God1)
        {
            return spawnPoint1.position;
        }
        else if (god == Enums.God.God2)
        {
            return spawnPoint2.position;
        }
        else if (god == Enums.God.God3)
        {
            return spawnPoint3.position;
        }
        else if (god == Enums.God.God4)
        {
            return spawnPoint4.position;
        }

        // If none, default spawn
        return Vector3.zero;
    }
    #endregion
}
