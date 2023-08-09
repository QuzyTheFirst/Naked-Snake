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

    private SnakeMovement _snakeMovement;
    private CameraController _cameraController;

    private InGameUI _inGameUI;

    public void Initialize(GridsManipulator gridsManipulator, LevelGenerator levelGenerator, FruitSpawner fruitSpawner, SceneController sceneController, CameraController cameraController, InGameUI inGameUI)
    {
        _gridsManipulator = gridsManipulator;
        _levelGenerator = levelGenerator;

        _fruitSpawner = fruitSpawner;

        _sceneController = sceneController;

        _fruitsCollector = new FruitsCollector();

        _cameraController = cameraController;

        _inGameUI = inGameUI;
    }
    
    public bool TryChangeLevel(int levelID)
    {
        if (levelID >= _allLevels.Count)
            return false;

        //Generating Level and Grid
        _levelGenerator.GenerateLevel(_allLevels[levelID].Map);
        _gridsManipulator.SetGridsSize(_levelGenerator.GeneratedGridSize.x, _levelGenerator.GeneratedGridSize.y);
        _gridsManipulator.GridTiles.SetGridTiles(_levelGenerator.GridTiles);

        _fruitsCollector.SetNewTarget(_allLevels[levelID].ApplesToEat);
        _inGameUI.UpdateApplesTextField(0, _allLevels[levelID].ApplesToEat);

        _cameraController.SetNewCameraPos(_levelGenerator.GeneratedGridSize.x, _levelGenerator.GeneratedGridSize.y);

        // Cleaning Previous Stuff
        _fruitSpawner.DeleteAllFruits();

        if (_snakeMovement != null)
            _snakeMovement.CancelMovementAndDestroySnake();

        // Spawning New Stuff
        _fruitSpawner.SpawnFruit();

        GridTile tile = _gridsManipulator.GridTiles.GetRandomSpawnTile();
        Vector3 spawnPos = new Vector3(tile.X, 0, tile.Y) * LevelGenerator.DistanceBetweenTiles + Vector3.up;
        SnakeMovement snakeMovement = Instantiate(_snakePf, spawnPos, Quaternion.identity, _snakeParent)
            .GetComponent<SnakeMovement>();
        snakeMovement.Initialize(_gridsManipulator, tile, _snakeParent, _inGameUI);
        _snakeMovement = snakeMovement;

        // Let Snake Go Wild
        snakeMovement.StartMoving();

        _currentLevelID = levelID;

        return true;
    }
    
    private void OnEnable()
    {
        FruitsCollector.AllFruitsCollected += FruitsCollectorOnAllFruitsCollected;
    }

    private void FruitsCollectorOnAllFruitsCollected(object sender, EventArgs e)
    {
        if (TryChangeLevel(_currentLevelID + 1) == false)
        {
            if (_snakeMovement != null)
                _snakeMovement.CancelMovementAndDestroySnake();
            
            _sceneController.LoadNextScene();
        }
    }

    private void OnDisable()
    {
        FruitsCollector.AllFruitsCollected -= FruitsCollectorOnAllFruitsCollected;
    }
}
