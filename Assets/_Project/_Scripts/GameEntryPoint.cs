using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameEntryPoint : MonoBehaviour
{
    [SerializeField] private SceneController _sceneController;
    [SerializeField] private InGameUI _inGameUI;
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private GameStateController _gameStateController;
    [SerializeField] private FruitSpawner _fruitSpawner;

    [SerializeField] private SnakeMovement snake;

    private GridTiles _gridTiles;
    private GridItems _gridItems;
    
    private void Start()
    {
        // Initializing Variables
        _gridTiles = new GridTiles();
        _gridItems = new GridItems();
        
        _inGameUI.Initialize(_sceneController, _gameStateController);
        _gameStateController.Initialize(_inGameUI);
        _fruitSpawner.Initialize(_gridItems, _gridTiles, _levelGenerator.DistanceBetweenTiles);
        
        // Setting Up Level
        _levelGenerator.GenerateLevel();
        
        _gridTiles.ResetGrid(_levelGenerator.GridSize.x, _levelGenerator.GridSize.y);
        _gridTiles.SetGridObjects(_levelGenerator.GridTiles);
        
        _gridItems.ResetGrid(_levelGenerator.GridSize.x, _levelGenerator.GridSize.y);
        
        _fruitSpawner.SpawnFruit();
        
        // Spawning Snake
        GridTile tile = _gridTiles.GetRandomSpawnTile();
        Vector3 spawnPos = new Vector3(tile.X, 0, tile.Y) * _levelGenerator.DistanceBetweenTiles + Vector3.up;
        SnakeMovement snakeMovement = Instantiate(snake, spawnPos, Quaternion.identity);
        snakeMovement.Initialize(_gridTiles, tile, _levelGenerator.DistanceBetweenTiles);
        
        // Let Snake Go Wild
        snakeMovement.StartMoving();
    }
}
