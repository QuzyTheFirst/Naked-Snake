using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public enum GameState
    {
        Paused,
        Active
    }

    private GameState _currentGameState = GameState.Active;
    
    private InGameUI _inGameUI;

    public GameState CurrentGameState
    {
        get { return _currentGameState; }
    }
    
    public void Initialize(InGameUI inGameUI)
    {
        _inGameUI = inGameUI;
    }
    
    public void PauseGame()
    {
        _inGameUI.TogglePauseMenu(true);
        
        Time.timeScale = 0;
        
        _currentGameState = GameState.Paused;
    }

    public void ContinueGame()
    {
        _inGameUI.TogglePauseMenu(false);
        
        Time.timeScale = 1;
        
        _currentGameState = GameState.Active;
    }
}
