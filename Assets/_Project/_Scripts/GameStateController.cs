using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public enum GameState
    {
        Paused,
        Active,
        Win,
        Lost
    }

    private GameState _currentGameState = GameState.Active;
    
    private InGameUI _inGameUI;
    private SnakeMovement _snakeMovement;
    private FruitSpawner _fruitSpawner;

    [SerializeField] private ParticleSystem _winParticles;
    
    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        set { _currentGameState = value; }
    }
    
    public void Initialize(InGameUI inGameUI, FruitSpawner fruitSpawner)
    {
        _inGameUI = inGameUI;
        _fruitSpawner = fruitSpawner;
    }

    public void SetSnakeMovement(SnakeMovement snakeMovement)
    {
        _snakeMovement = snakeMovement;
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

    private void OnEnable()
    {
        FruitsCollector.AllFruitsCollected += FruitsCollector_OnAllFruitsCollected;
        SnakeMovement.OnSnakeDeath += SnakeMovementOnOnSnakeDeath;
    }
    
    private async void FruitsCollector_OnAllFruitsCollected(object sender, EventArgs e)
    {
        _snakeMovement.DeactivateSnake();
        Vector3 center = new Vector3(LevelGenerator.GeneratedGridSize.x * .5f, 0, LevelGenerator.GeneratedGridSize.y * .5f) - (Vector3.one * .5f);
        _winParticles.transform.position = center * LevelGenerator.DistanceBetweenTiles;
        _winParticles.Play();

        await Task.Delay(500);
        SoundManager.Instance.Play("WinSound");
        _fruitSpawner.DeleteAllFruits();
        _inGameUI.ActivateVictoryMenu();
        _currentGameState = GameState.Win;
    }
    
    private  void SnakeMovementOnOnSnakeDeath(object sender, EventArgs e)
    {
        _inGameUI.ActivateDeathMenu();
        _currentGameState = GameState.Lost;
        SoundManager.Instance.Play("LostSound");
    }

    private void OnDisable()
    {
        FruitsCollector.AllFruitsCollected -= FruitsCollector_OnAllFruitsCollected;
        SnakeMovement.OnSnakeDeath -= SnakeMovementOnOnSnakeDeath;
    }
}
