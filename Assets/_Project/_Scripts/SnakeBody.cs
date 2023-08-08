using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeBody
{
    private Vector2Int _gridPosition;
    private Transform _transform;

    public Vector2Int GridPosition => _gridPosition;
    public Transform Transform => _transform;

    public SnakeBody(int x, int y, Transform transform)
    {
        _gridPosition = new Vector2Int(x, y);
        _transform = transform;
    }

    public SnakeBody(Vector2Int gridPosition, Transform transform)
    {
        _gridPosition = gridPosition;
        _transform = transform;
    }

    public void SetGridPosition(int x, int y)
    {
        _gridPosition = new Vector2Int(x, y);
    }

    public void SetGridPosition(Vector2Int position)
    {
        _gridPosition = position;
    }
}
