using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsVisual : MonoBehaviour
{
    private Grid<FruitsGrid.FruitGridObject> _grid;
    private bool _updateFruits = false;

    [SerializeField] private Transform _fruitPf;

    private List<Transform> _allFruits;

    private void Awake()
    {
        _allFruits = new List<Transform>();
    }
    
    public void SetGrid(Grid<FruitsGrid.FruitGridObject> grid)
    {
        _grid = grid;

        UpdateMapVisual();

        _grid.OnGridObjectChanged += OnGridObjectChanged;
    }
    
    private void OnGridObjectChanged(object sender, Grid<FruitsGrid.FruitGridObject>.OnGridObjectChangedEventArgs e)
    {
        _updateFruits = true;
    }
    
    private void LateUpdate()
    {
        if (_updateFruits)
        {
            _updateFruits = false;
            UpdateMapVisual();
        }
    }
    
    private void UpdateMapVisual()
    {
        DeleteAllFruits();
        
        _allFruits.Clear();
        
        for(int x = 0; x < _grid.GetWidth(); x++)
        {
            for(int y = 0; y < _grid.GetHeight(); y++)
            {
                int index = x * _grid.GetHeight() + y;
                switch (_grid.GetGridObject(x, y).GetTileType())
                {
                    case FruitsGrid.FruitGridObject.TileTypeEnum.Fruit:
                        _allFruits.Add(Instantiate(_fruitPf, new Vector3(x, 0.25f, y) * _grid.GetCellSize() + _grid.GetOriginPosition(), Quaternion.LookRotation(Vector3.down)));
                        break;
                }
            }
        }
    }

    private void DeleteAllFruits()
    {
        foreach (Transform tile in _allFruits)
        {
            Destroy(tile.gameObject);
        }
    }
}
