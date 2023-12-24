using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class LevelEditorGrid
{
    private Grid<LevelEditorGridObject> _grid;

    public LevelEditorGrid(int width, int height, float cellsize, Vector3 originPosition)
    {
        _grid = new Grid<LevelEditorGridObject>(width, height, cellsize, originPosition, (Grid<LevelEditorGridObject> grid, int x, int y) => new LevelEditorGridObject(grid, x, y));
    }

    public void SetLevelEditorVisuals(LevelEditorVisual levelEditorVisual)
    {
        levelEditorVisual.SetGrid(_grid);
    }

    public void SetLevelEditorSaveLoad(LevelEditorSaveLoad levelEditorSaveLoad)
    {
        levelEditorSaveLoad.SetGrid(_grid);
    }
    
    public LevelEditorGridObject GetLevelEditorTile(Vector3 worldPosition)
    {
        return _grid.GetGridObject(worldPosition);
    }

    public void SetLevelEditorTile(Vector3 worldPosition, LevelEditorGridObject.TileType tiletype)
    {
        LevelEditorGridObject levelEditorObject = _grid.GetGridObject(worldPosition);
        if (levelEditorObject != null)
        {
            levelEditorObject.SetTileType(tiletype);
        }
    }

    public void SetLevelEditorTiles(Texture2D map)
    {
        for (int x = 0; x < _grid.GetWidth(); x++)
        {
            for (int y = 0; y < _grid.GetHeight(); y++)
            {
                LevelEditorGridObject gridObject = _grid.GetGridObject(x, y);
                Color pixel = map.GetPixel(x, y);
                if (map.GetPixel(x, y) == Color.red)
                {
                    gridObject.SetTileType(LevelEditorGridObject.TileType.Obstacle);
                }
                else if (pixel == Color.green)
                {
                    gridObject.SetTileType(LevelEditorGridObject.TileType.Spawnpoint);
                }
                else if (pixel == Color.white)
                {
                    gridObject.SetTileType(LevelEditorGridObject.TileType.Walkable);
                }
                else if (pixel == Color.clear)
                {
                    gridObject.SetTileType(LevelEditorGridObject.TileType.Empty);
                }
            }
        }
    }
    
    public class LevelEditorGridObject
    {
        public enum TileType
        {
            Obstacle,
            Walkable,
            Spawnpoint,
            Empty,
        }

        private TileType _tileType;

        private int _x;
        private int _y;

        private Grid<LevelEditorGridObject> _grid;

        public LevelEditorGridObject(Grid<LevelEditorGridObject> grid, int x, int y)
        {
            _tileType = TileType.Empty;
            _grid = grid;
            _x = x;
            _y = y;
        }

        public void SetTileType(TileType tiletype)
        {
            _tileType = tiletype;
            _grid.TriggerGridObjectChanged(_x, _y);
        }

        public TileType GetTileType()
        {
            return _tileType;
        }
    }
}
