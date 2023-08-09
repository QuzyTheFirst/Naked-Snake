using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSnakes : IGrid<GridIntItem>
{
    private GridIntItem[,] _grid;

    private int _gridSizeX;
    private int _gridSizeY;

    public Vector2Int GridSize => new Vector2Int(_gridSizeX, _gridSizeY);
    
    public void ResetGrid()
    {
        _grid = new GridIntItem[_gridSizeX, _gridSizeY];
    }

    public void SetGridSize(int gridSizeX, int gridSizeY)
    {
        _gridSizeX = gridSizeX;
        _gridSizeY = gridSizeY;
        ResetGrid();
    }
    
    public void SetGridTiles(IReadOnlyList<GridIntItem> gridItems)
    {
        foreach (GridIntItem gridItem in gridItems)
        {
            _grid[gridItem.X, gridItem.Y] = gridItem;
        }
    }

    public GridIntItem TryGetTile(int x, int y)
    {
        if (_grid == null)
            return null;

        if (x < 0 || x >= _grid.GetLength(0) || y < 0 || y >= _grid.GetLength(1))
            return null;
        
        return _grid[x, y];
    }

    public bool TrySetTile(GridIntItem tile)
    {
        if (_grid == null)
            return false;

        if (tile.X < 0 || tile.X >= _grid.GetLength(0) || tile.Y < 0 || tile.Y >= _grid.GetLength(1))
            return false;

        _grid[tile.X, tile.Y] = tile;
        return true;
    }
    
    public bool TryClearTile(int x, int y)
    {
        if (_grid == null)
            return false;
        
        if (x < 0 || x >= _grid.GetLength(0) || y < 0 || y >= _grid.GetLength(1))
            return false;

        _grid[x, y] = null;
        return true;
    }
}
