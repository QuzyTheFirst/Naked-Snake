using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEditorUI : MonoBehaviour
{
    [SerializeField] private LevelEditor _levelEditor;
    [SerializeField] private TextMeshProUGUI _errorLogger;
    [SerializeField] private GameObject[] _saveLoadButtons;

#if UNITY_WEBGL && !UNITY_EDITOR

    private void Start()
    {
        foreach (GameObject button in _saveLoadButtons)
        {
            button.SetActive(false);
        }
    }

#endif

    public void Play()
    {
        try
        {
            LevelToLoadInfo levelToLoadInfo = FindObjectOfType<LevelToLoadInfo>();
            _levelEditor.TrySetupLevelToLoadInfo(levelToLoadInfo);
            SceneManager.LoadScene(2);
        }
        catch (Exception ex)
        {
            _errorLogger.text = ex.Message;
        }
    }

    public void Save()
    {
        _levelEditor.SaveMap();
    }

    public void Load()
    {
        _levelEditor.LoadMap();
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
        _levelEditor.ChangeBrushType(LevelEditorGrid.LevelEditorGridObject.TileType.Empty);
    }
}