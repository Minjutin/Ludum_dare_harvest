using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player inventory contains an array of every item in the player's inventory. It can hold max 3 items simultaneously.

//In this script, you can check if there is an item in certain slot, what the item is, remove it from the inventory or add a new item if there is enough space.

public class PlayerInventory : MonoBehaviour
{
    //How many inventory items.
    const int inventorySize = 3;

    //Return if the inventory is full.
    public bool isFull()
    {
        bool full = true;
        for(int i = 0; i<inventorySize; i++)
        {
            if (inventoryItems[i] == null)
            {
                full = false;
            }
        }
        return full;
    }

    //Array of items in the inventory
    InventoryItem[] inventoryItems = new InventoryItem[inventorySize];

    public bool thereIsItem(int slot)
    {
        if (inventoryItems[slot] != null)
        {
            return true;
        }
        return false;
    } //Check if there is an item in this slot.


    public InventoryItem GetItem(int slot)
    {
        return inventoryItems[slot];
    } //Get inventory item.

    public void AddItem(InventoryItem item)
    {
        if (!isFull())
        {
            for (int i = 0; i < inventorySize; i++)
            {
                if (inventoryItems[i] != null) //Find the first array placement with no element and add the item.
                {
                    inventoryItems[i] = item;
                    break;
                }
            }
        }
        else {
            Debug.LogError("Inventory is full but you're trying to collect an item.");
        }


    } //Add item to the player's inventory.

    public void RemoveItem(int slot)
    {
        if ( slot==0 || slot==1 || slot==2 )
        { 
            inventoryItems[slot] = null;
        }
    } //Drop the chosen item in the inventory.
}

