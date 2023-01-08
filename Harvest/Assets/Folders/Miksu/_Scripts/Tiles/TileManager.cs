using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TilePainter))]
public class TileManager : MonoBehaviour
{
    // TileManager handles with TileGrids data & functionality side

    #region PROPERTIES

    private TilePainter painter;

    public int gridRows = 30;
    public int gridColumns = 30;

    // 2D Tile Array
    TileDaddy[,] tileArray;

    // TileDaddy
    //
    //  -> FertileTile
    //  -> EmptyTile
    //  -> FloorTile

    public enum tileType
    {
        fertile_2, fertile_1, fertile_0, empty, water, sacrifice, notSet
    }



    #endregion

    #region SETUP
    private void Awake()
    {
        painter = GetComponent<TilePainter>();
    }

    private void Start()
    {
        // Set up the tile Array
        SetUpTileArray();

        // Print the array
        PrintTiles();
    }

    private void SetUpTileArray()
    {
        // Initialize the Array
        tileArray = new TileDaddy[gridRows, gridColumns];

        // Set the tile types
        for (int x = 0; x < gridRows; x++)
        {
            for (int y = 0; y < gridColumns; y++)
            {
                // Check if there already is a tile
                tileType type = painter.ReadTileGraphics(x, y);
                if (type != tileType.notSet)
                {
                    // There is already a tile
                    // -> Get Tile Type
                    // --> Save the Tile Type into the TileArray
                    SetNewTileAt(type, x, y);
                }
                else
                {
                    // It's empty
                    // -> Put a random tile there
                    SetRandomTileAt(x, y);
                }
            }
        }
    }

    private void RandomizeEmptyTiles()
    {
        for (int x = 0; x < gridRows; x++)
        {
            for (int y = 0; y < gridColumns; y++)
            {
                // Set Tile Type
                //tileArray[x, y] = SetRandomTileAt(x, y); TODO !!!!!!
            }
        }
    }
    #endregion

    #region Tile PRINTING
    private void PrintTiles()
    {
        for (int x = 0; x < gridRows; x++)
        {
            for (int y = 0; y < gridColumns; y++)
            {
                // Set Tile Type
                //painter.SetRandomTileAt       TODO
            }
        }
    }
    #endregion

    #region Tile CHANGING
    public TileDaddy ReadTile(int xPos, int yPos)
    {
        // Get the Tile at x,y location
        return tileArray[xPos, yPos];
    }

    public void SetNewTileAt(tileType type, int x, int y)
    {
        Vector3 pos = new Vector3(x, 0, y);


        // FERTILE
        if (type == tileType.fertile_0)
        {
            FertileTile fTile = new FertileTile(Enums.Fertility.F0);
            fTile.position = pos;
            tileArray[x, y] = fTile;
            painter.PaintTileAt(x, y, fTile);
        }
        else if (type == tileType.fertile_1)
        {
            FertileTile fTile = new FertileTile(Enums.Fertility.F1);
            fTile.position = pos;
            tileArray[x, y] = fTile;
            painter.PaintTileAt(x, y, fTile);
        }
        else if (type == tileType.fertile_2)
        {
            FertileTile fTile = new FertileTile(Enums.Fertility.F2);
            fTile.position = pos;
            tileArray[x, y] = fTile;
            painter.PaintTileAt(x, y, fTile);
        }

        // WATER
        else if (type == tileType.water)
        {
            WaterTile wTile = new WaterTile();
            wTile.position = pos;
            tileArray[x, y] = wTile;
            //painter.PaintTileAt(x, y, wTile); // Already has a tile
        }

        // SACRIFICE
        else if (type == tileType.sacrifice)
        {
            SacrificeTile sTile = new SacrificeTile(Enums.God.God1);    // TODO: Different Gods
            sTile.position = pos;
            tileArray[x, y] = sTile;
            //painter.PaintTileAt(x, y, sTile); // Already has a tile
        }
    }

    private void SetRandomTileAt(int x, int y)
    {
        Vector3 pos = new Vector3(x, 0, y);

        // Set a random tile in memory
        int random = Random.Range(0, 3);

        // FERTILE
        if (random == 0)
        {
            // Create the new Tile
            FertileTile fTile = new FertileTile(Enums.Fertility.F0);
            // Set the position for the tile
            fTile.position = pos;
            // Set the tile to memory
            tileArray[x, y] = fTile;
            // Paint
            painter.PaintTileAt(x, y, fTile);
        }
        else if (random == 1)
        {
            FertileTile fTile = new FertileTile(Enums.Fertility.F1);
            tileArray[x, y] = fTile;
            fTile.position = pos;
            painter.PaintTileAt(x, y, fTile);
        }
        else if (random == 2)
        {
            FertileTile fTile = new FertileTile(Enums.Fertility.F2);
            tileArray[x, y] = fTile;
            fTile.position = pos;
            painter.PaintTileAt(x, y, fTile);
        }

        // WATER
        else if (random == 3)
        {
            WaterTile wTile = new WaterTile();
            wTile.position = pos;
            tileArray[x, y] = wTile;
            painter.PaintTileAt(x, y, wTile);
        }
    }
    #endregion


    #region Tile INTERACTION
    public TileDaddy GetTilePlayerIsOn(Vector3 playerPos)
    {
        // ATTENTION!
        // Grid needs to be positioned -0.5 from origo in both X and Z axis
        // for the positioning to work properly

        Debug.DrawRay(playerPos, Vector3.up, Color.red, 1f);

        int x = Mathf.RoundToInt(playerPos.x);
        int y = Mathf.RoundToInt(playerPos.z); // translates Z pos to Y

        //Debug.Log("Player Real  Pos: " + playerPos);
        //Debug.Log("Player ROUND Pos: " + x + " " + y);
        //if (tileArray[x, y] is FertileTile)
        //{
        //    FertileTile fTile = tileArray[x, y] as FertileTile;
        //    if (fTile.fertilityLevel == Enums.Fertility.F0)
        //    {
        //        Debug.Log("Barren");
        //    }
        //    else if (fTile.fertilityLevel == Enums.Fertility.F1) { Debug.Log("Fertile"); }
        //    else if (fTile.fertilityLevel == Enums.Fertility.F2) { Debug.Log("VERY Fertile"); }
        //}
        //else if (tileArray[x, y] is WaterTile)
        //{
        //    Debug.Log("It's water.");
        //}
        //if (tileArray[x,y] is SacrificeTile)
        //{
        //    Debug.Log("SACRIFICE");
        //}

        // Return TileDaddy type
        return tileArray[x, y];

    }

    #endregion

    public TileDaddy GetTileAt(int x, int y)
    {
        return tileArray[x, y];
    }
}
