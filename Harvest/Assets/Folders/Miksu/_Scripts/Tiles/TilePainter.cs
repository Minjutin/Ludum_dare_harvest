using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePainter : MonoBehaviour
{
    // Handles the TileGrids graphical side

    #region Properties

    public Tilemap tilemap;

    [Header("Fertile Tiles")]
    public List<Tile> fertile_2 = new List<Tile>();
    public List<Tile> fertile_1 = new List<Tile>();
    public List<Tile> fertile_0 = new List<Tile>();

    [Header("Water Tiles")]
    public List<Tile> waterTile = new List<Tile>();

    [Header("Sacrifice Tiles")]
    public List<Tile> sacrificeTiles = new List<Tile>();

    [ContextMenu("Paint")]

    #endregion

    #region PAINTING

    public void PaintTileAt(int x, int y, TileDaddy tile)
    {
        SetRandomGraphicFor(x, y, tile);
    }

    // Graphically Paints the Tile to Tilemap
    private void Paint(int x, int y, Tile tile)
    {
        Vector3Int pos = new Vector3Int(x, y);

        // Paint the tile
        tilemap.SetTile(pos, tile);
    }

    #endregion

    public void SetRandomGraphicFor(int x, int y, TileDaddy tile)
    {
        Vector3Int pos = new Vector3Int(x, y);

        // FERTILE
        FertileTile fTile = tile as FertileTile;
        if (fTile is FertileTile)
        {
            if (fTile.fertilityLevel == Enums.Fertility.F0)
            {
                // No fertility
                Paint(x, y, fertile_0[Random.Range(0, fertile_0.Count)]);
            }
            else if (fTile.fertilityLevel == Enums.Fertility.F1)
            {
                // Fertility level 1
                Paint(x, y, fertile_1[Random.Range(0, fertile_1.Count)]);
            }
            else if (fTile.fertilityLevel == Enums.Fertility.F2)
            {
                // Fertility level 2
                Paint(x, y, fertile_2[Random.Range(0, fertile_2.Count)]);
            }
        }

        // WATER
        WaterTile wTile = tile as WaterTile;
        if(wTile is WaterTile)
        {
            if (waterTile.Count >= 1)
            { Paint(x, y, waterTile[0]); }
            else { Debug.LogWarning("No WaterTiles assigned at TilePainter"); }
        }

        // SACRIFICE
        SacrificeTile sTile = tile as SacrificeTile;
        if (sTile is SacrificeTile)
        {
            if (sacrificeTiles.Count >= 1)
            { Paint(x, y, sacrificeTiles[0]); } // Might want to turn this off?
            else { Debug.LogWarning("No SacrificeTiles assigned at TilePainter"); }
        }

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

        // FERTILE
        if (fertile_2.Contains(tile))
        {
            return TileManager.tileType.fertile_2;
        }
        else if (fertile_1.Contains(tile))
        {
            return TileManager.tileType.fertile_1;
        }
        else if (fertile_0.Contains(tile))
        {
            return TileManager.tileType.fertile_0;
        }

        // WATER
        else if (waterTile.Contains(tile))
        {
            return TileManager.tileType.water;
        }

        // SACRIFICE
        else if (sacrificeTiles.Contains(tile))
        {
            return TileManager.tileType.sacrifice;
        }

        //Debug.Log("Tile at (" + x + "," + y + ") has invalid type of: " + tile.name);
        return TileManager.tileType.empty;

    }

}
