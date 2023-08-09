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

    public void SetNewCameraPos(int gridSizeX, int gridSizeY)
    {
        Vector3 center = new Vector3(gridSizeX * .5f, 10, gridSizeY * .5f) - (Vector3.one * .5f);
        transform.position = center * LevelGenerator.DistanceBetweenTiles;

        float bottomLineX = gridSizeX * .5f * LevelGenerator.DistanceBetweenTiles;
        float bottomLineY = gridSizeY * .5f * LevelGenerator.DistanceBetweenTiles;
        float bottomLine = bottomLineX > bottomLineY ? bottomLineX : bottomLineY;

        float hipotenuza = Mathf.Sqrt(bottomLine * bottomLine);

        _cam.orthographicSize = hipotenuza + .5f;
    }
}
