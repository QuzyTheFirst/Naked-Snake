using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    [SerializeField] private LevelEditorVisual _levelEditorVisuals;
    [SerializeField] private float _cellSize = 1;
    [SerializeField] private Vector3 _originPosition = Vector3.zero;
    
    private LevelEditorGrid _levelEditorGrid;
    private LevelEditorSaveLoad _levelEditorSaveLoad;

    private LevelEditorGrid.LevelEditorGridObject _lastLevelGridObject;
    
    private LevelEditorGrid.LevelEditorGridObject.TileType _currentBrushType;

    private void Awake()
    {
        _levelEditorGrid = new LevelEditorGrid(20, 20 , _cellSize, _originPosition);
        _levelEditorSaveLoad = new LevelEditorSaveLoad();
        _currentBrushType = LevelEditorGrid.LevelEditorGridObject.TileType.Walkable;
    }

    private void Start()
    {
        _levelEditorGrid.SetLevelEditorVisuals(_levelEditorVisuals);
        _levelEditorGrid.SetLevelEditorSaveLoad(_levelEditorSaveLoad);
    }

    public void SetupLevelToLoadInfo(LevelToLoadInfo levelToLoadInfo)
    {
        Texture2D levelTexture = _levelEditorSaveLoad.GetTexture2D(true);
        Sprite levelSprite = Sprite.Create(levelTexture, new Rect(0, 0, levelTexture.width, levelTexture.height), Vector2.zero);
        levelToLoadInfo.SetLevelSprite(levelSprite);
    }

    public void ChangeBrushType(LevelEditorGrid.LevelEditorGridObject.TileType brushType)
    {
        _currentBrushType = brushType;
    }

    public void SaveMap()
    {
        _levelEditorSaveLoad.Save();
    }

    public void LoadMap()
    {
        Texture2D map = _levelEditorSaveLoad.Load();
        _levelEditorGrid.SetLevelEditorTiles(map);
    }
    
    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Vector2 mousePosition = UtilsClass.GetMouseWorldPosition(10);
            LevelEditorGrid.LevelEditorGridObject gridObject = _levelEditorGrid.GetLevelEditorTile(mousePosition);
            if (gridObject == null || gridObject.GetTileType() == _currentBrushType)
                return;
            gridObject.SetTileType(_currentBrushType);
            _lastLevelGridObject = gridObject;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeBrushType(LevelEditorGrid.LevelEditorGridObject.TileType.Obstacle);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            ChangeBrushType(LevelEditorGrid.LevelEditorGridObject.TileType.Spawnpoint);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeBrushType(LevelEditorGrid.LevelEditorGridObject.TileType.Walkable);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeBrushType(LevelEditorGrid.LevelEditorGridObject.TileType.Empty);
        }
    }
}
