using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private TextMeshProUGUI _applesTextField;
    [SerializeField] private GameObject _shiftText;
    
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

    public void UpdateApplesTextField(int apples, int applesToWin)
    {
        _applesTextField.SetText($"{apples} / {applesToWin}");
    }
    
    public void TogglePauseMenu(bool value)
    {
        _pauseMenu.SetActive(value);
    }

    public void ToggleShiftText(bool value)
    {
        _shiftText.SetActive(value);
    }
}
