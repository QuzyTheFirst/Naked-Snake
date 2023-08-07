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
    enum MovementDirection
    {
        Up, 
        Down, 
        Left, 
        Right
    }
    private MovementDirection _movementDirection = MovementDirection.Up;

    public enum SnakeState
    {
        Alive, 
        Dead
    }
    private SnakeState _snakeState = SnakeState.Alive;

    [SerializeField] private int _moveEach_ms = 500;
    private float _moveTimer;

    private GridController _gridController;
    private GridTile _currentGridTile;
    private float _distanceBetweenTiles;

    public void Initialize(GridController gridController, GridTile gridTile, float distanceBetweenTiles)
    {
        _gridController = gridController;
        _currentGridTile = gridTile;
        _distanceBetweenTiles = distanceBetweenTiles;
    }

    public async void StartMoving()
    {
        while (_snakeState == SnakeState.Alive)
        {
            await Task.Delay(_moveEach_ms);
            
            // Finding Next Tile
            Vector2Int nextTilePosition = _currentGridTile.GridPosition + Get2DMovementDirection();
            GridTile nextTile = _gridController.GetTile(nextTilePosition.x, nextTilePosition.y);

            if (nextTile == null)
            {
                _snakeState = SnakeState.Dead;
                continue;
            }
            
            // Changing Snake Position
            Vector3 newSnakePosition = new Vector3(nextTile.GridPosition.x, 0, nextTile.GridPosition.y) * _distanceBetweenTiles + Vector3.up;
            transform.position = newSnakePosition;
            
            _currentGridTile = nextTile;

            if (_currentGridTile.CurrentTileType == GridTile.TileType.DeathTile)
            {
                _snakeState = SnakeState.Dead;
                continue;
            }
        }
        Debug.Log("Snake is dead");
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
        if (_movementDirection == MovementDirection.Right || _movementDirection == MovementDirection.Left)
            return;

        _movementDirection = MovementDirection.Right;
        transform.rotation = Quaternion.LookRotation(Vector3.right);
    }

    private void SnakeHead_OnLeftPressed(object sender, EventArgs e)
    {
        if (_movementDirection == MovementDirection.Right || _movementDirection == MovementDirection.Left)
            return;

        _movementDirection = MovementDirection.Left;
        transform.rotation = Quaternion.LookRotation(Vector3.left);
    }

    private void SnakeHead_OnDownPressed(object sender, EventArgs e)
    {
        if (_movementDirection == MovementDirection.Down || _movementDirection == MovementDirection.Up)
            return;

        _movementDirection = MovementDirection.Down;
        transform.rotation = Quaternion.LookRotation(Vector3.back);
    }

    private void SnakeHead_OnUpPressed(object sender, EventArgs e)
    {
        if (_movementDirection == MovementDirection.Down || _movementDirection == MovementDirection.Up)
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
