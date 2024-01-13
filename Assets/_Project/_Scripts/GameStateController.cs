using System;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public enum GameState
    {
        Paused,
        Active,
        Lost
    }

    private GameState _currentGameState = GameState.Active;
    
    private InGameUI _inGameUI;
    private FruitsCollector _fruitCollector;
    
    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        set { _currentGameState = value; }
    }
    
    public void Initialize(InGameUI inGameUI, FruitsCollector fruitCollector)
    {
        _inGameUI = inGameUI;
        _fruitCollector = fruitCollector;
    }
    
    public void PauseGame()
    {
        _inGameUI.OpenPauseMenu();
        
        Time.timeScale = 0;
        
        _currentGameState = GameState.Paused;
    }

    public void ContinueGame()
    {
        _inGameUI.ClosePauseMenu();
        
        Time.timeScale = 1;
        
        _currentGameState = GameState.Active;
    }

    private void OnEnable()
    {
        SnakeMovement.OnSnakeDeath += SnakeMovementOnOnSnakeDeath;
    }

    private  void SnakeMovementOnOnSnakeDeath(object sender, EventArgs e)
    {
        _inGameUI.ActivateDeathMenu();
        _inGameUI.ToggleShiftText(false);
        _inGameUI.SetDeathMenuText($"Game Over!\nIs your snake on a diet?\nYou have eaten just {_fruitCollector.CollectedFruits} fruit(s)!\nTry to feed it more!");
        _currentGameState = GameState.Lost;
        SoundManager.Instance.Play("LostSound");
    }

    private void OnDisable()
    {
        SnakeMovement.OnSnakeDeath -= SnakeMovementOnOnSnakeDeath;
    }
}
