using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridItem : IGridItem<GridItem.ObjectType>
{
    public enum ObjectType
    {
        Fruit,
        Snake
    }
    
    private int _x;
    private int _y;
    
    private ObjectType _value;
    
    public int X => _x;
    public int Y => _y;

    public ObjectType Value => _value;
    
    public void SetItem(int x, int y, ObjectType gridItem)
    {
        _x = x;
        _y = y;
        _value = gridItem;
    }
}
