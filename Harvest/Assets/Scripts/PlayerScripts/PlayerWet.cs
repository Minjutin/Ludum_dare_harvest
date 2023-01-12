using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWet : MonoBehaviour
{
    [SerializeField] float maxWetTime = 10f; //In seconds
    [SerializeField] float tileCheckTimer = 0.2f;
    
    float wetTime;
    bool wetterIsOn = false;

    TileManager tileManager;

    private void Start()
    {
        // Get TileManager
        tileManager = FindObjectOfType<TileManager>();
    }

    public void GetWet()
    {
        wetTime = maxWetTime;
        if (!wetterIsOn)
        {
            StartCoroutine(Wetter());
        }
    }

    IEnumerator Wetter()
    {
        wetterIsOn = true;

        while (true)
        {
            yield return new WaitForSeconds(tileCheckTimer);

            TileDaddy currentTile = tileManager.GetTileCreatureIsOn(this.transform.position);

            //If tile we are standing is fertile tile, check if it has a plant.
            if (currentTile is FertileTile)
            {
                FertileTile ftile = currentTile as FertileTile;
                //If plant on fertile tile
                if (ftile.HasItem())
                {
                    if (ftile.item is Plant) //If the item is plant
                    {
                        PlantGrowth grow = ftile.itemGO.GetComponent<PlantGrowth>();
                        grow.Water();
                    }
                }

            }

            if (wetTime < 0)
                { break; }

            wetTime = wetTime - tileCheckTimer;
        }

        wetterIsOn = false;
    }
}
