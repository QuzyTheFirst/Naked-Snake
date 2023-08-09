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
    [SerializeField] private LevelChanger levelChanger;
    [SerializeField] private CameraController _cameraController;

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
        
        levelChanger.Initialize(_gridsManipulator, _levelGenerator, _fruitSpawner, _sceneController, _cameraController, _inGameUI);
        _inGameUI.Initialize(_sceneController, _gameStateController);
        _gameStateController.Initialize(_inGameUI);
        _fruitSpawner.Initialize(_gridsManipulator, LevelGenerator.DistanceBetweenTiles);
        
        // Setting Up Level or going to end menu
        if(levelChanger.TryChangeLevel(0) == false)
            _sceneController.LoadNextScene();
    }
}
