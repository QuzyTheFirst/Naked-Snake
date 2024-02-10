using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Serialization;

public class GameEntryPoint : MonoBehaviour
{
    [Header("Grid")] 
    [SerializeField] private float _cellSize;
    [SerializeField] private Vector2 _originPosition;

    [Header("Snake")] 
    [SerializeField] private int _moveEach_ms = 400;
    [SerializeField] private int _boostMoveEach_ms = 200;
    
    [Header("Dependencies")]
    [SerializeField] private Sprite _testMap;
    [SerializeField] private LevelGridGenerator _levelGridGenerator;
    [SerializeField] private SnakeController _snakeController;
    [SerializeField] private FruitsController fruitsController;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private GameLevelController _gameLevelController;
    [SerializeField] private InGameUI _inGameUI;
    [SerializeField] private PostProcessVolume _postProcessVolume;
    
    [Header("Visuals")] 
    [SerializeField] private MapTilesVisual _mapTilesVisual;
    [SerializeField] private SnakePartsVisual _snakePartsVisual;
    [SerializeField] private FruitsVisual _fruitsVisual;

    private LevelLoader _levelLoader;
    
    private GridsManipulator _gridsManipulator;

    private void Awake()
    {
        _levelLoader = FindObjectOfType<LevelLoader>();
    }

    private void Start()
    {
        // Initialize Variables
        LevelToLoadInfo lvl = FindObjectOfType<LevelToLoadInfo>();
        
        // Check if there is level
        if (lvl == null)
            return;
        
        // Initialize Grids
        MapTilesGrid mapTilesGrid = _levelGridGenerator.GenerateLevel(lvl.LevelSprite.texture, _cellSize, _originPosition);
        SnakePartsGrid snakePartsGrid = new SnakePartsGrid(mapTilesGrid.GetWidth(), mapTilesGrid.GetHeight(),_cellSize, _originPosition);
        FruitsGrid fruitsGrid = new FruitsGrid(mapTilesGrid.GetWidth(), mapTilesGrid.GetHeight(), _cellSize, _originPosition);
        
        // Initialize Grids Manipulator
        _gridsManipulator = new GridsManipulator(mapTilesGrid, snakePartsGrid, fruitsGrid);
        
        // Give SnakeController and FruitController their grids and GridsManipulator
        _snakeController.Initialize(snakePartsGrid, _gridsManipulator, _moveEach_ms, _boostMoveEach_ms, _gameLevelController);
        fruitsController.Initialize(fruitsGrid, _gridsManipulator);
        
        // Game Visuals
        mapTilesGrid.SetMapTilesVisuals(_mapTilesVisual);
        snakePartsGrid.SetSnakePartsVisuals(_snakePartsVisual);
        fruitsGrid.SetFruitsGridVisuals(_fruitsVisual);
        
        //UI
        _inGameUI.Initialize(_gameLevelController, _postProcessVolume);
        
        // Camera
        _cameraController.SetupCamera(mapTilesGrid.GetWidth(), mapTilesGrid.GetHeight(), _cellSize, _originPosition);
        
        // Game Level Controller
        _gameLevelController.Initialize(_snakeController, fruitsController, _levelLoader);
        _gameLevelController.InitializeGame();
    }
}
