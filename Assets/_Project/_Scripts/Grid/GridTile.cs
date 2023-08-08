using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GridTile : IGridItem<GridTile.TileType>
{
    public enum TileType
    {
        WalkingTile,
        DeathTile,
        SpawnTile,
    }
    
    private int _x;
    private int _y;
    
    private TileType _currentTileType;
    
    public int X => _x;
    public int Y => _y;
    
    public TileType CurrentTileType => _currentTileType;
    
    public void SetItem(int x, int y, TileType gridItem)
    {
        _x = x;
        _y = y;
        _currentTileType = gridItem;
    }
}
