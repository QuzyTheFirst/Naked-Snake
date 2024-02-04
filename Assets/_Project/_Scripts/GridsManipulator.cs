using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class GridsManipulator
{
    private MapTilesGrid _mapTilesGrid;
    private SnakePartsGrid _snakePartsGrid;
    private FruitsGrid _fruitsGrid;

    private Dictionary<MapTilesGrid.MapTileGridObject.TileType, List<MapTilesGrid.MapTileGridObject>> _mapTilesDictionary;

    public GridsManipulator(MapTilesGrid mapTilesGrid, SnakePartsGrid snakePartsGrid, FruitsGrid fruitsGrid)
    {
        _mapTilesGrid = mapTilesGrid;
        _snakePartsGrid = snakePartsGrid;
        _fruitsGrid = fruitsGrid;

        FillMapTilesDictionary();
    }

    private void FillMapTilesDictionary()
    {
        _mapTilesDictionary =
            new Dictionary<MapTilesGrid.MapTileGridObject.TileType, List<MapTilesGrid.MapTileGridObject>>();
        
        foreach (MapTilesGrid.MapTileGridObject.TileType tileType in Enum.GetValues(typeof(MapTilesGrid.MapTileGridObject.TileType)))
        {
            _mapTilesDictionary.Add(tileType, new List<MapTilesGrid.MapTileGridObject>());
        }
        
        for (int x = 0; x <= _mapTilesGrid.GetWidth(); x++)
        {
            for (int y = 0; y <= _mapTilesGrid.GetHeight(); y++)
            {
                MapTilesGrid.MapTileGridObject currentTile = _mapTilesGrid.GetMapTile(x, y);
                
                if(currentTile == null)
                    continue;
                
                if (_mapTilesDictionary.ContainsKey(currentTile.GetTileType()))
                {
                    _mapTilesDictionary[currentTile.GetTileType()].Add(currentTile);
                }
            }
        }
    }
    
    public bool CheckTileForFruit(int x, int y)
    {
        FruitsGrid.FruitGridObject fruitTile = _fruitsGrid.GetFruitTile(new Vector2(x, y));
        return fruitTile.GetTileType() == FruitsGrid.FruitGridObject.TileTypeEnum.Fruit;
    }

    public bool CheckTileForSnake(int x, int y)
    {
        SnakePartsGrid.SnakePartGridObject snakeTile = _snakePartsGrid.GetSnakePartTile(new Vector2(x, y));
        return snakeTile.GetTileType() == SnakePartsGrid.SnakePartGridObject.TileTypeEnum.SnakeHead ||
               snakeTile.GetTileType() == SnakePartsGrid.SnakePartGridObject.TileTypeEnum.SnakePart;
    }

    public bool CheckTileForObstacle(int x, int y)
    {
        return _mapTilesGrid.GetMapTile(new Vector2(x, y)).GetTileType() ==
               MapTilesGrid.MapTileGridObject.TileType.Obstacle;
    }

    public bool CheckTileForWalkableOrSpawnpoint(int x, int y)
    {
        MapTilesGrid.MapTileGridObject.TileType tileType = _mapTilesGrid.GetMapTile(x, y).GetTileType();

        return tileType == MapTilesGrid.MapTileGridObject.TileType.Walkable ||
               tileType == MapTilesGrid.MapTileGridObject.TileType.Spawnpoint;
    }

    public bool CheckTileForEmptyOrNull(int x, int y)
    {
        MapTilesGrid.MapTileGridObject tile = _mapTilesGrid.GetMapTile(x, y);
        
        if (tile == null)
            return true;

        return tile.GetTileType() == MapTilesGrid.MapTileGridObject.TileType.Empty;
    }

    public MapTilesGrid.MapTileGridObject GetRandomSpawnpointTile()
    {
        List<MapTilesGrid.MapTileGridObject> spawnPoints =
            _mapTilesDictionary[MapTilesGrid.MapTileGridObject.TileType.Spawnpoint];
        
        int randomNumber = Random.Range(0, spawnPoints.Count);
        return spawnPoints[randomNumber];
    }

    public MapTilesGrid.MapTileGridObject GetRandomWalkableOrSpawnpointTileWithoutSnake()
    {
        // Get All Acceptable tiles
        List<MapTilesGrid.MapTileGridObject> allPossibleSpawnpoints =
            _mapTilesDictionary[MapTilesGrid.MapTileGridObject.TileType.Spawnpoint];
        allPossibleSpawnpoints.AddRange(_mapTilesDictionary[MapTilesGrid.MapTileGridObject.TileType.Walkable]);

        // Remove Snake Tiles From This List
        SnakePartsGrid.SnakePartGridObject currentSnakePart = null;//_snakePartsGrid.GetSnakeHead();
        while (currentSnakePart != null)
        {
            Vector2Int coordinates = currentSnakePart.GetCoordinates();
            allPossibleSpawnpoints.Remove(_mapTilesGrid.GetMapTile(coordinates.x, coordinates.y));
            currentSnakePart = currentSnakePart.GetPreviousBody();
        }

        // Get Random Tile and Return;
        int randomNumber = Random.Range(0, allPossibleSpawnpoints.Count);
        return allPossibleSpawnpoints[randomNumber];
    }
}
