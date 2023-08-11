using System.Collections.Generic;
using UnityEngine;

public class GridTiles : IGrid<GridTile>
{
    private GridTile[,] _grid;

    private Dictionary<GridTile.TileType, List<GridTile>> _gridTilesDictionary;

    private int _gridSizeX;
    private int _gridSizeY;
    
    public IReadOnlyDictionary<GridTile.TileType, List<GridTile>> GridTilesDictionary => _gridTilesDictionary;

    public Vector2Int GridSize => new Vector2Int(_gridSizeX, _gridSizeY);
    
    public GridTiles()
    {
        _gridTilesDictionary = new Dictionary<GridTile.TileType, List<GridTile>>();
    }
    
    public void ResetGrid()
    {
        _grid = new GridTile[_gridSizeX, _gridSizeY];
        _gridTilesDictionary.Clear();
        
        _gridTilesDictionary.Add(GridTile.TileType.DeathTile, new List<GridTile>());
        _gridTilesDictionary.Add(GridTile.TileType.SpawnTile, new List<GridTile>());
        _gridTilesDictionary.Add(GridTile.TileType.WalkingTile, new List<GridTile>());
    }

    public void SetGridSize(int gridSizeX, int gridSizeY)
    {
        _gridSizeX = gridSizeX;
        _gridSizeY = gridSizeY;
        ResetGrid();
    }
    
    public void SetGridTiles(IReadOnlyList<GridTile> gridTiles)
    {
        foreach (GridTile gridTile in gridTiles)
        {
            _grid[gridTile.X, gridTile.Y] = gridTile;

            _gridTilesDictionary[gridTile.CurrentTileType].Add(gridTile);
        }
    }

    public GridTile TryGetTile(int x, int y)
    {
        if (_grid == null)
            return null;

        if (x < 0 || x >= _grid.GetLength(0) || y < 0 || y >= _grid.GetLength(1))
            return null;
        
        return _grid[x, y];
    }

    public bool TrySetTile(GridTile tile)
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

    public GridTile GetRandomSpawnTile()
    {
        if (_grid == null)
            return null;

        List<GridTile> spawnTiles = _gridTilesDictionary[GridTile.TileType.SpawnTile];
        
        int randomNumber = Random.Range(0, spawnTiles.Count);
        
        return spawnTiles[randomNumber];
    }

    public GridTile GetRandomWalkableTile()
    {
        if (_grid == null)
            return null;

        List<GridTile> walkableTiles = _gridTilesDictionary[GridTile.TileType.WalkingTile];
        
        int randomNumber = Random.Range(0, walkableTiles.Count);
        
        return walkableTiles[randomNumber];
    }
    
}
