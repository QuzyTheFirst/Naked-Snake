using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    [Header("Snakes")]
    [SerializeField] private Transform _snakePf;
    [SerializeField] private Transform _snakeParent;
    
    [Header("Levels")]
    [SerializeField] private List<Level> _allLevels;

    private int _currentLevelID = 0;

    private GridsManipulator _gridsManipulator;
    private LevelGenerator _levelGenerator;

    private FruitSpawner _fruitSpawner;
    private FruitsCollector _fruitsCollector;
    
    private SceneController _sceneController;

    [SerializeField]private SnakeMovement _snakeMovement;
    private CameraController _cameraController;

    private InGameUI _inGameUI;

    private SnakeExploder _snakeExploder;

    private GameStateController _gameStateController;

    public SnakeMovement SnakeMovement => _snakeMovement;

    public void Initialize(GridsManipulator gridsManipulator, LevelGenerator levelGenerator, FruitSpawner fruitSpawner, SceneController sceneController, CameraController cameraController, InGameUI inGameUI, SnakeExploder snakeExploder, GameStateController gameStateController, FruitsCollector fruitsCollector)
    {
        _gridsManipulator = gridsManipulator;
        _levelGenerator = levelGenerator;

        _fruitSpawner = fruitSpawner;

        _sceneController = sceneController;

        _fruitsCollector = fruitsCollector;

        _cameraController = cameraController;

        _inGameUI = inGameUI;

        _snakeExploder = snakeExploder;

        _gameStateController = gameStateController;
    }
    
    public bool TryChangeLevel(int levelID)
    {
        if (levelID >= _allLevels.Count)
            return false;

        //Generating Level and Grid
        _levelGenerator.GenerateLevel(_allLevels[levelID].Map);
        _gridsManipulator.SetGridsSize(LevelGenerator.GeneratedGridSize.x, LevelGenerator.GeneratedGridSize.y);
        _gridsManipulator.GridTiles.SetGridTiles(_levelGenerator.GridTiles);

        _fruitsCollector.SetNewTarget(_allLevels[levelID].ApplesToEat);
        _inGameUI.UpdateApplesTextField(0, _allLevels[levelID].ApplesToEat);

        _cameraController.SetNewCameraPos(LevelGenerator.GeneratedGridSize.x, LevelGenerator.GeneratedGridSize.y);

        // Cleaning Previous Stuff
        _fruitSpawner.DeleteAllFruits();

        if (_snakeMovement != null)
            _snakeMovement.DestroySnake();

        // Spawning New Stuff
        _fruitSpawner.SpawnFruit();

        GridTile tile = _gridsManipulator.GridTiles.GetRandomSpawnTile();
        Vector3 spawnPos = new Vector3(tile.X, 0, tile.Y) * LevelGenerator.DistanceBetweenTiles + Vector3.up;
        SnakeMovement snakeMovement = Instantiate(_snakePf, spawnPos, Quaternion.identity, _snakeParent)
            .GetComponent<SnakeMovement>();
        snakeMovement.Initialize(_gridsManipulator, tile, _snakeParent, _inGameUI, _snakeExploder);
        _snakeMovement = snakeMovement;
        _gameStateController.SetSnakeMovement(_snakeMovement);

        // Let Snake Go Wild
        StartCoroutine(snakeMovement.StartMoving());

        _currentLevelID = levelID;
        _gameStateController.CurrentGameState = GameStateController.GameState.Active;

        return true;
    }

    public void RestartLevel()
    {
        if (TryChangeLevel(_currentLevelID) == false)
        {
            if (_snakeMovement != null)
                _snakeMovement.DestroySnake();
            
            _sceneController.LoadNextScene();
        }
    }

    public void LoadNextLevel()
    {
        if (TryChangeLevel(_currentLevelID + 1) == false)
        {
            if (_snakeMovement != null)
                _snakeMovement.DestroySnake();
            
            _sceneController.LoadNextScene();
        }
    }
}
