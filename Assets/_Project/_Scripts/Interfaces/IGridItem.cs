using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGridItem<T>
{
    public void SetItem(int x, int y, T gridItem);
}
