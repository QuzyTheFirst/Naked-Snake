using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePartsVisual : MonoBehaviour
{
    private Grid<SnakePartsGrid.SnakePartGridObject> _grid;
    private bool _updateSnake = false;

    [SerializeField] private Transform _snakeHeadPf;
    [SerializeField] private Transform _snakeBodyPf;

    private List<Transform> _allSnakeParts;

    private void Awake()
    {
        _allSnakeParts = new List<Transform>();
    }
    
    public void SetGrid(Grid<SnakePartsGrid.SnakePartGridObject> grid)
    {
        _grid = grid;

        UpdateLevelEditorVisual();

        _grid.OnGridObjectChanged += OnGridObjectChanged;
    }
    
    private void OnGridObjectChanged(object sender, Grid<SnakePartsGrid.SnakePartGridObject>.OnGridObjectChangedEventArgs e)
    {
        _updateSnake = true;
    }
    
    private void LateUpdate()
    {
        if (_updateSnake)
        {
            _updateSnake = false;
            UpdateLevelEditorVisual();
        }
    }
    
    private void UpdateLevelEditorVisual()
    {
        DeleteAllSnakes();
        
        _allSnakeParts.Clear();
        
        for(int x = 0; x < _grid.GetWidth(); x++)
        {
            for(int y = 0; y < _grid.GetHeight(); y++)
            {
                int index = x * _grid.GetHeight() + y;

                SnakePartsGrid.SnakePartGridObject.TileTypeEnum tileType = _grid.GetGridObject(x, y).GetTileType();
                switch (tileType)
                {
                    case SnakePartsGrid.SnakePartGridObject.TileTypeEnum.SnakeHead:
                        _allSnakeParts.Add(Instantiate(_snakeHeadPf,
                            new Vector3(x, 0.25f, y) * _grid.GetCellSize() + _grid.GetOriginPosition(),
                            Quaternion.identity));
                        break;
                    case SnakePartsGrid.SnakePartGridObject.TileTypeEnum.SnakePart:
                        _allSnakeParts.Add(Instantiate(_snakeBodyPf,
                            new Vector3(x, 0.25f, y) * _grid.GetCellSize() + _grid.GetOriginPosition(),
                            Quaternion.identity));
                        break;
                }
            }
        }
    }
    
    private void DeleteAllSnakes()
    {
        foreach (Transform snake in _allSnakeParts)
        {
            Destroy(snake.gameObject);
        }
    }
}
