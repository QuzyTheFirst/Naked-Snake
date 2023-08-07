using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController
{
    private GridTile[,] _grid;

    private List<GridTile> _spawnGridTiles;

    public IReadOnlyList<GridTile> SpawnTiles => _spawnGridTiles;

    public GridController()
    {
        _spawnGridTiles = new List<GridTile>();
    }
    
    public void SetGrid(int gridSizeX, int gridSizeY, List<GridTile> gridTiles)
    {
        _grid = new GridTile[gridSizeX, gridSizeY];
        _spawnGridTiles.Clear();
        
        foreach (GridTile tile in gridTiles)
        {
            _grid[tile.GridPosition.x, tile.GridPosition.y] = tile;

            if (tile.CurrentGridTileType == GridTile.GridTileType.SpawnTile)
            {
                Debug.Log("Added Spawn Tile at position: " + tile.GridPosition);
                _spawnGridTiles.Add(tile);
            }
        }
    }

    public GridTile GetTile(int x, int y)
    {
        return _grid[x, y];
    }

    public GridTile GetRandomTile()
    {
        int x = Random.Range(0, _grid.GetLength(0));
        int y = Random.Range(0, _grid.GetLength(1));

        return _grid[x, y];
    }

    public GridTile GetRandomSpawnTile()
    {
        int randomNumber = Random.Range(0, _spawnGridTiles.Count);
        Debug.Log("Spawn Tile Grid Position: " + _spawnGridTiles[randomNumber].GridPosition);
        return _spawnGridTiles[randomNumber];
    }
}
