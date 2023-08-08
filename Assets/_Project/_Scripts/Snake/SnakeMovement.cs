using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class SnakeMovement : PlayerInputHandler
{
    public static event EventHandler<Vector2Int> OnSnakeHitFruit;
    public static event EventHandler<GridTile> OnSnakeMoved;
    
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

    [SerializeField] private int _moveEach_ms = 500;
    private float _moveTimer;

    private GridsManipulator _gridsManipulator;
    private GridTile _currentGridTile;
    private float _distanceBetweenTiles;

    private SnakeBodyController _snakeBodyController;

    public void Initialize(GridsManipulator gridsManipulator, GridTile gridTile, float distanceBetweenTiles)
    {
        _gridsManipulator = gridsManipulator;
        _currentGridTile = gridTile;
        _distanceBetweenTiles = distanceBetweenTiles;

        _snakeBodyController = GetComponent<SnakeBodyController>();
    }

    public async void StartMoving()
    {
        while (_snakeState == SnakeState.Alive)
        {
            await Task.Delay(_moveEach_ms);
            
            // Finding Next Tile
            Vector2Int nextTilePosition = new Vector2Int(_currentGridTile.X, _currentGridTile.Y) + Get2DMovementDirection();
            GridTile nextTile = _gridsManipulator.GridTiles.TryGetTile(nextTilePosition.x, nextTilePosition.y);

            if (nextTile == null)
            {
                _snakeState = SnakeState.Dead;
                continue;
            }
            
            OnSnakeMoved?.Invoke(this, _currentGridTile);
            
            _currentGridTile = nextTile;
            
            
            // Changing Snake Position
            UpdateSnakeGridPosition();
            
            Vector3 newSnakePosition = new Vector3(_currentGridTile.X, 0, _currentGridTile.Y) * _distanceBetweenTiles + Vector3.up;
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
        Debug.Log("Snake is dead");
    }

    private void UpdateSnakeGridPosition()
    {
        _gridsManipulator.GridSnakes.ResetGrid();
        
        GridIntItem snake = new GridIntItem();
        snake.SetItem(_currentGridTile.X, _currentGridTile.Y, 1);
        _gridsManipulator.GridSnakes.TrySetTile(snake);
        
        Debug.Log("Head new grid pos: " + snake.X + " | " + snake.Y);

        foreach (SnakeBody snakeBody in _snakeBodyController.SpawnedSnakeBodies)
        {
            GridIntItem body = new GridIntItem();
            body.SetItem(snakeBody.GridPosition.x, snakeBody.GridPosition.y, 1);
            _gridsManipulator.GridSnakes.TrySetTile(body);
            
            Debug.Log("Body new grid pos: " + body.X + " | " + body.Y);
        }
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
