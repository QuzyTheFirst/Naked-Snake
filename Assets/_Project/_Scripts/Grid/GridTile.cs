using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GridTile : MonoBehaviour
{
    public enum TileType
    {
        WalkingTile,
        DeathTile,
        SpawnTile,
    }

    [SerializeField] private TileType tileType;
    private Vector2Int _gridPosition;

    public TileType CurrentTileType => tileType;
    public Vector2Int GridPosition
    {
        get { return _gridPosition; }
        set { _gridPosition = value; }
    }
}
