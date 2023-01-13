using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    #region Properties
    TileManager tileManager;

    PlayerInventory playerInventory;
    PlayerInteraction playerInteraction;

    [Tooltip("How long the OTHER Player will remain stunned")]
    [SerializeField] float stunTime = 1f;

    [Header("Knockback Power")]
    [SerializeField] float stunKnockBackPower = 15f;

    [Header("Item Fly Distance")]
    [Tooltip("How far the items will fly on getting stunned.")]
    [SerializeField] float itemFlyDistance = 2f;

    #endregion

    #region Setup
    private void Start()
    {
        // Get references
        tileManager = FindObjectOfType<TileManager>();

        playerInteraction = GetComponent<PlayerInteraction>();
        playerInventory = playerInteraction.inventory;
    }
    #endregion

    #region Collision Checking
    private void OnCollisionEnter(Collision collision)
    {
        // Check if other is Player
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Collision with Player is happening
            CollideWithPlayer(collision.gameObject);
        }
    }

    private void CollideWithPlayer(GameObject otherPlayer)
    {
        // Check inventory size
        int otherItemsAmount = otherPlayer.GetComponent<PlayerInteraction>().inventory.ItemsAmount();
        int yourItemsAmount = playerInventory.ItemsAmount();

        // If it is higher than your inventory...
        if (yourItemsAmount <= otherItemsAmount)
        {
            // --> STUN the other Player
            StunOtherPlayer(otherPlayer);
        }
    }
    #endregion

    #region Stunning
    private void StunOtherPlayer(GameObject otherPlayer)
    {
        // Get their rb
        PlayerMovement pMove = otherPlayer.GetComponent<PlayerMovement>();

        // Calculate the HitDirection
        Vector3 hitDir = (otherPlayer.transform.position - transform.position).normalized;

        // Ram them
        pMove.GetRammed(hitDir, stunKnockBackPower);

        // Actually stun them
        otherPlayer.GetComponent<PlayerStun>().GetStunned(stunTime, hitDir);

    }

    public void GetStunned(float stunDuration, Vector3 hitDir)
    {
        // Activate the Stun Timer
        StartCoroutine(StunTimer(stunDuration));

        // Drop your items
        DropItemsOnStun(hitDir);
    }

    IEnumerator StunTimer(float stunDuration)
    {
        // Get playerInput
        PlayerInput input = playerInventory.GetComponent<PlayerInput>();

        //Debug.Log("Begin stun");

        // Check if already stunLocked
        if (input.stunLocked != true)
        {
            // Stunlock them
            input.stunLocked = true;

            // Wait for the stunTime
            yield return new WaitForSeconds(stunDuration);

            // End the stunlock
            input.stunLocked = false;
            //Debug.Log("END stun");
        }
    }
    #endregion

    #region Drop Items
    private void DropItemsOnStun(Vector3 hitDirection)
    {
        // Get a list of the tiles near Player
        List<TileDaddy> nearTiles = GetAvailableTiles(hitDirection);

        // Get another list for empty tiles
        List<TileDaddy> emptyTiles = GetEmptyTiles(nearTiles);
        // Add inventory items there

        // Remove them from inventory
    }

    private List<TileDaddy> GetAvailableTiles(Vector3 hitDir)
    {
        Vector3 curPos = transform.position;

        // Adjust Drop Position towards Hit Direction
        Vector3 dropPos = curPos + (hitDir * itemFlyDistance);


        // Get Player pos
        int x = Mathf.RoundToInt(dropPos.x);
        int y = Mathf.RoundToInt(dropPos.z);

        List<TileDaddy> availableTiles = new List<TileDaddy>();

        // Get tiles around that center coordinate
        for (int _x = -1; _x < 2; _x++)
        {
            for (int _y = -1; _y < 2; _y++)
            {
                // If tile is valid (exists), add it to the list
                if (tileManager.CheckTileExistance(_x + x, _y + y))
                {
                    availableTiles.Add(tileManager.GetTileAt(_x + x, _y + y));

                }
            }
        }

        // Return list of available tiles
        return availableTiles;
    }

    private List<TileDaddy> GetEmptyTiles(List<TileDaddy> tiles)
    {
        if (tiles == null) { return null; }

        //Debug.Log("Tiles amount: " + tiles.Count);

        List<TileDaddy> emptyTiles = new List<TileDaddy>();

        // Get tiles around that center coordinate
        foreach (TileDaddy tile in tiles)
        {
            //Debug.Log("TIle pos: " + tile.position);

            // If tile is valid (exists), add it to the list
            if (tile is FertileTile)
            {

                int x = Mathf.RoundToInt(tile.position.x);
                int y = Mathf.RoundToInt(tile.position.z);

                //if (!(tileManager.GetTileAt(x, y) as FertileTile).HasItem())
                if (!(tile as FertileTile).HasItem())
                {

                    // Doesn't have an item, add it
                    emptyTiles.Add(tile);

                    //Debug.Log("Available tile found at: " + (_x + x) + "," + (_y +y));
                    Debug.DrawRay(new Vector3(x, 0, y), Vector3.up, Color.green, 0.5f);

                }
                else
                {
                    Debug.DrawRay(new Vector3(x, 0, y), Vector3.up, Color.red, 0.7f);
                }
            }

        }
        // Return list of available tiles
        return emptyTiles;
    }

    private void DropInventoryItemsOnGround(List<TileDaddy> tiles)
    {
        // Add them to the tiles

        // Remove them from inventory
    }
    #endregion
}
