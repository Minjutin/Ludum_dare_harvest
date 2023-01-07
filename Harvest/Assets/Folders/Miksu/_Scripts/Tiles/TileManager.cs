using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    #region PROPERTIES

    private TilePainter painter;

    public int gridRows = 30;
    public int gridColumns = 30;


    // TEMPORARY
    public enum tileType
    {
        f3, f2, f1, empty, water, path
    }

    tileType[,] gridArray;
    // ***

    #endregion

    #region SETUP
    private void Awake()
    {
        painter = GetComponent<TilePainter>();

        // Set up the tile Array
        SetUpTileArray();

        // Print the array
    }

    private void SetUpTileArray()
    {
        // Initialize the Array
        gridArray = new tileType[gridRows, gridColumns];

        // Set the tile types

    }

    private void RandomizeTiles()
    {
        for (int x = 0; x < gridRows; x++)
        {
            for (int y = 0; y < gridColumns; y++)
            {
                // Set Tile Type
                gridArray[x, y] = SetRandomTileAt(x, y);
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
                //painter.SetRandomTileAt
            }
        }
    }
    #endregion

    #region Tile CHANGING
    private tileType SetRandomTileAt(int x, int y)
    {
        // Randomize the tile
        return tileType.f3;
    }
    #endregion

    #region Tile FUNCTIONALITY

    #endregion
}
