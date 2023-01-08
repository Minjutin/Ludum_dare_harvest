using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player inventory contains an array of every item in the player's inventory. It can hold max 3 items simultaneously.

//In this script, you can check if there is an item in certain slot, what the item is, remove it from the inventory or add a new item if there is enough space.

public class PlayerInventory : MonoBehaviour
{
    //How many inventory items.
    const int inventorySize = 3;

    //Check, use, add and remove items.
    #region Using inventory
    
    //Array of items in the inventory
    InventoryItem[] inventoryItems = new InventoryItem[inventorySize];

    public bool ThereIsItem(int slot)
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

    #endregion

    //-----------------------------------------------------------

    //Check how many items is currently in the inventory and related stuff
    #region Inventory size

    public int itemsAmount()
    {
        int amount = 0;
        for (int i = 0; i < inventorySize; i++)
        {
            if (inventoryItems[i] != null)
            {
                amount++;
            }
        }
        return amount;
    } //Return the amount of items currently in the inventory.
    public bool isFull()
    {
        if (itemsAmount() == 3)
        {
            return true;
        }
        return false;
    } //Return if the inventory is full.

    #endregion

    //-----------------------------------------------------------
}

