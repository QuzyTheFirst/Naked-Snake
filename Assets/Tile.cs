using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private GridTile.TileType _currentTileType;

    public GridTile.TileType CurrentTileType => _currentTileType;
}
