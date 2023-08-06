using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameEntryPoint : MonoBehaviour
{
    [SerializeField] private SceneController _sceneController;
    [SerializeField] private InGameUI _inGameUI;

    [SerializeField] private GameStateController _gameStateController;

    private void Start()
    {
        _inGameUI.Initialize(_sceneController, _gameStateController);
        _gameStateController.Initialize(_inGameUI);
    }
}
