using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//You can plant plants on the fertile ground. Every fertile ground should have a Plant class object that grows.
//Basic constructor plants a random plant.

public class Plant : Item
{
    public const int maxLevel = 2; //Max level of the plant / when plant can be harvested.
    public Enums.FruitType Type { get; private set; }

    public int level {get; private set;}//Level of the plant. If big enough, plant can be harvested.

    public bool isFertilized = false;

    public Plant()
    {
        level = 0; //Level is 0 at first.

        //Randomize plant type
        int randType = Random.Range(0, 4);
        switch (randType)
        {
            case 0:
                Type = Enums.FruitType.Fruit1;
                break;
            case 1:
                Type = Enums.FruitType.Fruit2;
                break;
            case 2:
                Type = Enums.FruitType.Fruit3;
                break;
            case 3:
                Type = Enums.FruitType.Fruit4;
                break;
            default:
                Debug.Log("There is no plant type with int " + randType);
                break;
        }

    }

    public void LevelUp()
    {
        if(level < maxLevel)
        {
            level++;
        }
    }

    public void FullLevel()
    {
        level = maxLevel;
    }
}
