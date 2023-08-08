using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrid<T>
{
    public void ResetGrid(int gridSizeX, int gridSizeY);

    public void SetGridObjects(IReadOnlyList<T> gridItems);

    public T GetTile(int x, int y);
}
