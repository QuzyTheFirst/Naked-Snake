using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridsManipulator
{
    private GridTiles _gridTiles;
    private GridSnakes _gridSnakes;
    private GridItems _gridItems;

    public GridTiles GridTiles => _gridTiles;
    public GridSnakes GridSnakes => _gridSnakes;
    public GridItems GridItems => _gridItems;
    
    public GridsManipulator(GridTiles gridTiles, GridSnakes gridSnakes, GridItems gridItems)
    {
        _gridTiles = gridTiles;
        _gridSnakes = gridSnakes;
        _gridItems = gridItems;
    }

    public void ResetGrids()
    {
        _gridTiles.ResetGrid();
        _gridSnakes.ResetGrid();
        _gridItems.ResetGrid();
    }

    public void SetGridsSize(int gridSizeX, int gridSizeY)
    {
        _gridTiles.SetGridSize(gridSizeX, gridSizeY);
        _gridSnakes.SetGridSize(gridSizeX, gridSizeY);
        _gridItems.SetGridSize(gridSizeX, gridSizeY);
    }
    
    public bool CheckTileForSnake(int x, int y)
    {
        GridIntItem snakeTile = _gridSnakes.TryGetTile(x, y);

        if (snakeTile == null)
            return false;
        
        if (snakeTile.Value != 1)
            return false;
        
        return true;
    }

    public bool CheckTileForFruit(int x, int y)
    {
        GridTransformItem fruitTile = _gridItems.TryGetTile(x, y);

        if (fruitTile == null)
            return false;
        
        if (fruitTile.Value == null)
            return false;
        
        return true;
    }
}
