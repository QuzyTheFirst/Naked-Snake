using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class GridItems : IGrid<GridItem>
{
    private GridItem[,] _grid;
    
    public void ResetGrid(int gridSizeX, int gridSizeY)
    {
        _grid = new GridItem[gridSizeX, gridSizeY];
    }

    public void SetGridObjects(IReadOnlyList<GridItem> gridItems)
    {
        foreach (GridItem gridItem in gridItems)
        {
            _grid[gridItem.X, gridItem.Y] = gridItem;
        }
    }

    public GridItem GetTile(int x, int y)
    {
        if (_grid == null)
            return null;

        if (x < 0 || x >= _grid.GetLength(0) || y < 0 || y >= _grid.GetLength(1))
            return null;
        
        return _grid[x, y];
    }
}
