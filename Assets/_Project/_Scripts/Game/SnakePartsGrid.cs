using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SnakePartsGrid
{
    private Grid<SnakePartGridObject> _grid;
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
        
        private int _x, _y;

        private Grid<SnakePartGridObject> _grid;

        public SnakePartGridObject(Grid<SnakePartGridObject> grid, int x, int y)
        {
            _grid = grid;
            _x = x;
            _y = y;

            _tileType = TileTypeEnum.Empty;
            _nextBody = null;
            _previousBody = null;
        }

        public void SetSnakeTileParams(TileTypeEnum tileType, SnakePartGridObject nextBody,
            SnakePartGridObject previousBody)
        {
            _tileType = tileType;
            _nextBody = nextBody;
            _previousBody = previousBody;
            _grid.TriggerGridObjectChanged(_x, _y);
        }

        public (TileTypeEnum tileType, SnakePartGridObject nextBody, SnakePartGridObject previousBody)
            GetSnakeTileParams()
        {
            return (_tileType, _nextBody, _previousBody);
        }
        
        public void ClearSnakeTileParams()
        {
            _tileType = TileTypeEnum.Empty;
            _nextBody = null;
            _previousBody = null;
            _grid.TriggerGridObjectChanged(_x, _y);
        }

        public SnakePartGridObject GetNextBody()
        {
            return _nextBody;
        }

        public SnakePartGridObject GetPreviousBody()
        {
            return _previousBody;
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
