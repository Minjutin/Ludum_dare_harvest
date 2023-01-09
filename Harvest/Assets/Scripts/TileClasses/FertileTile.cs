using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tile where you can plant and things are spawning. Has a fertility level. You can check if it contains an item, set and delete an item.

public class FertileTile : TileDaddy
{
    public Enums.Fertility fertilityLevel { get; private set; }

    public Item item { get; private set; }

    public GameObject itemGO;

    public FertileTile(Enums.Fertility fertility)
    {
        fertilityLevel = fertility;
    } //Create a fertile tile with certain amount of fertility (1-3)

    #region Edit item
    public bool HasItem()
    {
        if(item != null)
        {
            return true;
        }
        return false;
    } //Return true if tile already contains an item.

    public void SetItem(Item setItem)
    {
        if (HasItem())
        {
            Debug.LogWarning("Tile already contains an item, but you're trying to give it a new item.");
        }

        if(setItem is Plant || setItem is Seed || setItem is Manure)
        {
            item = setItem;


            if (setItem is Plant)
            {
                // Spawn a Plant GameObject
            }
        } else
        {
            Debug.Log("Tile can't hold this type of item.");        }
    } //Set a new item. 

    public void RemoveItem()
    {
        item = null;
    } //Remove an existing item.

    public void SpawnSeed(GameObject seed)
    {
        // Get the Seed reference
        itemGO = seed;

        // Needs to be Destroyed by something else,
        // as FertileTile etc. aren't MonoBehaviours
    }

    public void SpawnPlant(GameObject plant)
    {
        // Get the Plant Reference
        itemGO = plant;

        // Give Plant a reference to this tile
        itemGO.GetComponent<PlantGrowth>().StartGrow(this);
    }
    #endregion
}
