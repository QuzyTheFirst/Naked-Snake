using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] private Fruit _fruitPf;

    private GridController _gridController;
    private float _distanceBetweenTiles;
    
    public void Initialize(GridController gridController, float distanceBetweenTiles)
    {
        _gridController = gridController;
        _distanceBetweenTiles = distanceBetweenTiles;
    }
    
    public void SpawnFruit()
    {
        GridTile tile = _gridController.GetRandomWalkableTile();
        Vector3 spawnPos = new Vector3(tile.GridPosition.x, 0, tile.GridPosition.y) * _distanceBetweenTiles + Vector3.up;
        Instantiate(_fruitPf, spawnPos, Quaternion.identity);
    }

    private void OnEnable()
    {
        Fruit.OnPlayerTriggerEnter+=FruitOnOnPlayerTriggerEnter;
    }

    private void FruitOnOnPlayerTriggerEnter(object sender, EventArgs e)
    {
        Fruit fruit = sender as Fruit;
        Destroy(fruit.gameObject);
        SpawnFruit();
    }

    private void OnDisable()
    {
        Fruit.OnPlayerTriggerEnter-=FruitOnOnPlayerTriggerEnter;
    }
}
