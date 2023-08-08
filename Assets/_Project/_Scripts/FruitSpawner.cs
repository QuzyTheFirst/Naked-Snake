using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] private Transform _fruitPf;

    private GridItems _gridItems;
    private GridTiles _gridTiles;
    private float _distanceBetweenTiles;
    
    public void Initialize(GridItems gridItems, GridTiles gridTiles , float distanceBetweenTiles)
    {
        _gridItems = gridItems;
        _gridTiles = gridTiles;
        _distanceBetweenTiles = distanceBetweenTiles;
    }
    
    public void SpawnFruit()
    {
        GridTile tile = _gridTiles.GetRandomWalkableTile();
        
        if (tile == null) 
            throw new Exception("Walkable Tile Wasn't found");
        
        Vector3 spawnPos = new Vector3(tile.X, 0, tile.Y) * _distanceBetweenTiles + Vector3.up;
        Instantiate(_fruitPf, spawnPos, Quaternion.identity);

        GridIntItem item = new GridIntItem();
        item.SetItem(tile.X, tile.Y, 1);
        _gridItems.TrySetTile(item);
    }
}
