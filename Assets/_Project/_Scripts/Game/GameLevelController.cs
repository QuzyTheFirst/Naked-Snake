using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelController : MonoBehaviour
{
    public enum GameStateEnum
    {
        Beginning,
        IsGoing,
        Ended,
    }

    public GameStateEnum GameState;
    public bool IsPaused { get; private set; }
    
    private SnakeController _snakeController;
    private FruitsController _fruitsController;
    private LevelLoader _levelLoader;
    
    public LevelLoader LevelLoader {get => _levelLoader;}
    
    public void Initialize(SnakeController snakeController, FruitsController fruitsController, LevelLoader levelLoader)
    {
        _snakeController = snakeController;
        _fruitsController = fruitsController;
        _levelLoader = levelLoader;
    }

    public void InitializeGame()
    {
        GameState = GameStateEnum.Beginning;
        
        _snakeController.SpawnSnakeHead();
        _fruitsController.SpawnFruit();
    }
    
    public void StartGame()
    {
        if (GameState != GameStateEnum.Beginning)
            return;
        
        _snakeController.StartMoving();
        
        GameState = GameStateEnum.IsGoing;
        
        Debug.Log("Game Started!");
    }

    public void RestartGame()
    {
        _levelLoader.LoadGameScene();
    }

    public void PauseGame()
    {
        if (GameState == GameStateEnum.Ended)
            return;
        
        Time.timeScale = 0;
        IsPaused = true;
    }

    public void ContinueGame()
    {
        if (GameState == GameStateEnum.Ended)
            return;
        
        Time.timeScale = 1;
        IsPaused = false;
    }

    private void OnEnable()
    {
        SnakeController.OnSnakeDeath += SnakeControllerOnOnSnakeDeath;
    }

    private void SnakeControllerOnOnSnakeDeath(object sender, EventArgs e)
    {
        if(IsPaused)
            ContinueGame();
        
        GameState = GameStateEnum.Ended;
    }

    private void OnDisable()
    {
        SnakeController.OnSnakeDeath -= SnakeControllerOnOnSnakeDeath;
    }
}
