using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InGameUI : UIInputHandler
{
    [Header("Properties")]
    [SerializeField] private TextMeshProUGUI _applesTextField;
    [SerializeField] private GameObject _shiftText;

    [Header("Pause Menu")]
    [SerializeField] private GameObject _pauseMenu;

    [Header("Death Menu")]
    [SerializeField] private GameObject _deathMenu;
    [SerializeField] private Text _deathMenuTextField;

    [Header("First Selected Options")]
    [SerializeField] private GameObject _pauseMenuFirst;
    [SerializeField] private GameObject _deathMenuFirst;

    private LevelLoader _levelLoader;
    private GameStateController _gameStateController;
    private MapController _mapChanger;

    private Coroutine _stepProgressBarCoroutine;

    private bool _isDeathMenuActivated = false;

    public void Initialize(LevelLoader levelLoader, GameStateController gameStateController, MapController mapChanger)
    {
        _levelLoader = levelLoader;
        _gameStateController = gameStateController;
        _mapChanger = mapChanger;
    }

    public void ContinueBtn()
    {
        _gameStateController.ContinueGame();
        SoundManager.Instance.Play("ButtonClick");
    }

    public void MainMenuBtn()
    {
        _levelLoader.LoadMainMenu();
        SoundManager.Instance.Play("ButtonClick");
    }

    public void RestartBtn()
    {
        _mapChanger.RestartLevel();
        DeactivateDeathMenu();
        _gameStateController.ContinueGame();
        SoundManager.Instance.Play("ButtonClick");
    }

    public void UpdateApplesTextField(int apples)
    {
        _applesTextField.SetText($"{apples}");
    }

    public void OpenPauseMenu()
    {
        _pauseMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_pauseMenuFirst);
    }

    public void ClosePauseMenu()
    {
        _pauseMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ToggleShiftText(bool value)
    {
        _shiftText.SetActive(value);
    }

    public void ActivateDeathMenu()
    {
        _deathMenu.SetActive(true);
        _gameStateController.ContinueGame();
        EventSystem.current.SetSelectedGameObject(_deathMenuFirst);
        _isDeathMenuActivated = true;
    }

    public void SetDeathMenuText(string text)
    {
        _deathMenuTextField.text = text;
    }

    public void DeactivateDeathMenu()
    {
        _deathMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        _isDeathMenuActivated = false;
    }
    
    protected  override void OnEnable()
    {
        base.OnEnable();
        
        OnPauseButtonPressed += InGameUI_OnPauseButtonPressed;
        FruitsCollector.OnCollectedFruitAmountChanged += FruitsCollector_OnCollectedFruitAmountChanged;
    }


    private void FruitsCollector_OnCollectedFruitAmountChanged(object sender, int fruitsCollectedAmount)
    {
        UpdateApplesTextField(fruitsCollectedAmount);
    }

    private void InGameUI_OnPauseButtonPressed(object sender, EventArgs e)
    {
        if (_isDeathMenuActivated)
            return;
        
        switch (_gameStateController.CurrentGameState)
        {
            case GameStateController.GameState.Active:
                _gameStateController.PauseGame();
                break;
            case GameStateController.GameState.Paused:
                _gameStateController.ContinueGame();
                break;
        }
        SoundManager.Instance.Play("ButtonClick");
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        OnPauseButtonPressed -= InGameUI_OnPauseButtonPressed;
        FruitsCollector.OnCollectedFruitAmountChanged -= FruitsCollector_OnCollectedFruitAmountChanged;
    }
}
