using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    PlayerInventory inventory;

    //Chosen inventory slot
    int chosenSlot = 0;

    private void Awake()
    {
        inventory = this.GetComponent<PlayerInventory>(); //Fetch inventory
    }

    //TODO check if interaction button.
    private void InteractionButtonPressed()
    {
        //Tile tile = fetch tile we were are

        //TODO check if something has spawned on the spawnTile
        //if(tile is spawnTile) {
            //if(tile has InventoryItem){
                InventoryItem item = new InventoryItem(); //Get the item from the tile
                Collect(item);
        //}
        //}

        //TODO check if tile is fertile.
        //if(tile is fertileTile){
            //if tile has no plant and you have seed in your inventory slot
                PlantSeed();
            //if tile has a plant and plant is ready, collect it.
            //otherwise be like "NO"
        //}

        //TODO check if you can sacrifice the fruit on the tile
        //if(you can sacrifice on this tile){
            //if you have inventory item chosen, give it to the gods
        //}
    }

    private void SacrificeItem()
    {
        if (inventory.thereIsItem(chosenSlot))
        {
            inventory.RemoveItem(chosenSlot);
            //TODO Commit the sacrification.
        }
        else
        {
            //TODO if the inventory slot is empty.
        }
    } //Sacrifice item from the inventory.

    private void PlantSeed() //TODO Add a Tile class object here!
    {
        if (inventory.thereIsItem(chosenSlot))
        {
            if(inventory.GetItem(chosenSlot) is Seed)
            {
                inventory.RemoveItem(chosenSlot);
                //Plant seed to the Tile in question
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
    }

    private void Collect(InventoryItem item) //TODO add a Tile object to here!
    {
        if (!inventory.isFull())
        {
            inventory.AddItem(item);
        }
        else
        {
            Debug.LogWarning("Inventory is full but you are trying to collect an item.");
        }
    } //Collect the item.

    private void ChangeSlot()
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
}
