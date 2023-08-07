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

    private GridController _gridController;

    public float DistanceBetweenTiles => _distanceBetweenTiles;
    
    public void Initialize(GridController gridController)
    {
        _gridController = gridController;
    }

    public void GenerateLevel()
    {
        List<GridTile> gridTiles = new List<GridTile>();

        for (int x = 0; x < _map.width; x++)
        {
            for (int y = 0; y < _map.height; y++)
            {
                GridTile tile = GenerateTile(x, y);
                
                if(tile != null) gridTiles.Add(tile);
            }
        }
        
        _gridController.SetGrid(_map.width, _map.height, gridTiles);
    }

    private GridTile GenerateTile(int x, int y)
    {
        Color pixelColor = _map.GetPixel(x, y);
        
        if (pixelColor.a == 0)
            return null;

        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            Debug.Log(colorMapping.color + " | " + pixelColor);
            
            if (colorMapping.color.Equals(pixelColor))
            {
                Vector3 spawnPosition = new Vector3(x, 0, y);
                GridTile tile = Instantiate(colorMapping.prefab, spawnPosition * _distanceBetweenTiles, Quaternion.identity, transform);
                tile.GridPosition = new Vector2Int(x, y);
                return tile;
            }
        }

        return null;
    }
}
