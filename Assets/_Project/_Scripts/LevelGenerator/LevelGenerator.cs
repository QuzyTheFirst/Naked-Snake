using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Texture2D _map;

    [SerializeField] private ColorToPrefab[] colorMappings;
    
    [SerializeField] private float _distanceBetweenTiles = 1.2f;
    
    private List<GridTile> _gridTiles;
    private List<Tile> _tiles;

    public float DistanceBetweenTiles => _distanceBetweenTiles;
    public Vector2Int GridSize => new Vector2Int(_map.width, _map.height);
    public IReadOnlyList<GridTile> GridTiles => _gridTiles;
    

    public void GenerateLevel()
    {
        _gridTiles = new List<GridTile>();
        _tiles = new List<Tile>();

        for (int x = 0; x < _map.width; x++)
        {
            for (int y = 0; y < _map.height; y++)
            {
                GridTile tile = GenerateTile(x, y);
                
                if (tile == null)
                    continue;
                
                _gridTiles.Add(tile);
            }
        }
    }

    private GridTile GenerateTile(int x, int y)
    {
        Color pixelColor = _map.GetPixel(x, y);
        
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
