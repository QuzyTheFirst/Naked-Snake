using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private MainMenuUI _mainMenuUI;
    [SerializeField] private SceneController _sceneController;

    [SerializeField] private Sprite[] _levelsSprites;

    private void Start()
    {
        Application.targetFrameRate = 60;
        
        _mainMenuUI.Initialize(_sceneController, _levelsSprites);
        
        SoundManager.Instance.Play("Music");
    }
}
