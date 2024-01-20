using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorVisual : MonoBehaviour
{
    private MeshFilter _meshFilter;

    private Grid<LevelEditorGrid.LevelEditorGridObject> _grid;
    private Mesh _mesh;
    private bool _updateMesh = false;

    private void Awake()
    {
        _mesh = new Mesh();

        _meshFilter = GetComponent<MeshFilter>();
        _meshFilter.mesh = _mesh;
    }

    public void SetGrid(Grid<LevelEditorGrid.LevelEditorGridObject> grid)
    {
        _grid = grid;

        UpdateLevelEditorVisual();

        _grid.OnGridObjectChanged += _grid_OnGridObjectChanged;
    }

    private void _grid_OnGridObjectChanged(object sender, Grid<LevelEditorGrid.LevelEditorGridObject>.OnGridObjectChangedEventArgs e)
    {
        _updateMesh = true;
    }

    private void LateUpdate()
    {
        if (_updateMesh)
        {
            _updateMesh = false;
            UpdateLevelEditorVisual();
        }
    }

    private void UpdateLevelEditorVisual()
    {
        MeshUtils.CreateSquareGrid(_grid.GetWidth(), _grid.GetHeight(), _grid.GetOriginPosition(), _grid.GetCellSize(), out Vector3[] vertices, out Vector2[] uvs, out int[] triangles);

        for(int x = 0; x < _grid.GetWidth(); x++)
        {
            for(int y = 0; y < _grid.GetHeight(); y++)
            {
                int index = x * _grid.GetHeight() + y;

                LevelEditorGrid.LevelEditorGridObject levelEditorGridObject = _grid.GetGridObject(x, y);
                switch (levelEditorGridObject.GetTileType())
                {
                    case LevelEditorGrid.LevelEditorGridObject.TileType.Eraser:
                        MeshUtils.ChangeSquareGridUvsToCenterPoint(index, 4, 1, 4, 1, ref uvs);
                        break;
                    case LevelEditorGrid.LevelEditorGridObject.TileType.Obstacle:
                        MeshUtils.ChangeSquareGridUvsToCenterPoint(index, 4, 1, 1, 1, ref uvs);
                        break;
                    case LevelEditorGrid.LevelEditorGridObject.TileType.Spawnpoint:
                        MeshUtils.ChangeSquareGridUvsToCenterPoint(index, 4, 1, 2, 1, ref uvs);
                        break;
                    case LevelEditorGrid.LevelEditorGridObject.TileType.Walkable:
                        MeshUtils.ChangeSquareGridUvsToCenterPoint(index, 4, 1, 3, 1, ref uvs);
                        break;
                }
            }
        }
        
        _mesh.vertices = vertices;
        _mesh.uv = uvs;
        _mesh.triangles = triangles;   
    }
}
