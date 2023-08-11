using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] private Transform _fruitPf;
    [SerializeField] private int _amountOfTriesToFindRightTile = 5;

    private GridsManipulator _gridsManipulator;
    private float _distanceBetweenTiles;

    public void Initialize(GridsManipulator gridsManipulator , float distanceBetweenTiles)
    {
        _gridsManipulator = gridsManipulator;
        _distanceBetweenTiles = distanceBetweenTiles;

    }
    
    public void SpawnFruit()
    {
        List<GridTile> tiles = _gridsManipulator.FindAllWalkableTilesWithoutSnakesAndFruits();
        if (tiles.Count == 0)
            return;
        
        int randomNumber = Random.Range(0, tiles.Count);
        GridTile tile = tiles[randomNumber];

        if (tile == null) 
            throw new Exception("Walkable Tile Wasn't found");
        
        Vector3 spawnPos = new Vector3(tile.X, 0, tile.Y) * _distanceBetweenTiles + Vector3.up;
        Transform fruit = Instantiate(_fruitPf, spawnPos, Quaternion.identity,transform);

        GridTransformItem item = new GridTransformItem();
        item.SetItem(tile.X, tile.Y, fruit);
        _gridsManipulator.GridItems.TrySetTile(item);
    }

    public void DeleteAllFruits()
    {
        foreach (Transform fruit in transform)
        {
            Destroy(fruit.gameObject);
        }
    }
    
    private void OnEnable()
    {
        FruitsCollector.FruitSuccessfullyEaten += FruitsCollectorOnFruitSuccessfullyEaten;
    }

    private void FruitsCollectorOnFruitSuccessfullyEaten(object sender, Vector2Int fruitPos)
    {
        GridTransformItem gridItem = _gridsManipulator.GridItems.TryGetTile(fruitPos.x, fruitPos.y);
        
        if (gridItem == null)
            return;
        
        Transform item = gridItem.Value;
        
        if (_gridsManipulator.GridItems.TryClearTile(fruitPos.x, fruitPos.y) && item != null)
        {
            Destroy(item.gameObject);
        }

        SpawnFruit();
    }


    private void OnDisable()
    {
        FruitsCollector.FruitSuccessfullyEaten -= FruitsCollectorOnFruitSuccessfullyEaten;
    }
}
