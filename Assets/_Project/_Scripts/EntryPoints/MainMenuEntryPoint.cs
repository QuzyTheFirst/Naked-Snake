using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private MainMenuUI _mainMenuUI;
    [SerializeField] private Sprite[] _levelsSprites;

    private LevelLoader _levelLoader;
    
    private void Start()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1;
        
        _levelLoader = FindObjectOfType<LevelLoader>();
        
        _mainMenuUI.Initialize(_levelLoader, _levelsSprites);
        
        SoundManager.Instance.Play("Music");
    }
}
