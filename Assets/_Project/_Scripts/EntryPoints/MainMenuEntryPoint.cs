using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private MainMenuUI _mainMenuUI;
    [SerializeField] private SceneController _sceneController;
    private void Start()
    {
        Application.targetFrameRate = 60;
        
        _mainMenuUI.Initialize(_sceneController);
    }
}
