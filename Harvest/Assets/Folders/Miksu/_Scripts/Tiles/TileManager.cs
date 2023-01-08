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
        fertile_2, fertile_1, fertile_0, empty, water, notSet
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

        // FERTILE
        if (type == tileType.fertile_0)
        {
            FertileTile fTile = new FertileTile(Enums.Fertility.F0);
            tileArray[x, y] = fTile;
            painter.PaintTileAt(x, y, fTile);
        }
        else if (type == tileType.fertile_1)
        {
            FertileTile fTile = new FertileTile(Enums.Fertility.F1);
            tileArray[x, y] = fTile;
            painter.PaintTileAt(x, y, fTile);
        }
        else if (type == tileType.fertile_2)
        {
            FertileTile fTile = new FertileTile(Enums.Fertility.F2);
            tileArray[x, y] = fTile;
            painter.PaintTileAt(x, y, fTile);
        }

        // WATER
        else if (type == tileType.water)
        {
            WaterTile wTile = new WaterTile();
            tileArray[x, y] = wTile;
            painter.PaintTileAt(x, y, wTile);
        }
    }

    private void SetRandomTileAt(int x, int y)
    {
        // Set a random tile in memory
        int random = Random.Range(0, 4);

        // FERTILE
        if (random == 0)
        {
            // Create the new Tile
            FertileTile fTile = new FertileTile(Enums.Fertility.F0);
            // Set the tile to memory
            tileArray[x, y] = fTile;
            // Paint
            painter.PaintTileAt(x, y, fTile);
        }
        else if (random == 1)
        {
            FertileTile fTile = new FertileTile(Enums.Fertility.F1);
            tileArray[x, y] = fTile;
            painter.PaintTileAt(x, y, fTile);
        }
        else if (random == 2)
        {
            FertileTile fTile = new FertileTile(Enums.Fertility.F2);
            tileArray[x, y] = fTile;
            painter.PaintTileAt(x, y, fTile);
        }

        // WATER
        else if (random == 3)
        {
            WaterTile wTile = new WaterTile();
            tileArray[x, y] = wTile;
            painter.PaintTileAt(x, y, wTile);
        }
    }
    #endregion


    #region Tile INTERACTION
    public TileDaddy GetTilePlayerIsOn(Vector3 playerPos)
    {
        int x = Mathf.RoundToInt(playerPos.x);
        int y = Mathf.RoundToInt(playerPos.z); // translates Z pos to Y

        Debug.Log("Player Real  Pos: " + playerPos);
        Debug.Log("Player ROUND Pos: " + x + " " + y);

        if (tileArray[x, y] is FertileTile)
        {
            Debug.Log("Is fertile tile");

            FertileTile fTile = tileArray[x, y] as FertileTile;
            if (fTile.fertilityLevel == Enums.Fertility.F0)
            {
                Debug.Log("... but it isn't very fertile");
            }
        }
        else { Debug.Log("It ISNT fertile"); }

        return tileArray[x, y];

    }
    #endregion
}
