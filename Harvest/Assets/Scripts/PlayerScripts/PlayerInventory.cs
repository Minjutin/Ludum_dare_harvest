using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player inventory contains an array of every item in the player's inventory. It can hold max 3 items simultaneously.s
//In this script, you can check if there is an item in certain slot, what the item is, remove it from the inventory or add a new item if there is enough space.

public class PlayerInventory : MonoBehaviour
{
    //How many inventory items.
    const int inventorySize = 3;

    GameObject inventory;
    GameObject[] inventorySlots = new GameObject[3];
    GameObject[] items = new GameObject[3];
    Object[] sprites;

    //Initialize
    PlayerStats stats;

    void Start()
    {
        stats = this.GetComponent<PlayerStats>();

        inventory = this.GetComponent<PlayerInput>().player.transform.Find("Graphics").transform.Find("Inventory").gameObject;
        inventorySlots[0] = inventory.transform.Find("Slot1").gameObject;
        inventorySlots[1] = inventory.transform.Find("Slot2").gameObject;
        inventorySlots[2] = inventory.transform.Find("Slot3").gameObject;

        for (int i = 0; i < inventorySize; i++)
        {
            items[i] = inventorySlots[i].transform.Find("Item").gameObject;
        }

        sprites = Resources.LoadAll("InventorySprites", typeof(Sprite));

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

    // Returns a list of all items
    public List<InventoryItem> GetAllItems()
    {
        List<InventoryItem> allItems = new List<InventoryItem>();

        foreach (InventoryItem item in inventoryItems)
        {
            allItems.Add(item);
        }

        return allItems;
    }

    public void AddItem(InventoryItem item)
    {
        if (!IsFull())
        {
            for (int i = 0; i < inventorySize; i++)
            {
                if (!ThereIsItem(i)) //Find the first array placement with no element and add the item.
                {
                    inventoryItems[i] = item; //add item

                    //Change sprite
                    Sprite newSprite = sprites[0] as Sprite;

                    #region What item
                    if (item is Fruit)
                    {
                        Fruit fruit = item as Fruit;
                        if (fruit.Type == Enums.FruitType.Fruit1)
                            newSprite = sprites[0] as Sprite;
                        if (fruit.Type == Enums.FruitType.Fruit2)
                            newSprite = sprites[1] as Sprite;
                        if (fruit.Type == Enums.FruitType.Fruit3)
                            newSprite = sprites[2] as Sprite;
                        if (fruit.Type == Enums.FruitType.Fruit4)
                            newSprite = sprites[3] as Sprite;
                    }
                    else if (item is Manure)
                        newSprite = sprites[4] as Sprite;
                    else if (item is Seed)
                        newSprite = sprites[5] as Sprite;
                    else
                    {
                        Debug.LogWarning("Item is not a known inventory item");
                    }
                    #endregion

                    items[i].GetComponent<SpriteRenderer>().sprite = newSprite;

                    stats.ChangeSpeed(ItemsAmount()); //change speed
                    //PrintInventory();
                    break;
                }
            }
        }
        else
        {
            Debug.LogError("Inventory is full but you're trying to collect an item.");
        }


    } //Add item to the player's inventory. Must be used only when you're sure that the inventory isn't full.

    public void RemoveItem(int slot)
    {
        if (slot < 0 || slot > 3)
        {
            Debug.LogError("There is no inventory slot " + slot);
        }

        if (this.ThereIsItem(slot))
        {
            inventoryItems[slot] = null; //make the slot null.
            items[slot].GetComponent<SpriteRenderer>().sprite = null;
            stats.ChangeSpeed(ItemsAmount()); //Change speed.
            //PrintInventory();
        }

    } //Remove the chosen item in the inventory.

    public void DropAll()
    {
        for (int i = 0; i < 3; i++)
        {
            inventoryItems[i] = null; //make the slot null.
            items[i].GetComponent<SpriteRenderer>().sprite = null;
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
        if (ItemsAmount() >= inventorySize)
        {
            return true;
        }
        return false;
    } //Return if the inventory is full.

    #endregion

    //-----------------------------------------------------------

    //Only for Debug purposes
    public void PrintInventory()
    {

        Debug.Log("Inventory placement " + 1 + ": " + inventoryItems[0] + "\n" +
            "Inventory placement " + 2 + ": " + inventoryItems[1] + "\n" +
            "Inventory placement " + 3 + ": " + inventoryItems[2]);

    }

    public void MakeChosen(int slot)
    {
        for (int i = 0; i < inventorySize; i++)
        {

            if (slot == i)
            {
                inventorySlots[i].GetComponent<SpriteRenderer>().sprite = sprites[7] as Sprite;
                items[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                inventorySlots[i].GetComponent<SpriteRenderer>().sprite = sprites[6] as Sprite;
                items[i].GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            }
        }

    }
}

