using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrid<T>
{
    public void ResetGrid();

    public void SetGridSize(int gridSizeX, int gridSizeY);

    public void SetGridTiles(IReadOnlyList<T> gridItems);

    public T TryGetTile(int x, int y);

    public bool TrySetTile(T tile);

    public bool TryClearTile(int x, int y);
}
