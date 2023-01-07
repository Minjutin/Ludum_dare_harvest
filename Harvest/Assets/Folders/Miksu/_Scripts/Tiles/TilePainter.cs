using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePainter : MonoBehaviour
{
    [SerializeField]
    public UnityEngine.Tilemaps.Tile fertile3;
    public UnityEngine.Tilemaps.Tile fertile2;
    public UnityEngine.Tilemaps.Tile fertile1;

    private List<Tile> tiles = new List<Tile>();

    public Tilemap tilemap;

    public int rows = 5;
    public int columns = 5;

    [ContextMenu("Paint")]

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

    private void SetRandomTileAt(int x, int y)
    {
        Vector3Int pos = new Vector3Int(x, y);

        Tile random = tiles[Random.Range(0, tiles.Count)];

        if (tilemap.GetTile(pos) == null)
        {
            tilemap.SetTile(pos, random);

        }
    }

    private void Start()
    {
        tiles.Add(fertile3);
        tiles.Add(fertile2);
        tiles.Add(fertile1);

        PaintTiles();
    }
}
