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
    private GridSnakes _gridSnakes;
    private GridsManipulator _gridsManipulator;
    
    private void Start()
    {
        // Initializing Variables
        _gridTiles = new GridTiles();
        _gridItems = new GridItems();
        _gridSnakes = new GridSnakes();
        _gridsManipulator = new GridsManipulator(_gridTiles, _gridSnakes, _gridItems);
        
        _inGameUI.Initialize(_sceneController, _gameStateController);
        _gameStateController.Initialize(_inGameUI);
        _fruitSpawner.Initialize(_gridsManipulator, _levelGenerator.DistanceBetweenTiles);
        
        // Setting Up Level
        _levelGenerator.GenerateLevel();
        
        _gridTiles.SetGridSize(_levelGenerator.GridSize.x, _levelGenerator.GridSize.y);
        _gridTiles.SetGridTiles(_levelGenerator.GridTiles);
        
        _gridItems.SetGridSize(_levelGenerator.GridSize.x, _levelGenerator.GridSize.y);
        
        _gridSnakes.SetGridSize(_levelGenerator.GridSize.x, _levelGenerator.GridSize.y);
        
        _fruitSpawner.SpawnFruit();
        
        // Spawning Snake
        GridTile tile = _gridTiles.GetRandomSpawnTile();
        Vector3 spawnPos = new Vector3(tile.X, 0, tile.Y) * _levelGenerator.DistanceBetweenTiles + Vector3.up;
        SnakeMovement snakeMovement = Instantiate(snake, spawnPos, Quaternion.identity);
        snakeMovement.Initialize(_gridsManipulator, tile, _levelGenerator.DistanceBetweenTiles);
        
        // Let Snake Go Wild
        snakeMovement.StartMoving();
    }
}
