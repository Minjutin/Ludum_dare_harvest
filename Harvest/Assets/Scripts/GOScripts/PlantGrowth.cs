using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowth : MonoBehaviour
{

    public FertileTile tile; //get the tile
    Plant plant; //get this from the tile the game object is.

    float growTime;

    Constants constants;

    bool isWatered = false;

    public void StartGrow(FertileTile tiley)
    {
        tile = tiley;
        constants = FindObjectOfType<Constants>();
        growTime = constants.growSpeed;

        //If tile has lower fertility, change the grow speed.
        if(tile.fertilityLevel == Enums.Fertility.F1)
        {
            growTime = growTime / constants.f1Multiple;
        }

        if (tile.item is Plant)
        {
            plant = tile.item as Plant;
        }
        else
            Debug.LogError("Tile does not contain Plant item even though it has Plant gameobject");

        StartCoroutine(Growth());
    }

    //When the plant has been watered, start the coroutine.
    public void Watered()
    {
        if (!isWatered) //Water the plant only if the plant is not already watered.
        {
            isWatered = true;
            StartCoroutine(Growth());
        }

    }

    //Coroutine for plant growing.
    IEnumerator Growth()
    {
        float growTimeLeft = growTime;
        while (true)
        {                      
            Debug.Log("Time left" +growTimeLeft);
            if (growTimeLeft <= (growTime/Plant.maxLevel) && plant.level == 0)
            {
                plant.LevelUp(); //Level up to level 2
                //TODO change the sprite

                Debug.Log("Level now" + plant.level);
            }
            
            if (growTimeLeft <= 0)
            {
                plant.LevelUp();
                Debug.Log("Level now" +plant.level);            
                //TODO change the sprite
                break;
            }

            yield return new WaitForSeconds(1f);

            growTimeLeft--;
        }
        
    }

}
