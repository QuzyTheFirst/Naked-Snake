using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MapTilesGrid
{
    private Grid<MapTileGridObject> _grid;

    public MapTilesGrid(int width, int height, float cellSize, Vector3 originPosition)
    {
        _grid = new Grid<MapTileGridObject>(width, height, cellSize, originPosition, (Grid<MapTileGridObject> grid, int x, int y) => new MapTileGridObject(grid, x, y));
    }

    public int GetHeight()
    {
        return _grid.GetHeight();
    }

    public int GetWidth()
    {
        return _grid.GetWidth();
    }
    
    public void SetMapTilesVisuals(MapTilesVisual mapTilesVisual)
    {
        // TODO: Create SetMapTilesVisuals Class
        mapTilesVisual.SetGrid(_grid);
    }

    public MapTileGridObject GetMapTile(Vector3 worldPosition)
    {
        return _grid.GetGridObject(worldPosition);
    }

    public MapTileGridObject GetMapTile(int x, int y)
    {
        return _grid.GetGridObject(x, y);
    }

    public void SetMapTile(Vector3 worldPosition, MapTileGridObject.TileType tileType)
    {
        MapTileGridObject mapTileGridObject = _grid.GetGridObject(worldPosition);
        if (mapTileGridObject != null)
        {
            mapTileGridObject.SetTileType(tileType);
        }
    }

    public void SetMapTile(int x, int y, MapTileGridObject.TileType tileType)
    {
        MapTileGridObject mapTileGridObject = _grid.GetGridObject(x, y);
        if (mapTileGridObject != null)
        {
            mapTileGridObject.SetTileType(tileType);
        }
    }
    
    public class MapTileGridObject
    {
        public enum TileType
        {
            Obstacle,
            Walkable,
            Spawnpoint,
            Empty
        }

        private TileType _tileType;

        private int _x, _y;

        private Grid<MapTileGridObject> _grid;

        public MapTileGridObject(Grid<MapTileGridObject> grid, int x, int y)
        {
            _tileType = TileType.Empty;
            _grid = grid;
            _x = x;
            _y = y;
        }

        public (int xPos, int yPos) GetCoordinates()
        {
            return (_x, _y);
        }
        
        public void SetTileType(TileType tileType)
        {
            _tileType = tileType;
            _grid.TriggerGridObjectChanged(_x, _y);
        }

        public TileType GetTileType()
        {
            return _tileType;
        }
    }
}
