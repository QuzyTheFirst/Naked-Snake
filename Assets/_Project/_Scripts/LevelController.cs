using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [Header("Snakes")]
    [SerializeField] private Transform _snakePf;
    [SerializeField] private Transform _snakeParent;

    private LevelToLoadInfo _currentLevelInfo;

    private GridsManipulator _gridsManipulator;
    private LevelGenerator _levelGenerator;

    private FruitSpawner _fruitSpawner;
    private FruitsCollector _fruitsCollector;

    [SerializeField]private SnakeMovement _snakeMovement;
    private CameraController _cameraController;

    private InGameUI _inGameUI;

    private SnakeExploder _snakeExploder;

    private GameStateController _gameStateController;

    public SnakeMovement SnakeMovement => _snakeMovement;

    public void Initialize(GridsManipulator gridsManipulator, LevelGenerator levelGenerator, FruitSpawner fruitSpawner, CameraController cameraController, InGameUI inGameUI, SnakeExploder snakeExploder, GameStateController gameStateController, FruitsCollector fruitsCollector)
    {
        _gridsManipulator = gridsManipulator;
        _levelGenerator = levelGenerator;

        _fruitSpawner = fruitSpawner;

        _fruitsCollector = fruitsCollector;

        _cameraController = cameraController;

        _inGameUI = inGameUI;

        _snakeExploder = snakeExploder;

        _gameStateController = gameStateController;
    }
    
    public bool TryCollectLevel(LevelToLoadInfo levelInfo)
    {
        if (levelInfo == null)
            return false;

        //Generating Level and Grid
        _levelGenerator.GenerateLevel(levelInfo.LevelSprite.texture);

        _gridsManipulator.SetGridsSize(LevelGenerator.GeneratedGridSize.x, LevelGenerator.GeneratedGridSize.y);
        _gridsManipulator.GridTiles.SetGridTiles(_levelGenerator.GridTiles);

        _cameraController.SetNewCameraPos(LevelGenerator.GeneratedGridSize.x, LevelGenerator.GeneratedGridSize.y);

        // Spawning Stuff
        _fruitSpawner.SpawnFruit();

        GridTile tile = _gridsManipulator.GridTiles.GetRandomSpawnTile();
        Vector3 spawnPos = new Vector3(tile.X, 0, tile.Y) * LevelGenerator.DistanceBetweenTiles + Vector3.up * .25f;

        SnakeMovement snakeMovement = Instantiate(_snakePf, spawnPos, Quaternion.identity, _snakeParent)
            .GetComponent<SnakeMovement>();

        snakeMovement.Initialize(_gridsManipulator, tile, _snakeParent, _inGameUI, _snakeExploder);

        _snakeMovement = snakeMovement;

        // Let Snake Go Wild
        StartCoroutine(snakeMovement.StartMoving());

        _currentLevelInfo = levelInfo;
        _gameStateController.CurrentGameState = GameStateController.GameState.Active;

        return true;
    }

    public void RestartLevel()
    {
        // Cleaning Previous Stuff
        _fruitSpawner.DeleteAllFruits();
        _fruitsCollector.ResetCollectedFruitsAmount();

        if (_snakeMovement != null)
            _snakeMovement.DestroySnake();

        TryCollectLevel(_currentLevelInfo);
    }
}
