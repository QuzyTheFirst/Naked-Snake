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
    [SerializeField] private LevelController _levelChanger;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private SnakeExploder _snakeExploder;
    [SerializeField] private FruitsCollector _fruitsCollector;

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
        
        _levelChanger.Initialize(
            _gridsManipulator,
            _levelGenerator,
            _fruitSpawner,
            _cameraController,
            _inGameUI,
            _snakeExploder,
            _gameStateController,
            _fruitsCollector);
        _inGameUI.Initialize(_sceneController, _gameStateController, _levelChanger);
        _fruitSpawner.Initialize(_gridsManipulator);

        LevelToLoadInfo lvl = FindObjectOfType<LevelToLoadInfo>();
        _levelChanger.TryCollectLevel(lvl);

        _gameStateController.Initialize(_inGameUI, _fruitsCollector);
        _gameStateController.ContinueGame();
    }
}
