using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePainter : MonoBehaviour
{
    // Handles the TileGrids graphical side

    #region Properties

    public List<Tile> fullFertile = new List<Tile>();
    public List<Tile> semiFertile = new List<Tile>();
    public List<Tile> littleFertile = new List<Tile>();

    private List<Tile> tiles = new List<Tile>();

    public Tilemap tilemap;

    public int rows = 5;
    public int columns = 5;

    [ContextMenu("Paint")]

    #endregion

    private void PaintTiles()
    {
        //tilemap.SetTile(pos, fertile2);

        for (int y = 0; y < columns; y++)
        {
            for (int x = 0; x < rows; x++)
            {
                SetRandomTileAt(x, y);
            }
        }

        //Debug.Log(tilemap.GetTile(pos).name);
    }

    public void SetRandomTileAt(int x, int y)
    {
        Vector3Int pos = new Vector3Int(x, y);

        Tile random = tiles[Random.Range(0, tiles.Count)];

        if (tilemap.GetTile(pos) == null)
        {
            tilemap.SetTile(pos, random);
        }
    }

    public void SetRandomGraphicFor(int x, int y, TileDaddy tileType)
    {
        Vector3Int pos = new Vector3Int(x, y);

        Tile random;
        random = littleFertile[0]; // Default

        if (tileType is FertileTile)
        {
            // Choose random Fertility tile
            random = fullFertile[Random.Range(0, fullFertile.Count)];
        }
        else if (tileType is EmptyTile)
        {

        }
        else if (tileType is FloorTile)
        {

        }


            tilemap.SetTile(pos, random);
    }

    public TileManager.tileType ReadTileGraphics(int x, int y)
    {
        Vector3Int pos = new Vector3Int(x, y);

        Tile tile = tilemap.GetTile(pos) as Tile;

        // Check if there is a tile set
        if (tile == null)
        {
            // No tile set, return null
            return TileManager.tileType.notSet;
        }

        // If there is, check type
        if (fullFertile.Contains(tile))
        {
            return TileManager.tileType.fertile_3;
        }
        else if (semiFertile.Contains(tile))
        {
            return TileManager.tileType.fertile_2;
        }
        else if (littleFertile.Contains(tile))
        {
            return TileManager.tileType.fertile_1;
        }

        return TileManager.tileType.empty;

    }

    private void Awake()
    {
        AddAllTiles();

        //PaintTiles();
    }

    private void AddAllTiles()
    {
        foreach(Tile tile in fullFertile)
        {
            tiles.Add(tile);
        }
        foreach(Tile tile in semiFertile)
        {
            tiles.Add(tile);
        }
        foreach(Tile tile in littleFertile)
        {
            tiles.Add(tile);
        }

    }
}
