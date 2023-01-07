using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player inventory contains an array of every item in the player's inventory. It can hold max 5 items simultaneously.

public class PlayerInventory : MonoBehaviour
{
    //How many inventory items.
    const int inventorySize = 3;

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

    public void AddItem(InventoryItem item)
    {
        bool itemAdded = false;

        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] != null)
            {
                inventoryItems[i] = item;
                itemAdded = true;
                break;
            }
        }

        if (!itemAdded)
        {
            //TODO if the player couldn't add the item.
        }

    } //Add item to the player's inventory.

    public void DropItem(int slot)
    {
        if ( slot==0 || slot==1 || slot==2 )
        { 
            inventoryItems[slot] = null;
        }
    } //Drop the chosen item in the inventory.
}

