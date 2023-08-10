using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public struct SnakeMoveData
{
    public GridTile PreviousTile;
    public GridTile CurrentTile;
}

public class SnakeMovement : PlayerInputHandler
{
    public static event EventHandler<Vector2Int> OnSnakeHitFruit;
    public static event EventHandler<SnakeMoveData> OnSnakeMoved;

    enum MovementDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    private MovementDirection _movementDirection = MovementDirection.Up;
    private MovementDirection _lastStepMoveDir;

    public enum SnakeState
    {
        Alive,
        Dead
    }

    private SnakeState _snakeState = SnakeState.Alive;

    [SerializeField] private int _normalMoveEach_ms = 500;
    [SerializeField] private int _boostMoveEach_ms = 250;
    private int _currentMoveEach_ms;

    private GridsManipulator _gridsManipulator;
    private GridTile _currentGridTile;
    private Transform _snakeParent;
    private InGameUI _inGameUI;

    private SnakeBodyController _snakeBodyController;

    private CancellationTokenSource _snakeCTS;

    public void Initialize(GridsManipulator gridsManipulator, GridTile gridTile, Transform snakeParent, InGameUI inGameUI)
    {
        _gridsManipulator = gridsManipulator;
        _currentGridTile = gridTile;
        _snakeParent = snakeParent;

        _snakeBodyController = GetComponent<SnakeBodyController>();
        _snakeBodyController.Initialize(_snakeParent, _gridsManipulator, _currentGridTile);

        _inGameUI = inGameUI;

        _currentMoveEach_ms = _normalMoveEach_ms;
    }

    public async void StartMoving()
    {
        if (_snakeCTS == null)
            _snakeCTS = new CancellationTokenSource();

        try
        {
            await MoveSnake(_snakeCTS.Token);
        }
        catch (TaskCanceledException ex)
        {
            Debug.Log("Snake Canceled");
        }
        finally
        {
            _snakeCTS = null;
        }
        
        Debug.Log("Snake is dead");
    }

    private async Task MoveSnake(CancellationToken token)
    {
        while (_snakeState == SnakeState.Alive)
        {
            _inGameUI.StartProgressBarAnimation((float)_currentMoveEach_ms / 1000f);
            await Task.Delay(_currentMoveEach_ms, token);

            // Finding Next Tile
            Vector2Int nextTilePosition =
                new Vector2Int(_currentGridTile.X, _currentGridTile.Y) + Get2DMovementDirection();
            
            GridTile nextTile = _gridsManipulator.GridTiles.TryGetTile(nextTilePosition.x, nextTilePosition.y);
            GridTile oldTile = _currentGridTile;
            
            if (nextTile == null)
            {
                _snakeState = SnakeState.Dead;
                continue;
            }

            if (_gridsManipulator.CheckTileForSnake(nextTile.X, nextTile.Y))
            {
                _snakeState = SnakeState.Dead;
            }


            _currentGridTile = nextTile;


            // Changing Snake Position
            _gridsManipulator.GridSnakes.ResetGrid();
            
            UpdateSnakeGridPosition();
            
            OnSnakeMoved?.Invoke(this, new SnakeMoveData()
                {
                    CurrentTile = nextTile,
                    PreviousTile = oldTile
                });
            
            Vector3 newSnakePosition =
                new Vector3(_currentGridTile.X, 0, _currentGridTile.Y) * LevelGenerator.DistanceBetweenTiles +
                Vector3.up;
            transform.position = newSnakePosition;

            _lastStepMoveDir = _movementDirection;

            // Check New Tile For Things
            if (_gridsManipulator.CheckTileForFruit(_currentGridTile.X, _currentGridTile.Y))
            {
                OnSnakeHitFruit?.Invoke(this, new Vector2Int(_currentGridTile.X, _currentGridTile.Y));
            }

            if (_currentGridTile.CurrentTileType == GridTile.TileType.DeathTile)
            {
                _snakeState = SnakeState.Dead;
                continue;
            }
        }
    }
    
