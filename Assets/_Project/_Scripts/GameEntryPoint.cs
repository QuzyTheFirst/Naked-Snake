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

    private GridController _gridController;
    
    private void Start()
    {
        // Initializing Variables
        _gridController = new GridController();
        
        _inGameUI.Initialize(_sceneController, _gameStateController);
        _gameStateController.Initialize(_inGameUI);
        _levelGenerator.Initialize(_gridController);
        _fruitSpawner.Initialize(_gridController, _levelGenerator.DistanceBetweenTiles);
        
        // Setting Up Level
        _levelGenerator.GenerateLevel();
        _fruitSpawner.SpawnFruit();

        // Spawning Snake
        GridTile tile = _gridController.GetRandomSpawnTile();
        Vector3 spawnPos = new Vector3(tile.GridPosition.x, 0, tile.GridPosition.y) * _levelGenerator.DistanceBetweenTiles + Vector3.up;
        SnakeMovement snakeMovement = Instantiate(snake, spawnPos, Quaternion.identity);
        snakeMovement.Initialize(_gridController, tile, _levelGenerator.DistanceBetweenTiles);
        
        // Let Snake Go Wild
        snakeMovement.StartMoving();
    }
}
