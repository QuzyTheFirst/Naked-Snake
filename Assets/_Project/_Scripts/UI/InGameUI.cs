using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private TextMeshProUGUI _applesTextField;
    [SerializeField] private GameObject _shiftText;
    [SerializeField] private Slider _stepProgressBar;
    
    private SceneController _sceneController;
    private GameStateController _gameStateController;

    private Coroutine _stepProgressBarCoroutine;

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

    public void StartProgressBarAnimation(float time)
    {
        Debug.Log(time);
        
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
    
    private void OnEnable()
    {
        FruitsCollector.FruitSuccessfullyEaten += FruitsCollectorOnFruitSuccessfullyEaten;
    }

    private void FruitsCollectorOnFruitSuccessfullyEaten(object sender, Vector2Int e)
    {
        UpdateApplesTextField(FruitsCollector.CollectedFruits, FruitsCollector.AmountOfFruitsToCollect);
    }

    private void OnDisable()
    {
        FruitsCollector.FruitSuccessfullyEaten -= FruitsCollectorOnFruitSuccessfullyEaten;
    }
}
