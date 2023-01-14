using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWet : MonoBehaviour
{
    TileManager tileManager;

    [SerializeField] float maxWetTime = 10f; //In seconds
    [SerializeField] float tileCheckTimer = 0.2f;

    [SerializeField] GameObject soakAnimation;
    [SerializeField] GameObject spriteHolder;
    
    float wetTime;
    bool wetterIsOn = false;

    [Tooltip("The height the Player Sprite wil be lowered to when entering lake.")]
    [SerializeField] float bathHeight = -0.4f;

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

        soakAnimation.SetActive(true);

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

        soakAnimation.SetActive(false);
    }



    public void GetInLake()
    {
        // Lower character sprite "into" the lake
        MoveCharacterSprite(bathHeight);
    }
    public void GetOutOfLake()
    {
        // Rise character sprite out of the lake
        MoveCharacterSprite(0f);
    }

    private void MoveCharacterSprite(float toHeight)
    {
        Vector3 pos = spriteHolder.transform.localPosition;
        spriteHolder.transform.localPosition = new Vector3(pos.x, toHeight);
    }
}
