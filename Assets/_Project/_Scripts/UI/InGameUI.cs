using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : UIInputHandler
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _deathMenu;
    [SerializeField] private GameObject _victoryMenu;
    [SerializeField] private TextMeshProUGUI _applesTextField;
    [SerializeField] private GameObject _shiftText;
    [SerializeField] private Slider _stepProgressBar;
    
    private SceneController _sceneController;
    private GameStateController _gameStateController;
    private LevelChanger _levelChanger;

    private Coroutine _stepProgressBarCoroutine;

    private bool _isDeathMenuActivated = false;
    private bool _isVictoryMenuActivated = false;

    public void Initialize(SceneController sceneController, GameStateController gameStateController, LevelChanger levelChanger)
    {
        _sceneController = sceneController;
        _gameStateController = gameStateController;
        _levelChanger = levelChanger;
    }

    public void ContinueBtn()
    {
        _gameStateController.ContinueGame();
        SoundManager.Instance.Play("ButtonClick");
    }

    public void MainMenuBtn()
    {
        _sceneController.LoadMainMenu();
        SoundManager.Instance.Play("ButtonClick");
    }

    public void RestartBtn()
    {
        _levelChanger.RestartLevel();
        DeactivateDeathMenu();
        _gameStateController.ContinueGame();
        SoundManager.Instance.Play("ButtonClick");
    }

    public void NextLevelBtn()
    {
        _levelChanger.LoadNextLevel();
        DeactivateVictoryMenu();
        SoundManager.Instance.Play("ButtonClick");
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

    public void ActivateDeathMenu()
    {
        _deathMenu.SetActive(true);
        _gameStateController.ContinueGame();
        _isDeathMenuActivated = true;
    }

    public void DeactivateDeathMenu()
    {
        _deathMenu.SetActive(false);
        _isDeathMenuActivated = false;
    }
    
    public void ActivateVictoryMenu()
    {
        _victoryMenu.SetActive(true);
        _gameStateController.ContinueGame();
        _isVictoryMenuActivated = true;
    }

    public void DeactivateVictoryMenu()
    {
        _victoryMenu.SetActive(false);
        _isVictoryMenuActivated = false;
    }

    public void StartProgressBarAnimation(float time)
    {
        if (_stepProgressBarCoroutine != null)
        {
            StopCoroutine(_stepProgressBarCoroutine);
        }
        
        _stepProgressBarCoroutine = StartCoroutine(ProgressBarAnimation(time));
    }

    
    public IEnumerator ProgressBarAnimation(float time)
    {
        float timer = 0;
        while (timer < time)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;

            float progress = timer / time;
            _stepProgressBar.value = progress;;
        }

        _stepProgressBar.value = 1;
    }
    
    protected  override void OnEnable()
    {
        base.OnEnable();
        
        FruitsCollector.FruitSuccessfullyEaten += FruitsCollectorOnFruitSuccessfullyEaten;
        OnPauseButtonPressed += InGameUI_OnPauseButtonPressed;
    }
    
    private void FruitsCollectorOnFruitSuccessfullyEaten(object sender, Vector2Int e)
    {
        UpdateApplesTextField(FruitsCollector.CollectedFruits, FruitsCollector.AmountOfFruitsToCollect);
    }
    
    private void InGameUI_OnPauseButtonPressed(object sender, EventArgs e)
    {
        Debug.Log("Trying to open pause menu");
        if (_isDeathMenuActivated || _isVictoryMenuActivated)
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
        
        FruitsCollector.FruitSuccessfullyEaten -= FruitsCollectorOnFruitSuccessfullyEaten;
        OnPauseButtonPressed -= InGameUI_OnPauseButtonPressed;
    }
}
