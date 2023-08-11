using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBodyController : MonoBehaviour
{
    [SerializeField] private Transform _snakeBodyPf;

    private GridTile[] _lastSnakeGridTiles;

    private List<SnakeBody> _spawnedSnakeBodies;

    private Transform _snakeParent;
    private GridsManipulator _gridsManipulator;
    private GridTile _snakeHeadGridTile;

    private int _amountOfLastPlayerPositionsToSave;

    private Vector3 _standardSnakeBodySize;

    public IReadOnlyList<SnakeBody> SpawnedSnakeBodies => _spawnedSnakeBodies;

    public void Initialize(Transform snakeParent, GridsManipulator gridsManipulator, GridTile snakeHeadGridTile)
    {
        _snakeParent = snakeParent;
        _gridsManipulator = gridsManipulator;
        _snakeHeadGridTile = snakeHeadGridTile;

        _spawnedSnakeBodies = new List<SnakeBody>();
        _amountOfLastPlayerPositionsToSave = _spawnedSnakeBodies.Count + 1;

        _lastSnakeGridTiles = new GridTile[_amountOfLastPlayerPositionsToSave];

        _standardSnakeBodySize = _snakeBodyPf.localScale;
    }

    private void PlayerMoved(GridTile from, GridTile to)
    {
        _snakeHeadGridTile = to;

        AddLastPositionToArray(from);

        MoveSnakeBodies();
    }

    private void AddLastPositionToArray(GridTile tile)
    {
        for (int i = _lastSnakeGridTiles.Length - 2; i >= 0; i--)
        {
            _lastSnakeGridTiles[i + 1] = _lastSnakeGridTiles[i];
        }

        _lastSnakeGridTiles[0] = tile;
    }

    private void MoveSnakeBodies()
    {
        for (int i = 0; i < _spawnedSnakeBodies.Count; i++)
        {
            SnakeBody body = _spawnedSnakeBodies[i];
            GridTile tile = _lastSnakeGridTiles[i];

            //body.Transform.position = new Vector3(tile.X, 0, tile.Y) * LevelGenerator.DistanceBetweenTiles + Vector3.up;
            Vector3 targetPos = new Vector3(tile.X, 0, tile.Y) * LevelGenerator.DistanceBetweenTiles + Vector3.up;
            LeanTween.move(body.Transform.gameObject, targetPos, SnakeMovement.LeanTweenTransitionTime);
            body.SetGridPosition(tile.X, tile.Y);
        }

        UpdateSnakeBodiesGridPositions();
        RotateSnakeBodies();
    }

    private void SpawnNewBody()
    {
        Transform body = Instantiate(_snakeBodyPf, _snakeParent);
        GridTile tile = _lastSnakeGridTiles[_lastSnakeGridTiles.Length - 1];
        SnakeBody snakeBody = new SnakeBody(tile.X, tile.Y, body);
        body.position = new Vector3(tile.X, 0, tile.Y) * LevelGenerator.DistanceBetweenTiles + Vector3.up;

        _spawnedSnakeBodies.Add(snakeBody);
        _amountOfLastPlayerPositionsToSave = _spawnedSnakeBodies.Count + 1;

        UpdateSnakeBodiesGridPositions();
        RotateSnakeBodies();
        UpdateSavedPositionsCapacity();
        ResizeLastThreeBodies();
    }

    private void ResizeLastThreeBodies()
    {
        for (int i = _spawnedSnakeBodies.Count - 1; i >= 0; i--)
        {
            SnakeBody body = _spawnedSnakeBodies[i];
            
            float scaleValue = Mathf.InverseLerp(_spawnedSnakeBodies.Count, _spawnedSnakeBodies.Count - 3, i);

            body.Transform.localScale = _standardSnakeBodySize * scaleValue;
        }
    }
    
    private void RotateSnakeBodies()
    {
        for (int i = 0; i < _spawnedSnakeBodies.Count; i++)
        {
            SnakeBody previousBody = i - 1 < 0 ? null : _spawnedSnakeBodies[i - 1];
            SnakeBody body = _spawnedSnakeBodies[i];
            SnakeBody nextBody = i + 1 >= _spawnedSnakeBodies.Count ? null : _spawnedSnakeBodies[i + 1];

            RotateBody(previousBody, body, nextBody);
        }
    }

    private void RotateBody(SnakeBody previousBody, SnakeBody body, SnakeBody nextBody)
    {
        if (previousBody != null && nextBody != null)
        {
            Vector2 nextBodyDirection = nextBody.GridPosition - body.GridPosition;
            Vector2 previousBodyDirection = previousBody.GridPosition - body.GridPosition;
            Vector2 directionFromPreviousToNextBody = nextBodyDirection + previousBodyDirection;

            if (directionFromPreviousToNextBody == Vector2.zero)
            {
                Vector3 worldLookRotationVector =
                    new Vector3(previousBodyDirection.x, 0f, previousBodyDirection.y);
                body.SpineThing.rotation = Quaternion.LookRotation(worldLookRotationVector);
            }
            else
            {
                Vector3 worldLookRotationVector = new Vector3(-directionFromPreviousToNextBody.x, 0,
                    directionFromPreviousToNextBody.y);
                body.SpineThing.rotation = Quaternion.LookRotation(worldLookRotationVector);
            }
        }

        if (previousBody != null && nextBody == null)
        {
            Vector2 previousBodyDirection = previousBody.GridPosition - body.GridPosition;

            Vector3 worldLookRotationVector =
                new Vector3(previousBodyDirection.x, 0f, previousBodyDirection.y);
            body.SpineThing.rotation = Quaternion.LookRotation(worldLookRotationVector);
        }

        if (previousBody == null && nextBody != null)
        {
            Vector2 nextBodyDirection = nextBody.GridPosition - body.GridPosition;
            Vector2 headPos = new Vector2(_snakeHeadGridTile.X, _snakeHeadGridTile.Y);
            Vector2 previousBodyDirection = headPos - body.GridPosition;
            Vector2 directionFromPreviousToNextBody = nextBodyDirection + previousBodyDirection;

            if (directionFromPreviousToNextBody == Vector2.zero)
            {
                Vector3 worldLookRotationVector =
                    new Vector3(previousBodyDirection.x, 0f, previousBodyDirection.y);
                body.SpineThing.rotation = Quaternion.LookRotation(worldLookRotationVector);
            }
            else
            {
                Vector3 worldLookRotationVector = new Vector3(-directionFromPreviousToNextBody.x, 0,
                    directionFromPreviousToNextBody.y);
                body.SpineThing.rotation = Quaternion.LookRotation(worldLookRotationVector);
            }
        }

        if (previousBody == null && nextBody == null)
        {
            Vector2 headPos = new Vector2(_snakeHeadGridTile.X, _snakeHeadGridTile.Y);
            Vector2 snakeHeadDirection = headPos - body.GridPosition;

            Vector3 worldLookRotationVector =
                new Vector3(snakeHeadDirection.x, 0f, snakeHeadDirection.y);
            body.SpineThing.rotation = Quaternion.LookRotation(worldLookRotationVector);
        }
    }

    private void UpdateSnakeBodiesGridPositions()
    {
        foreach (SnakeBody snakeBody in _spawnedSnakeBodies)
        {
            GridIntItem body = new GridIntItem();
            body.SetItem(snakeBody.GridPosition.x, snakeBody.GridPosition.y, 1);
            _gridsManipulator.GridSnakes.TrySetTile(body);
        }
    }

    private void UpdateSavedPositionsCapacity()
    {
        GridTile[] tmp = new GridTile[_amountOfLastPlayerPositionsToSave];
        for (int i = 0; i < _lastSnakeGridTiles.Length; i++)
        {
            tmp[i] = _lastSnakeGridTiles[i];
        }

        _lastSnakeGridTiles = tmp;
    }

    private void OnEnable()
    {
        FruitsCollector.FruitSuccessfullyEaten += FruitsCollectorOnFruitSuccessfullyEaten;
        SnakeMovement.OnSnakeMoved += SnakeMovement_OnSnakeMoved;
    }

    private void FruitsCollectorOnFruitSuccessfullyEaten(object sender, Vector2Int position)
    {
        SpawnNewBody();
    }

    private void SnakeMovement_OnSnakeMoved(object sender, SnakeMoveData data)
    {
        PlayerMoved(data.PreviousTile, data.CurrentTile);
    }

    private void OnDisable()
    {
        FruitsCollector.FruitSuccessfullyEaten -= FruitsCollectorOnFruitSuccessfullyEaten;
        SnakeMovement.OnSnakeMoved -= SnakeMovement_OnSnakeMoved;
    }
}