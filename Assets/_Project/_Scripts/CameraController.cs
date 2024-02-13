using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _cam;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    public void SetupCamera(int gridSizeX, int gridSizeY, float cellSize, Vector3 originPosition)
    {
        Vector3 center = new Vector3(gridSizeX * cellSize * .5f, 10, gridSizeY * cellSize * .5f) - new Vector3(cellSize * 0.5f, 0 , cellSize * 0.5f) + originPosition;
        transform.position = center;

        float horizontalHalfLine = gridSizeX * cellSize * .5f;
        float verticalHalfLine = gridSizeY * cellSize * .5f;
        float longestLine = horizontalHalfLine >= verticalHalfLine ? horizontalHalfLine : verticalHalfLine;

        float hipotenuza = Mathf.Sqrt(longestLine * longestLine);

        _cam.orthographicSize = hipotenuza + .5f;
    }
}