using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    public enum GridTileType
    {
        WalkingTile,
        DeathTile,
        SpawnTile,
    }

    [SerializeField] private GridTileType _gridTileType;
    private Vector2Int _gridPosition;

    public GridTileType CurrentGridTileType => _gridTileType;
    public Vector2Int GridPosition
    {
        get { return _gridPosition; }
        set { _gridPosition = value; }
    }
}
