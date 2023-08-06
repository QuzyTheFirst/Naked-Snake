using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    
    private SceneController _sceneController;
    private GameStateController _gameStateController;

    public void Initialize(SceneController sceneController, GameStateController gameStateController)
    {
        _sceneController = sceneController;
        _gameStateController = gameStateController;
    }

    public void ContinueBtn()
    {
        _gameStateController.ContinueGame();
    }

    public void MainMenuBtn()
    {
        _sceneController.LoadMainMenu();
    }

    public void TogglePauseMenu(bool value)
    {
        _pauseMenu.SetActive(value);
    }
}
