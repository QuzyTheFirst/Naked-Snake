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

    [SerializeField] private Transform _snakeHead;

    private GridController _gridController;
    
    private void Start()
    {
        // Initializing Variables
        _gridController = new GridController();
        
        _inGameUI.Initialize(_sceneController, _gameStateController);
        _gameStateController.Initialize(_inGameUI);
        _levelGenerator.Initialize(_gridController);
        
        // Setting Up Level
        _levelGenerator.GenerateLevel();

        // Spawning Snake
        GridTile tile = _gridController.GetRandomSpawnTile();
        Vector3 spawnPos = new Vector3(tile.GridPosition.x, 0, tile.GridPosition.y) * _levelGenerator.DistanceBetweenTiles + Vector3.up;
        Debug.Log("Updated Spawn Pos: " + spawnPos);
        Instantiate(_snakeHead, spawnPos, Quaternion.identity);
    }
}
