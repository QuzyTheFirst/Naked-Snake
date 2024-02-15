using System.Collections.Generic;
using UnityEngine;

public class LevelGridGenerator : MonoBehaviour
{
    [SerializeField] private ColorToPrefab[] colorMappings;
    
    public MapTilesGrid GenerateLevel(Texture2D map, float cellSize, Vector3 originPosition)
    {
        MapTilesGrid mapTilesGrid = new MapTilesGrid(map.width, map.height, cellSize, originPosition);
        
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                SetTile(mapTilesGrid, x, y, map);
            }
        }

        return mapTilesGrid;
    }
    
    private void SetTile(MapTilesGrid grid, int x, int y, Texture2D map)
    {
        Color pixelColor = map.GetPixel(x, y);
        if (pixelColor.a == 0)
            return;

        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                grid.SetMapTile(x, y, colorMapping.tileType);
            }
        }
    }
}
