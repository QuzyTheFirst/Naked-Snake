using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelEditor : LevelEditorInputHandler
{
    [SerializeField] private LevelEditorVisual _levelEditorVisuals;
    [SerializeField] private float _cellSize = 1;
    [SerializeField] private Vector3 _originPosition = Vector3.zero;
    
    private LevelEditorGrid _levelEditorGrid;
    private LevelEditorSaveLoad _levelEditorSaveLoad;
    private LevelEditorMapRulesChecker _levelEditorMapRulesChecker;

    private LevelEditorGrid.LevelEditorGridObject _lastLevelGridObject;
    
    private LevelEditorGrid.LevelEditorGridObject.TileType _currentBrushType;

    private Coroutine _drawingCoroutine;

    protected override void Awake()
    {
        base.Awake();
        
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
        catch
        {
            throw;
        }
    }

    public void ChangeBrushType(LevelEditorGrid.LevelEditorGridObject.TileType brushType)
    {
        _currentBrushType = brushType;
    }

    public void TrySaveMap()
    {
        try
        {
            _levelEditorSaveLoad.Save();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public void TryLoadMap()
    {
        try
        {
            Texture2D map = _levelEditorSaveLoad.Load();
            _levelEditorGrid.SetLevelEditorTiles(map);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    
    protected override void OnEnable()
    {
        base.OnEnable();
        
        OnObstaclePerformed +=LevelEditor_OnObstaclePerformed;
        OnSpawnpointPerformed += LevelEditor_OnSpawnpointPerformed;
        OnWalkablePerformed += LevelEditor_OnWalkablePerformed;
        OnEraserPerformed += LevelEditor_OnEraserPerformed;
        
        OnMousePerformed += LevelEditor_OnMousePerformed;
        OnMouseCanceled += LevelEditor_OnMouseCanceled;
    }

    private void LevelEditor_OnObstaclePerformed(object sender, EventArgs e)
    {
        ChangeBrushType(LevelEditorGrid.LevelEditorGridObject.TileType.Obstacle);
    }

    private void LevelEditor_OnSpawnpointPerformed(object sender, EventArgs e)
    {
        ChangeBrushType(LevelEditorGrid.LevelEditorGridObject.TileType.Spawnpoint);
    }

    private void LevelEditor_OnWalkablePerformed(object sender, EventArgs e)
    {
        ChangeBrushType(LevelEditorGrid.LevelEditorGridObject.TileType.Walkable);
    }

    private void LevelEditor_OnEraserPerformed(object sender, EventArgs e)
    {
        ChangeBrushType(LevelEditorGrid.LevelEditorGridObject.TileType.Eraser);
    }
    
    private void LevelEditor_OnMousePerformed(object sender, EventArgs e)
    {
        _drawingCoroutine = StartCoroutine(MouseDrawing());
    }

    IEnumerator MouseDrawing()
    {
        while (true)
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            Vector2 mouseWorldPosition = UtilsClass.GetMouseWorldPosition(mousePosition,10);
            
            LevelEditorGrid.LevelEditorGridObject gridObject = _levelEditorGrid.GetLevelEditorTile(mouseWorldPosition);
            if (gridObject == null || gridObject.GetTileType() == _currentBrushType)
            {
                yield return null;
                continue;
            }

            gridObject.SetTileType(_currentBrushType);
            _lastLevelGridObject = gridObject;
            
            yield return null;
        }
    }
    
    private void LevelEditor_OnMouseCanceled(object sender, EventArgs e)
    {
        StopCoroutine(_drawingCoroutine);
    }
    
    protected override void OnDisable()
    {
        base.OnDisable();
        
        OnObstaclePerformed -= LevelEditor_OnObstaclePerformed;
        OnSpawnpointPerformed -= LevelEditor_OnSpawnpointPerformed;
        OnWalkablePerformed -= LevelEditor_OnWalkablePerformed;
        OnEraserPerformed -= LevelEditor_OnEraserPerformed;
        
        OnMousePerformed -= LevelEditor_OnMousePerformed;
        OnMouseCanceled -= LevelEditor_OnMouseCanceled;
    }
}
