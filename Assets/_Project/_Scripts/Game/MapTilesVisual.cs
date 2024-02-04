using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTilesVisual : MonoBehaviour
{
    private Grid<MapTilesGrid.MapTileGridObject> _grid;
    private bool _updateMap = false;

    [SerializeField] private Transform _obstacleTilePf;
    [SerializeField] private Transform _spawnpointTilePf;
    [SerializeField] private Transform _walkableTilePf;
    
    private List<Transform> _allTiles;
    
    private void Awake()
    {
        _allTiles = new List<Transform>();
    }
    
    public void SetGrid(Grid<MapTilesGrid.MapTileGridObject> grid)
    {
        _grid = grid;

        UpdateMapVisual();

        _grid.OnGridObjectChanged += OnGridObjectChanged;
    }
    
    private void OnGridObjectChanged(object sender, Grid<MapTilesGrid.MapTileGridObject>.OnGridObjectChangedEventArgs e)
    {
        _updateMap = true;
    }
    
    private void LateUpdate()
    {
        if (_updateMap)
        {
            _updateMap = false;
            UpdateMapVisual();
        }
    }
    
    private void UpdateMapVisual()
    {
        DeleteAllTiles();
        
        _allTiles.Clear();
        
        for(int x = 0; x < _grid.GetWidth(); x++)
        {
            for(int y = 0; y < _grid.GetHeight(); y++)
            {
                int index = x * _grid.GetHeight() + y;
                switch (_grid.GetGridObject(x, y).GetTileType())
                {
                    case MapTilesGrid.MapTileGridObject.TileType.Obstacle:
                        _allTiles.Add(Instantiate(_obstacleTilePf, new Vector3(x, 0, y) * _grid.GetCellSize() + _grid.GetOriginPosition(), Quaternion.identity));
                        break;
                    case MapTilesGrid.MapTileGridObject.TileType.Spawnpoint:
                        _allTiles.Add(Instantiate(_spawnpointTilePf, new Vector3(x, 0, y) * _grid.GetCellSize() + _grid.GetOriginPosition(), Quaternion.identity));
                        break;
                    case MapTilesGrid.MapTileGridObject.TileType.Walkable:
                        _allTiles.Add(Instantiate(_walkableTilePf, new Vector3(x, 0, y) * _grid.GetCellSize() + _grid.GetOriginPosition(), Quaternion.identity));
                        break;
                }
            }
        }
    }

    private void DeleteAllTiles()
    {
        foreach (Transform tile in _allTiles)
        {
            Destroy(tile.gameObject);
        }
    }
}
