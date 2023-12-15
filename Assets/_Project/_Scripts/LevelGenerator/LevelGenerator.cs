using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private static float _distanceBetweenTiles = 1.2f;

    private static int _generatedMapWidth;
    private static int _generatedMapHeight;

    [SerializeField] private ColorToPrefab[] colorMappings;

    private List<GridTile> _gridTiles;
    private List<Tile> _tiles;
    
    public static float DistanceBetweenTiles => _distanceBetweenTiles;
    public static Vector2Int GeneratedGridSize => new Vector2Int(_generatedMapWidth, _generatedMapHeight);
    public IReadOnlyList<GridTile> GridTiles => _gridTiles;
    

    public void GenerateLevel(Texture2D map)
    {
        ClearLevel();

        _generatedMapWidth = map.width;
        _generatedMapHeight = map.height;

        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GridTile tile = GenerateTile(x, y, map);
                
                if (tile == null)
                    continue;
                
                _gridTiles.Add(tile);
            }
        }
    }

    private void ClearLevel()
    {
        _gridTiles = new List<GridTile>();

        if (_tiles != null)
        {
            foreach (Tile tile in _tiles)
            {
                Destroy(tile.gameObject);
            }
        }

        _tiles = new List<Tile>();

        _generatedMapHeight = 0;
        _generatedMapWidth = 0;
    }
    
    private GridTile GenerateTile(int x, int y, Texture2D map)
    {
        Color pixelColor = map.GetPixel(x, y);
        
        if (pixelColor.a == 0)
            return null;

        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                Vector3 spawnPosition = new Vector3(x, 0, y);
                
                Tile tile = Instantiate(colorMapping.prefab, spawnPosition * _distanceBetweenTiles, Quaternion.identity, transform);
                _tiles.Add(tile);
                
                GridTile gridTile = new GridTile();
                gridTile.SetItem(x, y, tile.CurrentTileType);
                return gridTile;
            }
        }

        return null;
    }
}
