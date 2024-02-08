using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;

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

    [Header("Beginning Menu")] 
    [SerializeField] private GameObject _beginningMenu;

    [Header("First Selected Options")]
    [SerializeField] private GameObject _pauseMenuFirst;
    [SerializeField] private GameObject _deathMenuFirst;
    
    private GameLevelController _gameLevelController;

    private Coroutine _stepProgressBarCoroutine;

    private LensDistortion m_LensDistortion;
    
    public void Initialize(GameLevelController gameLevelController, PostProcessVolume postProcessVolume)
    {
        _gameLevelController = gameLevelController;
        postProcessVolume.profile.TryGetSettings(out m_LensDistortion);
    }

    public void ContinueBtn()
    {
        _gameLevelController.ContinueGame();
        ClosePauseMenu();
        SoundManager.Instance.Play("ButtonClick");
    }

    public void MainMenuBtn()
    {
        _gameLevelController.LevelLoader.LoadMainMenu();
        SoundManager.Instance.Play("ButtonClick");
    }

    public void RestartBtn()
    {
        _gameLevelController.RestartGame();
        SoundManager.Instance.Play("ButtonClick");
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
    
    public void ActivateDeathMenu()
    {
        _deathMenu.SetActive(true);
        _deathMenuTextField.text = $"Game Over!\nIs your snake on a diet?\nYou have eaten just {_applesTextField.text} fruit(s)!\nTry to feed it more!";
        SoundManager.Instance.Play("LostSound");
        
        EventSystem.current.SetSelectedGameObject(_deathMenuFirst);
    }
    
    protected  override void OnEnable()
    {
        base.OnEnable();
        
        OnPauseButtonPressed += InGameUI_OnPauseButtonPressed;
        OnStartGameButtonPerformed += InGameUI_OnStartGameButtonPerformed;
        SnakeController.OnSnakeDeath += SnakeControllerOnOnSnakeDeath;
        SnakeController.BoostStarted += SnakeControllerOnBoostStarted;
        SnakeController.BoostEnded += SnakeControllerOnBoostEnded;
        FruitsController.OnCollectedFruitAmountChanged += FruitsController_OnCollectedFruitAmountChanged;
    }

    private void SnakeControllerOnBoostEnded(object sender, EventArgs e)
    {
        LeanTween.value(gameObject, m_LensDistortion.intensity.value, -5, 0.5f);
    }

    private void SnakeControllerOnBoostStarted(object sender, EventArgs e)
    {
        LeanTween.value(gameObject, m_LensDistortion.intensity.value, -20, 0.5f);
    }

    private void InGameUI_OnStartGameButtonPerformed(object sender, EventArgs e)
    {
        if (_gameLevelController.GameState != GameLevelController.GameStateEnum.Beginning)
            return;

        _beginningMenu.gameObject.SetActive(false);
        _gameLevelController.StartGame();
    }

    private void SnakeControllerOnOnSnakeDeath(object sender, EventArgs e)
    {
        StartCoroutine(OpenDeathMenuIn(3));
        
        IEnumerator OpenDeathMenuIn(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            ActivateDeathMenu();
        }
    }

    private void FruitsController_OnCollectedFruitAmountChanged(object sender, int fruitsCollectedAmount)
    {
        _applesTextField.SetText($"{fruitsCollectedAmount}");
    }

    private void InGameUI_OnPauseButtonPressed(object sender, EventArgs e)
    {
        if (_gameLevelController.GameState == GameLevelController.GameStateEnum.Ended)
            return;
        
        switch (_gameLevelController.IsPaused)
        {
            case true:
                _gameLevelController.ContinueGame();
                ClosePauseMenu();
                break;
            case false:
                _gameLevelController.PauseGame();
                OpenPauseMenu();
                break;
        }
        SoundManager.Instance.Play("ButtonClick");
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        OnPauseButtonPressed -= InGameUI_OnPauseButtonPressed;
        OnStartGameButtonPerformed -= InGameUI_OnStartGameButtonPerformed;
        SnakeController.OnSnakeDeath -= SnakeControllerOnOnSnakeDeath;
        SnakeController.BoostStarted -= SnakeControllerOnBoostStarted;
        SnakeController.BoostEnded -= SnakeControllerOnBoostEnded;
        FruitsController.OnCollectedFruitAmountChanged -= FruitsController_OnCollectedFruitAmountChanged;
    }
}
