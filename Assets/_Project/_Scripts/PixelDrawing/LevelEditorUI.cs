using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEditorUI : UIInputHandler
{
    [SerializeField] private LevelEditor _levelEditor;
    [SerializeField] private TextMeshProUGUI _errorLogger;
    [SerializeField] private GameObject[] _saveLoadButtons;

    private LevelLoader _levelLoader;

#if UNITY_WEBGL && !UNITY_EDITOR

    private void Start()
    {
        foreach (GameObject button in _saveLoadButtons)
        {
            button.SetActive(false);
        }
    }

#endif

    public void Initialize(LevelLoader levelLoader)
    {
        _levelLoader = levelLoader;
    }
    
    public void Play()
    {
        try
        {
            LevelToLoadInfo levelToLoadInfo = FindObjectOfType<LevelToLoadInfo>();
            _levelEditor.TrySetupLevelToLoadInfo(levelToLoadInfo);
            _levelLoader.LoadGameScene();
        }
        catch (Exception ex)
        {
            _errorLogger.text = ex.Message;
        }
    }

    public void Save()
    {
        try
        {
            _levelEditor.TrySaveMap();
        }
        catch (Exception ex)
        {
            _errorLogger.text = ex.Message;
        }
    }

    public void Load()
    {
        try
        {
            _levelEditor.TryLoadMap();
        }
        catch (Exception ex)
        {
            _errorLogger.text = ex.Message;
        }
    }

    public void ObstacleColor()
    {
        _levelEditor.ChangeBrushType(LevelEditorGrid.LevelEditorGridObject.TileType.Obstacle);
    }

    public void SpawnpointColor()
    {
        _levelEditor.ChangeBrushType(LevelEditorGrid.LevelEditorGridObject.TileType.Spawnpoint);
    }

    public void WalkableColor()
    {
        _levelEditor.ChangeBrushType(LevelEditorGrid.LevelEditorGridObject.TileType.Walkable);
    }

    public void Eraser()
    {
        _levelEditor.ChangeBrushType(LevelEditorGrid.LevelEditorGridObject.TileType.Eraser);
    }

    public void ExitLevelEditor()
    {
        _levelLoader.LoadMainMenu();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        
        OnPauseButtonPressed += LevelEditorUI_OnPauseButtonPressed;
    }

    private void LevelEditorUI_OnPauseButtonPressed(object sender, EventArgs e)
    {
        ExitLevelEditor();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        
        OnPauseButtonPressed -= LevelEditorUI_OnPauseButtonPressed;
    }
}