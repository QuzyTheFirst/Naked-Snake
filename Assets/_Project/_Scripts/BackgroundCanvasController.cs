using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCanvasController : MonoBehaviour
{
    public void SetupAnimation(int gridSizeX, int gridSizeY, float cellSize, Vector3 originPosition)
    {
        Vector3 center = new Vector3(gridSizeX * cellSize * .5f, -1f, gridSizeY * cellSize * .5f) - new Vector3(cellSize * 0.5f, 0, cellSize * 0.5f) + originPosition;
        transform.position = center;

        float horizontalLine = gridSizeX * cellSize;
        float verticalLine = gridSizeY * cellSize;
        float longestLine = horizontalLine >= verticalLine ? horizontalLine : verticalLine;

        transform.position = center;
        transform.localScale = new Vector3(longestLine + 1.25f, longestLine + 1.25f, 0);
    }
}