    public void CancelMovementAndDestroySnake()
    {
        if (_snakeCTS == null)
            return;
        _snakeCTS.Cancel();

        foreach (Transform snakePart in _snakeParent)
        {
            Destroy(snakePart.gameObject);
        }
    }
    
    private void UpdateSnakeGridPosition()
    {
        GridIntItem snake = new GridIntItem();
        snake.SetItem(_currentGridTile.X, _currentGridTile.Y, 1);
        _gridsManipulator.GridSnakes.TrySetTile(snake);
    }

    private Vector2Int Get2DMovementDirection()
    {
        switch (_movementDirection)
        {
            case MovementDirection.Up:
                return Vector2Int.up;
                break;
            case MovementDirection.Down:
                return Vector2Int.down;
                break;
            case MovementDirection.Left:
                return Vector2Int.left;
                break;
            case MovementDirection.Right:
                return Vector2Int.right;
                break;
            default:
                return Vector2Int.zero;
                break;
        }
    }

    private MovementDirection GetOpposingMovementDirection(MovementDirection direction)
    {
        switch (direction)
        {
            case MovementDirection.Up:
                return MovementDirection.Down;
                break;
            case MovementDirection.Down:
                return MovementDirection.Up;
                break;
            case MovementDirection.Left:
                return MovementDirection.Right;
                break;
            case MovementDirection.Right:
                return MovementDirection.Left;
                break;
            default:
                throw new Exception("Where are you even going?");
                break;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        OnUpPressed += SnakeHead_OnUpPressed;
        OnDownPressed += SnakeHead_OnDownPressed;
        OnLeftPressed += SnakeHead_OnLeftPressed;
        OnRightPressed += SnakeHead_OnRightPressed;
        
        OnBoostButtonPressed += OnOnBoostButtonPressed;
        OnBoostButtonCanceled += OnOnBoostButtonCanceled;
    }

    private void OnOnBoostButtonCanceled(object sender, EventArgs e)
    {
        _inGameUI.ToggleShiftText(false);
        _currentMoveEach_ms = _normalMoveEach_ms;
    }

    private void OnOnBoostButtonPressed(object sender, EventArgs e)
    {
        _inGameUI.ToggleShiftText(true);
        _currentMoveEach_ms = _boostMoveEach_ms;
    }

    private void SnakeHead_OnRightPressed(object sender, EventArgs e)
    {
        if (MovementDirection.Right == GetOpposingMovementDirection(_lastStepMoveDir))
            return;

        _movementDirection = MovementDirection.Right;
        transform.rotation = Quaternion.LookRotation(Vector3.right);
    }

    private void SnakeHead_OnLeftPressed(object sender, EventArgs e)
    {
        if (MovementDirection.Left == GetOpposingMovementDirection(_lastStepMoveDir))
            return;

        _movementDirection = MovementDirection.Left;
        transform.rotation = Quaternion.LookRotation(Vector3.left);
    }

    private void SnakeHead_OnDownPressed(object sender, EventArgs e)
    {
        if (MovementDirection.Down == GetOpposingMovementDirection(_lastStepMoveDir))
            return;

        _movementDirection = MovementDirection.Down;
        transform.rotation = Quaternion.LookRotation(Vector3.back);
    }

    private void SnakeHead_OnUpPressed(object sender, EventArgs e)
    {
        if (MovementDirection.Up == GetOpposingMovementDirection(_lastStepMoveDir))
            return;

        _movementDirection = MovementDirection.Up;
        transform.rotation = Quaternion.LookRotation(Vector3.forward);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnUpPressed -= SnakeHead_OnUpPressed;
        OnDownPressed -= SnakeHead_OnDownPressed;
        OnLeftPressed -= SnakeHead_OnLeftPressed;
        OnRightPressed -= SnakeHead_OnRightPressed;
    }
}