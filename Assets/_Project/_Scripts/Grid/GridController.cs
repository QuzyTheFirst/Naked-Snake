using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController
{
    private GridTile[,] _grid;

    private Dictionary<GridTile.TileType, List<GridTile>> _gridTilesDictionary;
    
    public IReadOnlyDictionary<GridTile.TileType, List<GridTile>> GridTilesDictionary => _gridTilesDictionary;

    public GridController()
    {
        _gridTilesDictionary = new Dictionary<GridTile.TileType, List<GridTile>>();
    }
    
    public void SetGrid(int gridSizeX, int gridSizeY, List<GridTile> gridTiles)
    {
        _grid = new GridTile[gridSizeX, gridSizeY];
        _gridTilesDictionary.Clear();
        
        _gridTilesDictionary.Add(GridTile.TileType.DeathTile, new List<GridTile>());
        _gridTilesDictionary.Add(GridTile.TileType.SpawnTile, new List<GridTile>());
        _gridTilesDictionary.Add(GridTile.TileType.WalkingTile, new List<GridTile>());
        
        foreach (GridTile tile in gridTiles)
        {
            _grid[tile.GridPosition.x, tile.GridPosition.y] = tile;
            
            _gridTilesDictionary[tile.CurrentTileType].Add(tile);
        }
    }

    public GridTile GetTile(int x, int y)
    {
        if (_grid == null)
            return null;

        if (x < 0 || x >= _grid.GetLength(0) || y < 0 || y >= _grid.GetLength(1))
            return null;
        
        return _grid[x, y];
    }

    public GridTile GetRandomTile()
    {
        if (_grid == null)
            return null;
        
        int x = Random.Range(0, _grid.GetLength(0));
        int y = Random.Range(0, _grid.GetLength(1));

        return _grid[x, y];
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
