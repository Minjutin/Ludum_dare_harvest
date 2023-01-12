using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Everything that is related to the player interaction.
//Plant, Sacrifice, collect



public class PlayerInteraction : MonoBehaviour
{
    [HideInInspector]
    public PlayerInventory inventory;
    PlayerStats stats;
    PlayerPoints points;

    [SerializeField] GameObject playerController;

    //Currently chosen inventory slot (between 1-3)
    int chosenSlot = 0;

    private void Awake()
    {

        inventory = playerController.GetComponent<PlayerInventory>(); //Fetch inventory
        stats = playerController.GetComponent<PlayerStats>();
        points = playerController.GetComponent<PlayerPoints>();
    }

    #region Button detection

    //If interaction button has been pressed.
    public void InteractionButtonPressed(TileDaddy tile)
    {
        //If tile is fertile tile, either take the item, plant a seed or fertilize the plant.
        if (tile is FertileTile)
        {
            FertileTile fTile = tile as FertileTile;
            
            //If fertiled tile contains an item that is not a plant.
            if (fTile.HasItem())
            {
                //------------------------ TAKE ITEM ------------------------------------------------

                if (fTile.item is InventoryItem)
                {
                    TryCollect(fTile);
                }

                //------------------------ TAKE ITEM ------------------------------------------------


                else if (fTile.item is Plant)
                {
                    Plant plant = fTile.item as Plant;
                    //TODO If plant is not ready
                    if (plant.level < Plant.maxLevel)
                    {

                        //------------------------ FERTILIZE PLANT ------------------------------------------------

                        TryFertilize(fTile);

                        //------------------------ FERTILIZE PLANT ------------------------------------------------

                    }

                    //------------------------ COLLECT PLANT ---------------------------------------
                    else if (plant.level >= Plant.maxLevel)
                    {
                        TryCollect(fTile);
                    }
                    //------------------------ COLLECT PLANT ---------------------------------------


                }


                //------------------------ PLANT SEED ------------------------------------------------
            }
            //If fertiled tile does not contain an item and player is holding a seed.
            else if (!fTile.HasItem() && fTile.fertilityLevel != Enums.Fertility.F0)
            {

                PlantSeed(fTile);
            }

            //------------------------ PLANT SEED ------------------------------------------------
        }

        //If tile is sacrifice tile, check if there is an item to be sacrifices.
        if (tile is SacrificeTile)
        {
            SacrificeTile stile = tile as SacrificeTile;

            if (stats.god == stile.god)  //If god and sacrifice tile matches up.
            { 
                SacrificeItem();
            }
        }
        //TODO check if you can sacrifice the fruit on the tile
        //if(you can sacrifice on this tile){
        //if you have inventory item chosen, give it to the gods
        //}
    }

    //If change slot button has been pressed.
    public void ChangeSlot()
    {
        switch (chosenSlot)
        {
            case 0:
                chosenSlot = 1;                
                break;
            case 1:
                chosenSlot = 2;
                break;
            case 2:
                chosenSlot = 0;
                break;
            default:
                Debug.Log("There is no slot with number " + chosenSlot);
                break;
        }

        inventory.MakeChosen(chosenSlot);
        //Debug
        //if (inventory.ThereIsItem(chosenSlot))
        //{
        //    Debug.Log("Item is now " + inventory.GetItem(chosenSlot).itemName);
        //}
        //else
        //    Debug.Log("Slot " + chosenSlot + " is empty");

    }

    #endregion


    #region Different Interactions


    //-----------------------------------------------------------
    //------------------- SACRIFICE -----------------------------
    //-----------------------------------------------------------

    private void SacrificeItem()
    {
        if (inventory.ThereIsItem(chosenSlot))
        {
            InventoryItem item = inventory.GetItem(chosenSlot);
            points.GiveItem(item);
            inventory.RemoveItem(chosenSlot);

        }
        else
        {
            //TODO if the inventory slot is empty.
        }
    } //Sacrifice item from the inventory.


    //--------------------------------------------------------
    //------------------- PLANT SEED -------------------------
    //--------------------------------------------------------

    private void PlantSeed(FertileTile tile)
    {
        if (inventory.ThereIsItem(chosenSlot))
        {
            if (inventory.GetItem(chosenSlot) is Seed)
            {
                Plant planted = new Plant();
                tile.SetItem(planted); //Plant the plant to the tile.

                switch (planted.Type)
                {
                    case Enums.FruitType.Fruit1:
                        tile.SpawnPlant(Instantiate(Resources.Load("Plant/Plant1"), tile.position, Quaternion.identity) as GameObject);
                        break;
                    case Enums.FruitType.Fruit2:
                        tile.SpawnPlant(Instantiate(Resources.Load("Plant/Plant2"), tile.position, Quaternion.identity) as GameObject);
                        break;
                    case Enums.FruitType.Fruit3:
                        tile.SpawnPlant(Instantiate(Resources.Load("Plant/Plant3"), tile.position, Quaternion.identity) as GameObject);
                        break;
                    case Enums.FruitType.Fruit4:
                        tile.SpawnPlant(Instantiate(Resources.Load("Plant/Plant4"), tile.position, Quaternion.identity) as GameObject);
                        break;
                    default:
                        Debug.LogWarning("Plant doesn't have a type for some reason. That's no cool.");
                        break;
                }


                inventory.RemoveItem(chosenSlot); //Delete item from the inventory
            }
            else
            {
                //TODO if the inventory item has a wrong type
            }
        }
        else
        {
            //TODO if the inventory slot is empty.
        }
    } //Plant seed from the inventory.

    //-----------------------------------------------------------
    //------------------- COLLECT ITEMS -------------------------
    //-----------------------------------------------------------

    private void TryCollect(FertileTile tile) //Collect an item from a tile
    {
        //------ Check if there is an item.
        if (!tile.HasItem()) //Check if there is an item.
            Debug.LogWarning("Tile does not contain an item even though you're trying to collect it.");
        
        //------- Check if the item is InventoryItem.
        else if (tile.item is InventoryItem) 
        {
            InventoryItem item = tile.item as InventoryItem;

            //If there is space in the inventory, remove the object.
            if (!inventory.IsFull())
            {
                inventory.AddItem(item); //Aadd item to inventory.
                tile.RemoveItem(); //Remove the item from the tile.
                Destroy(tile.itemGO);
                tile.itemGO = null; // Remove indicator
            }
            else  {
                //TODO inventory is full.
            }

        }

        //------ Check if the item is plant
        else if (tile.item is Plant)
        {
            Plant plant = tile.item as Plant;

            //If there is space in the inventory, remove the object.
            if (!inventory.IsFull() && plant.level >= Plant.maxLevel)
            {
                Enums.FruitType plantType = plant.Type;
                inventory.AddItem(new Fruit(plantType)); //Add fruit.
                tile.RemoveItem(); //Remove the plant from the tile.
                Destroy(tile.itemGO);
                tile.itemGO = null; // Remove indicator
            }
            else
            {
                //TODO inventory is full.
            }
        }

    }



    //-----------------------------------------------------------
    //------------------- FERTILIZE -------------------------
    //-----------------------------------------------------------

    private void TryFertilize(FertileTile tile)
    {
        Plant plant = tile.item as Plant;
        if(inventory.GetItem(chosenSlot) is Manure)
        {
            plant.FullLevel();
            inventory.RemoveItem(chosenSlot);
        }
    }
    #endregion


}
