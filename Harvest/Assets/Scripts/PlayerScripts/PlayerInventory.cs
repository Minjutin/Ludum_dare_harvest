using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player inventory contains an array of every item in the player's inventory. It can hold max 3 items simultaneously.s
//In this script, you can check if there is an item in certain slot, what the item is, remove it from the inventory or add a new item if there is enough space.

public class PlayerInventory : MonoBehaviour
{
    //How many inventory items.
    const int inventorySize = 3;

    //Initialize
    PlayerStats stats;

    void Awake()
    {
        stats = this.GetComponent<PlayerStats>();
    }

    //-------------------------------------------------------------

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
        if (!IsFull())
        {
            for (int i = 0; i < inventorySize; i++)
            {
                if (inventoryItems[i] != null) //Find the first array placement with no element and add the item.
                {
                    inventoryItems[i] = item; //add item
                    stats.ChangeSpeed(ItemsAmount()); //change speed
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
        if(slot < 0 ||slot > 3)
        {
            Debug.LogError("There is no inventory slot "+slot);
        }

        if (this.ThereIsItem(slot))
        { 
            inventoryItems[slot] = null; //make the slot null.
            stats.ChangeSpeed(ItemsAmount()); //Change speed.
        }

    } //Remove the chosen item in the inventory.

    public void DropAll()
    {
        for(int i = 0; i<3; i++)
        {
            inventoryItems[i] = null; //make the slot null.
        }
        stats.ChangeSpeed(0);
    }
    #endregion

    //-----------------------------------------------------------

    //Check how many items is currently in the inventory and related stuff
    #region Inventory size

    public int ItemsAmount()
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
    public bool IsFull()
    {
        if (ItemsAmount() == 3)
        {
            return true;
        }
        return false;
    } //Return if the inventory is full.

    #endregion

    //-----------------------------------------------------------
}

