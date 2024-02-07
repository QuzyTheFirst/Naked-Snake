using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SnakePartsGrid
{
    public event EventHandler<SnakePartGridObject> OnSnakeHeadChanged; 
    
    private readonly Grid<SnakePartGridObject> _grid;
    private SnakePartGridObject _snakeHead;
    
    public SnakePartGridObject SnakeHead
    {
        get => _snakeHead;
        set
        {
            _snakeHead = value;
            OnSnakeHeadChanged?.Invoke(this, _snakeHead);
        }
    }
    public SnakePartsGrid(int width, int height, float cellSize, Vector3 originPosition)
    {
        _grid = new Grid<SnakePartGridObject>(width, height, cellSize, originPosition, (Grid<SnakePartGridObject> grid, int x, int y) => new SnakePartGridObject(grid, x, y));
    }

    public void SetSnakePartsVisuals(SnakePartsVisual snakePartsVisual)
    {
        // TODO: Create SnakePartsVisuals Class
        snakePartsVisual.SetGrid(_grid);
    }

    public SnakePartGridObject GetSnakePartTile(Vector3 worldPosition)
    {
        return _grid.GetGridObject(worldPosition);
    }

    public SnakePartGridObject GetSnakePartTile(int x, int y)
    {
        return _grid.GetGridObject(x, y);
    }
    
    public class SnakePartGridObject
    {
        public enum TileTypeEnum
        {
            SnakeHead,
            SnakePart,
            Empty
        }

        private TileTypeEnum _tileType;
        
        private SnakePartGridObject _nextBody;
        private SnakePartGridObject _previousBody;
        private SnakePartGridObject _previousTile;

        private float _yRotation;
        
        private int _x, _y;

        private Grid<SnakePartGridObject> _grid;
        
        public SnakePartGridObject NextBody
        {
            get => _nextBody;
            set => _nextBody = value;
        }

        public SnakePartGridObject PreviousBody
        {
            get => _previousBody;
            set => _previousBody = value;
        }

        public SnakePartGridObject(Grid<SnakePartGridObject> grid, int x, int y)
        {
            _grid = grid;
            _x = x;
            _y = y;

            _tileType = TileTypeEnum.Empty;
            _nextBody = default;
            _previousBody = default;
            _previousTile = default;
        }

        public void SetSnakeTileParams(TileTypeEnum tileType, SnakePartGridObject nextBody,
            SnakePartGridObject previousBody, SnakePartGridObject previousTile)
        {
            _tileType = tileType;
            _nextBody = nextBody;
            _previousBody = previousBody;
            _previousTile = previousTile;
            _grid.TriggerGridObjectChanged(_x, _y);
        }
        
        public (TileTypeEnum tileType, SnakePartGridObject nextBody, SnakePartGridObject previousBody, SnakePartGridObject previousTile) GetSnakeTileParams()
        {
            return (_tileType, _nextBody, _previousBody, _previousTile);
        }
        
        public void ClearSnakeTileParams()
        {
            _tileType = TileTypeEnum.Empty;
            _nextBody = default;
            _previousBody = default;
            _previousTile = default;
            _grid.TriggerGridObjectChanged(_x, _y);
        }
        
        public SnakePartGridObject GetPreviousTile()
        {
            return _previousTile;
        }
        
        public Vector2Int GetCoordinates()
        {
            return new Vector2Int(_x, _y);
        }

        public TileTypeEnum GetTileType()
        {
            return _tileType;
        }
        
    }
}
