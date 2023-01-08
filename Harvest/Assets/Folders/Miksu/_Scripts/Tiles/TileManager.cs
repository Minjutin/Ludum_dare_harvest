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
        fertile_3, fertile_2, fertile_1, empty, water, notSet
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
                    // There is already a tile, do nothing
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

    public void SetTile()
    {

    }

    private void SetRandomTileAt(int x, int y)
    {
        painter.SetRandomTileAt(x, y);
    }
    #endregion


    #region Tile INTERACTION

    #endregion
}
