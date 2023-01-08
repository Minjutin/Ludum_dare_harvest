using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fruit is an inventory item you get from collecting the grown plant and can offer to your god. 

public class Fruit: InventoryItem
{
    Enums.FruitType Type;

    public Fruit(Enums.FruitType fType)
    {
        Type = fType;
    }
}
