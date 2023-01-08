using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    private float phaseMove;

    public FertileTile tile; //get the tile
    Plant plant; //get this from the tile the game object is.

    private void Start()
    {
        if(tile.item is Plant)
        {
            plant = tile.item as Plant;
        }

        StartCoroutine(Growth());
    }

    IEnumerator Growth()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
        }
    }

}
