using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStun : MonoBehaviour
{
    #region Properties
    TileManager tileManager;

    PlayerInventory playerInventory;
    PlayerInteraction playerInteraction;

    [Header("Being stunned")]
    [Tooltip("How long the Player will remain stunned")]
    [SerializeField] float stunTime = 1f;


    [Header("Knockback Power")]
    [Tooltip("The Power this Player will RAM other players with")]
    [SerializeField] float stunKnockBackPower = 15f;

    [Header("Item Drop Distance")]
    [Tooltip("How far the items will fly on getting stunned. Ramming direction x Distance = center of 3x3 Drop-Grid.")]
    [SerializeField] float itemDropDistance = 2f;

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
            Debug.Log(otherPlayer.name + " got stunned with " + otherItemsAmount + " items");
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

        // Drop inventory items there
        DropInventoryItemsOnGround(emptyTiles);

    }

    private List<TileDaddy> GetAvailableTiles(Vector3 hitDir)
    {
        Vector3 curPos = transform.position;

        // Adjust Drop Position towards Hit Direction
        Vector3 dropPos = curPos + (hitDir * itemDropDistance);


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
                    // Doesn't have an item, add it to the list
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
        int numberOfItems = playerInventory.ItemsAmount();

        if (numberOfItems == 0) { return; }
        //if (tiles.Count == 0) { return; }

        // Choose random tiles up to the amount of inventory items
        int amountOfTilesSelected = Mathf.Min(playerInventory.ItemsAmount(), tiles.Count);

        List<FertileTile> dropTiles = new List<FertileTile>();

        for (int i = 0; i < amountOfTilesSelected; i++)
        {
            // Select a tile at random
            FertileTile candidate = tiles[Random.Range(0, tiles.Count)] as FertileTile;

            // If not, add it to the pile
            dropTiles.Add(candidate);

            // Remove from the old list
            tiles.Remove(candidate);
        }

        // Get the list of Items
        List<InventoryItem> items = playerInventory.GetAllItems();

        //playerInventory.PrintInventory();

        // Add ITEMS to the tiles
        for (int y = 0; y < amountOfTilesSelected; y++)
        {
            // Allocate 1 item to each of the randomly selected tiles
            dropTiles[y].SetItem(items[y]);

            // Give tiles the items
            SetCorrectItem(items[y], dropTiles[y]);
        }

        // Empty Player Inventory regardless of how many tiles were present
        playerInventory.DropAll();
    }

    private void SetCorrectItem(InventoryItem item, FertileTile tile)
    {
        // SEEDS
        if (item is Seed)
        {
            GameObject seed = Instantiate(Resources.Load("Seed"), tile.position, Quaternion.identity) as GameObject;
            tile.itemGO = seed;
        }

        // FRUITS
        if (item is Fruit)
        {
            // What kind of fruit
            if ((item as Fruit).Type == Enums.FruitType.Fruit1)
            {
                // Set it as Fruit
                GameObject fruit = Instantiate(Resources.Load("Fruits/FruitGO1"), tile.position, Quaternion.identity) as GameObject;
                tile.itemGO = fruit;
            }
            else if ((item as Fruit).Type == Enums.FruitType.Fruit2)
            {
                GameObject fruit = Instantiate(Resources.Load("Fruits/FruitGO2"), tile.position, Quaternion.identity) as GameObject;
                tile.itemGO = fruit;
            }
            else if ((item as Fruit).Type == Enums.FruitType.Fruit3)
            {
                GameObject fruit = Instantiate(Resources.Load("Fruits/FruitGO3"), tile.position, Quaternion.identity) as GameObject;
                tile.itemGO = fruit;
            }
            else if ((item as Fruit).Type == Enums.FruitType.Fruit3)
            {
                GameObject fruit = Instantiate(Resources.Load("Fruits/FruitGO4"), tile.position, Quaternion.identity) as GameObject;
                tile.itemGO = fruit;
            }
        }

        // MANURE
        if (item is Manure)
        {
            // Set it as Manure
            GameObject manure = Instantiate(Resources.Load("Manure"), tile.position, Quaternion.identity) as GameObject;
            tile.itemGO = manure;
        }
    }
    #endregion
}
