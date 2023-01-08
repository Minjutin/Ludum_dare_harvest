using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Everything that is related to the player interaction.
//Plant, Sacrifice, collect



public class PlayerInteraction : MonoBehaviour
{
    PlayerInventory inventory;

    //Currently chosen inventory slot (between 1-3)
    int chosenSlot = 0;

    private void Awake()
    {
        inventory = this.GetComponent<PlayerInventory>(); //Fetch inventory
    }


    #region Button detection

    //If interaction button has been pressed.
    public void InteractionButtonPressed()
    {
        // ---- TODO CHECK THE TILE ACTUALLY ----
        TileDaddy tile = new TileDaddy();
        // ---- TODO ENDS ----


        //If tile is fertile tile, either take the item, plant a seed or fertilize the plant.

        if (tile is FertileTile)
        {
            FertileTile fTile = tile as FertileTile;

            //If fertiled tile contains an item that is not a plant.
            if (fTile.item != null)
            {
                //------------------------ TAKE ITEM ------------------------------------------------

                if (fTile.item is InventoryItem) {
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
                        //Fertilize
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

                //If fertiled tile does not contain an item and player is holding a seed.
                else if (fTile.item == null)
                {
                    PlantSeed(fTile);
                }

                //------------------------ PLANT SEED ------------------------------------------------

            }

            //If tile is sacrifice tile, check if there is an item to be sacrifices.
            if (tile is SacrificeTile)
            {
                SacrificeItem();
            }
            //TODO check if you can sacrifice the fruit on the tile
            //if(you can sacrifice on this tile){
            //if you have inventory item chosen, give it to the gods
            //}
        } 
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
                chosenSlot = 3;
                break;
            default:
                Debug.Log("There is no slot with number " + chosenSlot);
                break;
        }
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

            //TODO change the points.
            if (item is Manure)
            {
                //points -amount
            }
            if (item is Fruit)
            {
                //if normal fruit +1, if favorite fruit +3, if needed fruit + 7
            }

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
                tile.SetItem(inventory.GetItem(chosenSlot)); //Plant the plant to the tile.
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

    public void TryCollect(FertileTile tile) //Collect an item from a tile
    {
        //------ Check if there is an item.
        if (tile.item == null) //Check if there is an item.
            Debug.LogWarning("Tile does not contain an item even though you're trying to collect it.");
        
        //------- Check if the item is InventoryItem.
        if (tile.item is InventoryItem) 
        {
            InventoryItem item = tile.item as InventoryItem;

            //If there is space in the inventory, remove the object.
            if (!inventory.isFull())
            {
                inventory.AddItem(item); //Aadd item to inventory.
                tile.RemoveItem(); //Remove the item from the tile.
            }
            else  {
                //TODO inventory is full.
            }

        }

        //------ Check if the item is plant
        if (tile.item is Plant)
        {
            Plant plant = tile.item as Plant;

            //If there is space in the inventory, remove the object.
            if (!inventory.isFull())
            {
                Enums.FruitType plantType = plant.Type;
                inventory.AddItem(new Fruit(plantType)); //Add fruit.
                tile.RemoveItem(); //Remove the plant from the tile.
            }
            else
            {
                //TODO inventory is full.
            }
        }

    } 

    #endregion

    
}
