using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeController : PlayerInputHandler
{
    public static event EventHandler SnakeChangedPosition;
    
    enum MovementDirectionEnum
    {
        Up,
        Right,
        Down,
        Left
    }

    private MovementDirectionEnum _currentMovementDirection = MovementDirectionEnum.Up;
    private MovementDirectionEnum _lastStepMovementDirection;
    
    public static event EventHandler<Vector2Int> OnSnakeHitFruit;
    
    public static event EventHandler OnSnakeDeath;

    private SnakePartsGrid _snakePartsGrid;
    private GridsManipulator _gridsManipulator;

    private int _moveEach_ms;
    private int _boostMoveEach_ms;
    private bool _isBoostButtonPressed = false;

    private int CurrentSpeed
    {
        get
        {
            if (_isBoostButtonPressed)
                return _boostMoveEach_ms;

            return _moveEach_ms;
        }
    }

    private SnakePartsGrid.SnakePartGridObject _snakeHead;
    private List<SnakePartsGrid.SnakePartGridObject> _snakeBodies;
    
    public enum SnakeState
    {
        Alive,
        Dead
    }

    private SnakeState _snakeState = SnakeState.Alive;

    private Coroutine SnakeMovingCoroutine = null;
    
    public void Initialize(SnakePartsGrid grid, GridsManipulator gridsManipulator, int moveEach_ms, int boostMoveEach_ms)
    {
        _snakePartsGrid = grid;
        _gridsManipulator = gridsManipulator;
        _moveEach_ms = moveEach_ms;
        _boostMoveEach_ms = boostMoveEach_ms;

        _snakeBodies = new List<SnakePartsGrid.SnakePartGridObject>();
    }

    public void SpawnSnakeHead()
    {
        // Get Random Spawn Tile
        MapTilesGrid.MapTileGridObject spawnTile = _gridsManipulator.GetRandomSpawnpointTile();
        // Get XY from spawn tile
        var tilePos = spawnTile.GetCoordinates();
        // Get Snake Tile By This Coordinates
        SnakePartsGrid.SnakePartGridObject snakeTile = _snakePartsGrid.GetSnakePartTile(tilePos.xPos, tilePos.yPos);
        // Spawn snake head on this tile
        snakeTile.SetSnakeTileParams(SnakePartsGrid.SnakePartGridObject.TileTypeEnum.SnakeHead, null, null, null);
        // Save snake head pos
        _snakeHead = snakeTile;
        _snakePartsGrid.SnakeHead = _snakeHead;
    }

    public void StartMoving()
    {
        if (_snakeHead == null)
            return;
        
        SnakeMovingCoroutine = StartCoroutine(SnakeMovement());
    }
    
    private IEnumerator SnakeMovement()
    {
        while (_snakeState == SnakeState.Alive)
        {
            yield return new WaitForSeconds(CurrentSpeed * .001f);

            Vector2Int currentTilePos = _snakeHead.GetCoordinates();
            Vector2Int nextTilePos = currentTilePos + Get2DMovementDirection();

            if (_gridsManipulator.CheckTileForEmptyOrNull(nextTilePos.x, nextTilePos.y))
            {
                _snakeState = SnakeState.Dead;
                continue;
            }
            
            SnakePartsGrid.SnakePartGridObject nextTile =
                _snakePartsGrid.GetSnakePartTile(nextTilePos.x, nextTilePos.y);

            if (_gridsManipulator.CheckTileForSnake(nextTilePos.x, nextTilePos.y))
            {
                _snakeState = SnakeState.Dead;
                continue;
            }

            SwitchOldToNewTile(ref _snakeHead, nextTile);
            _snakePartsGrid.SnakeHead = _snakeHead;
            
            MoveSnakeBodies();

            if (_gridsManipulator.CheckTileForObstacle(nextTilePos.x, nextTilePos.y))
            {
                _snakeState = SnakeState.Dead;
                continue;
            }

            if (_gridsManipulator.CheckTileForFruit(nextTilePos.x, nextTilePos.y))
            {
                SpawnSnakeBody();
            
                OnSnakeHitFruit?.Invoke(this, new Vector2Int(nextTilePos.x, nextTilePos.y));
            }
            
            _lastStepMovementDirection = _currentMovementDirection;
            SnakeChangedPosition?.Invoke(this, EventArgs.Empty);
        }
        
        OnSnakeDeath?.Invoke(this, EventArgs.Empty);
    }

    private void SpawnSnakeBody()
    {
        SnakePartsGrid.SnakePartGridObject spawnTile = null;
        SnakePartsGrid.SnakePartGridObject nextBody = null;
        if (!_snakeBodies.Any())
        {
            if (_snakeHead.GetPreviousTile() is not null)
            {
                spawnTile = _snakeHead.GetPreviousTile();
                nextBody = _snakeHead;
            }
        }
        else
        {
            SnakePartsGrid.SnakePartGridObject lastBody = _snakeBodies.Last();
            if (lastBody.GetPreviousTile() is not null)
            {
                spawnTile = lastBody.GetPreviousTile();
                nextBody = lastBody;
            }
        }

        if (spawnTile == null || nextBody == null)
            return;
        
        spawnTile.SetSnakeTileParams(SnakePartsGrid.SnakePartGridObject.TileTypeEnum.SnakePart, null,null, null);
        ConnectTwoSnakeParts(nextBody, spawnTile);
        
        _snakeBodies.Add(spawnTile);
    }
    
    private void MoveSnakeBodies()
    {
        if (_snakeHead.PreviousBody == null)
            return;

        List<SnakePartsGrid.SnakePartGridObject> newSnakeBodiesList = new List<SnakePartsGrid.SnakePartGridObject>();

        SnakePartsGrid.SnakePartGridObject currentSnakeTile = _snakeHead;
        
        // move others bodies to previous position of the next body
        while (currentSnakeTile.PreviousBody != null)
        {
            SnakePartsGrid.SnakePartGridObject previousBodyTile = currentSnakeTile.PreviousBody;
            SnakePartsGrid.SnakePartGridObject previousTile = currentSnakeTile.GetPreviousTile();
            
            //switch current body with previous tile
            SwitchOldToNewTile(ref previousBodyTile, previousTile);
            // Connect current body with previous body new tile
            ConnectTwoSnakeParts(currentSnakeTile, previousBodyTile);
            
            newSnakeBodiesList.Add(previousBodyTile);

            currentSnakeTile = previousBodyTile;
        }
        
        _snakeBodies.Clear();
        _snakeBodies = newSnakeBodiesList;
    }

    private void SwitchOldToNewTile(ref SnakePartsGrid.SnakePartGridObject oldTile,
        SnakePartsGrid.SnakePartGridObject newTile, bool clearOldTile = true)
    {
        var oldTileParams = oldTile.GetSnakeTileParams();
        
        newTile.SetSnakeTileParams(oldTileParams.tileType, oldTileParams.nextBody, oldTileParams.previousBody, oldTile);
        
        if (clearOldTile)
            oldTile.ClearSnakeTileParams();
        
        oldTile = newTile;
    }

    private void ConnectTwoSnakeParts(SnakePartsGrid.SnakePartGridObject nextBody,
        SnakePartsGrid.SnakePartGridObject previousBody)
    {
        nextBody.PreviousBody = previousBody;
        previousBody.NextBody = nextBody;
    }
    
    private Vector2Int Get2DMovementDirection()
    {
        switch (_currentMovementDirection)
        {
            case MovementDirectionEnum.Up:
                return Vector2Int.up;

            case MovementDirectionEnum.Down:
                return Vector2Int.down;

            case MovementDirectionEnum.Left:
                return Vector2Int.left;

            case MovementDirectionEnum.Right:
                return Vector2Int.right;

            default:
                return Vector2Int.zero;
        }
    }

    private MovementDirectionEnum GetOpposingMovementDirection(MovementDirectionEnum direction)
    {
        switch (direction)
        {
            case MovementDirectionEnum.Up:
                return MovementDirectionEnum.Down;
                
            case MovementDirectionEnum.Down:
                return MovementDirectionEnum.Up;
                
            case MovementDirectionEnum.Left:
                return MovementDirectionEnum.Right;
                
            case MovementDirectionEnum.Right:
                return MovementDirectionEnum.Left;
                
            default:
                throw new Exception("Where are you even going?");
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
        _isBoostButtonPressed = false;
    }

    private void OnOnBoostButtonPressed(object sender, EventArgs e)
    {
        _isBoostButtonPressed = true;
    }

    private void SnakeHead_OnRightPressed(object sender, EventArgs e)
    {
        if (GetOpposingMovementDirection(_lastStepMovementDirection) == MovementDirectionEnum.Right)
            return;

        _currentMovementDirection = MovementDirectionEnum.Right;
    }

    private void SnakeHead_OnLeftPressed(object sender, EventArgs e)
    {
        if (GetOpposingMovementDirection(_lastStepMovementDirection) == MovementDirectionEnum.Left)
            return;

        _currentMovementDirection = MovementDirectionEnum.Left;
    }

    private void SnakeHead_OnDownPressed(object sender, EventArgs e)
    {
        if (GetOpposingMovementDirection(_lastStepMovementDirection) == MovementDirectionEnum.Down)
            return;

        _currentMovementDirection = MovementDirectionEnum.Down;
    }

    private void SnakeHead_OnUpPressed(object sender, EventArgs e)
    {
        if (GetOpposingMovementDirection(_lastStepMovementDirection) == MovementDirectionEnum.Up)
            return;

        _currentMovementDirection = MovementDirectionEnum.Up;
    }
    
    protected override void OnDisable()
    {
        base.OnDisable();
        
        OnUpPressed -= SnakeHead_OnUpPressed;
        OnDownPressed -= SnakeHead_OnDownPressed;
        OnLeftPressed -= SnakeHead_OnLeftPressed;
        OnRightPressed -= SnakeHead_OnRightPressed;
        
        OnBoostButtonPressed -= OnOnBoostButtonPressed;
        OnBoostButtonCanceled -= OnOnBoostButtonCanceled;
    }
}
