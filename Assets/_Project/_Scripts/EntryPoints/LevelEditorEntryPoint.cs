using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorEntryPoint : MonoBehaviour
{
    [SerializeField] private LevelEditorUI _levelEditorUI;

    private LevelLoader _levelLoader;

    private void Start()
    {
        Time.timeScale = 1;
        
        _levelLoader = FindObjectOfType<LevelLoader>();
        
        _levelEditorUI.Initialize(_levelLoader);
    }
}
