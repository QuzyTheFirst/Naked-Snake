using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridIntItem : IGridItem<int>
{
    private int _x;
    private int _y;
    
    private int _value;
    
    public int X => _x;
    public int Y => _y;

    public int Value => _value;
    
    public void SetItem(int x, int y, int gridItem)
    {
        _x = x;
        _y = y;
        _value = gridItem;
    }
}
