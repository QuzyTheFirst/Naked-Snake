using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [Header("Snakes")]
    [SerializeField] private Transform _snakePf;
    [SerializeField] private Transform _snakeParent;

    private LevelToLoadInfo _currentLevelInfo;

    private GridsManipulator _gridsManipulator;
    private LevelGridGenerator _levelGridGenerator;

    private FruitSpawner _fruitSpawner;
    private FruitsCollector _fruitsCollector;

    [SerializeField]private SnakeMovement _snakeMovement;
    private CameraController _cameraController;

    private InGameUI _inGameUI;

    private SnakeExploder _snakeExploder;

    private GameStateController _gameStateController;

    public SnakeMovement SnakeMovement => _snakeMovement;

    public void Initialize(GridsManipulator gridsManipulator, LevelGridGenerator levelGridGenerator, FruitSpawner fruitSpawner, CameraController cameraController, InGameUI inGameUI, SnakeExploder snakeExploder, GameStateController gameStateController, FruitsCollector fruitsCollector)
    {
        _gridsManipulator = gridsManipulator;
        _levelGridGenerator = levelGridGenerator;

        _fruitSpawner = fruitSpawner;

        _fruitsCollector = fruitsCollector;

        _cameraController = cameraController;

        _inGameUI = inGameUI;

        _snakeExploder = snakeExploder;

        _gameStateController = gameStateController;
    }
}
