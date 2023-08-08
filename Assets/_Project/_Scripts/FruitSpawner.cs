using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] private Transform _fruitPf;
    [SerializeField] private int _amountOfTriesToFindRightTile = 5;

    private GridsManipulator _gridsManipulator;
    private float _distanceBetweenTiles;

    public static event EventHandler OnFruitEaten;
    
    public void Initialize(GridsManipulator gridsManipulator , float distanceBetweenTiles)
    {
        _gridsManipulator = gridsManipulator;
        _distanceBetweenTiles = distanceBetweenTiles;
    }
    
    public void SpawnFruit()
    {
        GridTile tile = null;
        
        for (int i = 0; i < _amountOfTriesToFindRightTile; i++)
        {
            Debug.Log("Amount of tries " + i);
            tile = _gridsManipulator.GridTiles.GetRandomWalkableTile();
            
            if (_gridsManipulator.CheckTileForSnake(tile.X, tile.Y) == false)
                break;
        }

        if (tile == null) 
            throw new Exception("Walkable Tile Wasn't found");
        
        Vector3 spawnPos = new Vector3(tile.X, 0, tile.Y) * _distanceBetweenTiles + Vector3.up;
        Transform fruit = Instantiate(_fruitPf, spawnPos, Quaternion.identity);

        GridTransformItem item = new GridTransformItem();
        item.SetItem(tile.X, tile.Y, fruit);
        _gridsManipulator.GridItems.TrySetTile(item);
    }

    private void OnEnable()
    {
        SnakeMovement.OnSnakeHitFruit += SnakeMovement_OnSnakeHitFruit;
    }

    private void SnakeMovement_OnSnakeHitFruit(object sender, Vector2Int fruitPos)
    {
        GridTransformItem item = _gridsManipulator.GridItems.TryGetTile(fruitPos.x, fruitPos.y);

        _gridsManipulator.GridItems.TryClearTile(fruitPos.x, fruitPos.y);
        Destroy(item.Value.gameObject);
        OnFruitEaten?.Invoke(this, EventArgs.Empty);
        
        SpawnFruit();
    }

    private void OnDisable()
    {
        SnakeMovement.OnSnakeHitFruit -= SnakeMovement_OnSnakeHitFruit;
    }
}
