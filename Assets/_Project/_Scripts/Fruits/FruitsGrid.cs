using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsGrid
{
    private Grid<FruitGridObject> _grid;
    
    public FruitsGrid(int width, int height, float cellsize, Vector3 originPosition)
    {
        _grid = new Grid<FruitGridObject>(width, height, cellsize, originPosition, (Grid<FruitGridObject> grid, int x, int y) => new FruitGridObject(grid, x, y));
    }

    public void SetFruitsGridVisuals(FruitsVisual fruitsVisual)
    {
        fruitsVisual.SetGrid(_grid);
    }
    
    public FruitGridObject GetFruitTile(Vector3 worldPosition)
    {
        return _grid.GetGridObject(worldPosition);
    }

    public FruitGridObject GetFruitTile(int x, int y)
    {
        return _grid.GetGridObject(x, y);
    }

    public void SetFruitTile(Vector3 worldPosition, FruitGridObject.TileTypeEnum tileType)
    {
        FruitGridObject levelEditorObject = _grid.GetGridObject(worldPosition);
        if (levelEditorObject != null)
        {
            levelEditorObject.SetTileType(tileType);
        }
    }

    
    
    public class FruitGridObject
    {
        public enum TileTypeEnum
        {
            Fruit,
            Empty
        }

        private TileTypeEnum _tileType;
        
        private int _x;
        private int _y;

        private Grid<FruitGridObject> _grid;

        public FruitGridObject(Grid<FruitGridObject> grid, int x, int y)
        {
            _grid = grid;
            _x = x;
            _y = y;

            _tileType = TileTypeEnum.Empty;
        }

        public void SetTileType(TileTypeEnum tileType)
        {
            _tileType = tileType;
            _grid.TriggerGridObjectChanged(_x, _y);
        }

        public void ClearTile()
        {
            _tileType = TileTypeEnum.Empty;
            _grid.TriggerGridObjectChanged(_x, _y);
        }
        
        public TileTypeEnum GetTileType()
        {
            return _tileType;
        }
    }
}
