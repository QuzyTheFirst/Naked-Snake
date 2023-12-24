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
    private LevelEditorMapRulesChecker _levelEditorMapRulesChecker;

    private LevelEditorGrid.LevelEditorGridObject _lastLevelGridObject;
    
    private LevelEditorGrid.LevelEditorGridObject.TileType _currentBrushType;

    private void Awake()
    {
        _levelEditorGrid = new LevelEditorGrid(20, 20 , _cellSize, _originPosition);
        _levelEditorSaveLoad = new LevelEditorSaveLoad();
        _levelEditorMapRulesChecker = new LevelEditorMapRulesChecker();
        _currentBrushType = LevelEditorGrid.LevelEditorGridObject.TileType.Walkable;
    }

    private void Start()
    {
        _levelEditorGrid.SetLevelEditorVisuals(_levelEditorVisuals);
        _levelEditorGrid.SetLevelEditorSaveLoad(_levelEditorSaveLoad);
    }

    public void TrySetupLevelToLoadInfo(LevelToLoadInfo levelToLoadInfo)
    {
        try
        {
            Texture2D levelTexture = _levelEditorSaveLoad.GetTexture2D(true);
            _levelEditorMapRulesChecker.CheckMapForRules(levelTexture);
            Sprite levelSprite = Sprite.Create(levelTexture, new Rect(0, 0, levelTexture.width, levelTexture.height),
                Vector2.zero);
            levelToLoadInfo.SetLevelSprite(levelSprite);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void ChangeBrushType(LevelEditorGrid.LevelEditorGridObject.TileType brushType)
    {
        _currentBrushType = brushType;
    }

    public void SaveMap()
    {
        try
        {
            _levelEditorSaveLoad.Save();
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    public void LoadMap()
    {
        try
        {
            Texture2D map = _levelEditorSaveLoad.Load();
            _levelEditorGrid.SetLevelEditorTiles(map);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
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
