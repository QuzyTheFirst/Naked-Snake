using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorMapRulesChecker
{
    private void GetAmountOfTiles(Texture2D map, out int obstacleTiles, out int walkableTiles, out int spawnPointTiles,
        out int emptyTiles)
    {
        obstacleTiles = 0;
        walkableTiles = 0;
        spawnPointTiles = 0;
        emptyTiles = 0;
        
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                if (map.GetPixel(x, y) == Color.red)
                {
                    obstacleTiles++;
                }
                else if (map.GetPixel(x, y) == Color.white)
                {
                    walkableTiles++;
                }
                else if (map.GetPixel(x, y) == Color.green)
                {
                    spawnPointTiles++;
                }
                else
                {
                    emptyTiles++;
                }
            }
        }
    }
    
    public bool CheckMapForRules(Texture2D map)
    {
        GetAmountOfTiles(map, out int obstacleTiles, out int walkableTiles, out int spawnPointTiles, out int emptyTiles);

        if (walkableTiles < 5)
        {
            throw new Exception("Place at least 5 walkable tiles!");
        }

        if (spawnPointTiles == 0)
        {
            throw new Exception("Place at least 1 spawn point tile!");
        }
        
        return true;
    }
}
