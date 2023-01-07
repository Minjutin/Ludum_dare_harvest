using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//You can plant plants on the fertile ground. Every fertile ground should have a Plant class object that grows

public class Plant
{
    const int maxLevel = 3; //Max level of the plant / when plant can be harvested.

    Enums.FruitType Type;
    int level = 0; //Level of the plant. If big enough, plant can be

    void TryToHarvest(GameObject player)
    {
        if (level >= maxLevel)
        {

        }
    }
}
