using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTransformItem : IGridItem<Transform>
{
    private int _x;
    private int _y;
    
    private Transform _value;
    
    public int X => _x;
    public int Y => _y;

    public Transform Value => _value;
    
    public void SetItem(int x, int y, Transform gridItem)
    {
        _x = x;
        _y = y;
        _value = gridItem;
    }
}
