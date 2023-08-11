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
    public static event EventHandler OnSnakeDeath;

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
    
    private Rigidbody _rig;

    private SnakeExploder _snakeExploder;
    
    private static float _leanTweenTransitionTime = .1f;
    public static float LeanTweenTransitionTime => _leanTweenTransitionTime;

    public void Initialize(GridsManipulator gridsManipulator, GridTile gridTile, Transform snakeParent, InGameUI inGameUI, SnakeExploder snakeExploder)
    {
        _gridsManipulator = gridsManipulator;
        _currentGridTile = gridTile;
        _snakeParent = snakeParent;

        _snakeBodyController = GetComponent<SnakeBodyController>();
        _snakeBodyController.Initialize(_snakeParent, _gridsManipulator, _currentGridTile);

        _rig = GetComponent<Rigidbody>();
        
        _inGameUI = inGameUI;

        _currentMoveEach_ms = _normalMoveEach_ms;

        _snakeExploder = snakeExploder;
    }

    public IEnumerator StartMoving()
    {
        yield return StartCoroutine(MoveSnake());

        yield return StartCoroutine(ExplodeSnake());

        OnSnakeDeath?.Invoke(this, EventArgs.Empty);
    }

    private IEnumerator ExplodeSnake()
    {
        Queue<Rigidbody> rigidbodies = new Queue<Rigidbody>();
        rigidbodies.Enqueue(_rig);
        
        foreach (SnakeBody body in _snakeBodyController.SpawnedSnakeBodies)
        {
            rigidbodies.Enqueue(body.Rigidbody);
        }
        
        yield return StartCoroutine(_snakeExploder.Explode(rigidbodies));
    }
    
    private IEnumerator MoveSnake()
    {
        while (_snakeState == SnakeState.Alive)
        {
            _inGameUI.StartProgressBarAnimation((float)_currentMoveEach_ms * .001f);
            yield return new WaitForSeconds(_currentMoveEach_ms * .001f);

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
            

            _currentGridTile = nextTile;


            // Changing Snake Position
            _gridsManipulator.GridSnakes.ResetGrid();
            
            // Shit started (dont touch)
            // Updates snake body first
            OnSnakeMoved?.Invoke(this, new SnakeMoveData()
                {
                    CurrentTile = nextTile,
                    PreviousTile = oldTile
                });
            
            //Check if next tile has a snake
            if (_gridsManipulator.CheckTileForSnake(nextTile.X, nextTile.Y))
            {
                _snakeState = SnakeState.Dead;
            }
            
            //Update Snake Head
            UpdateSnakeGridPosition();
            // Shit Ending
            
            Vector3 newSnakePosition =
                new Vector3(_currentGridTile.X, 0, _currentGridTile.Y) * LevelGenerator.DistanceBetweenTiles +
                Vector3.up;
            //transform.position = newSnakePosition;
            LeanTween.move(gameObject, newSnakePosition, _leanTweenTransitionTime).setOnComplete(() =>
            {
                SoundManager.Instance.Play("Footstep");
            });

            _lastStepMoveDir = _movementDirection;

            // Check New Tile For Things
            if (_gridsManipulator.CheckTileForFruit(_currentGridTile.X, _currentGridTile.Y))
            {
                OnSnakeHitFruit?.Invoke(this, new Vector2Int(_currentGridTile.X, _currentGridTile.Y));
                SoundManager.Instance.Play("Chew");
            }

            if (_currentGridTile.CurrentTileType == GridTile.TileType.DeathTile)
            {
                _snakeState = SnakeState.Dead;
                continue;
            }
        }
    }
    
    public void DestroySnake()
    {
        foreach (Transform snakePart in _snakeParent)
        {
            Destroy(snakePart.gameObject);
        }
    }
    
    public void DeactivateSnake()
    {
        StopAllCoroutines();
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