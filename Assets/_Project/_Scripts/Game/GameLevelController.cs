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

    public GameStateEnum GameState { get; private set; }
    public bool IsPaused { get; private set; }
    
    private SnakeController _snakeController;
    private FruitsController _fruitsController;
    private LevelLoader _levelLoader;
    private CameraController _cameraController;

    [SerializeField] private ParticleSystem _winParticles;

    public LevelLoader LevelLoader {get => _levelLoader;}
    
    public void Initialize(SnakeController snakeController, FruitsController fruitsController, CameraController cameraController, LevelLoader levelLoader)
    {
        _snakeController = snakeController;
        _fruitsController = fruitsController;
        _cameraController = cameraController;
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
        FruitsController.SnakeHaveEatenAllPossibleFruits += SnakeController_OnSnakeHaveEatenAllPossibleFruits;
    }

    private void SnakeControllerOnOnSnakeDeath(object sender, EventArgs e)
    {
        if(IsPaused)
            ContinueGame();
        
        GameState = GameStateEnum.Ended;
    }

    private void SnakeController_OnSnakeHaveEatenAllPossibleFruits(object sender, EventArgs e)
    {
        if (IsPaused)
            ContinueGame();

        GameState = GameStateEnum.Ended;

        // Stop Snake
        _snakeController.StopMoving();
        // Play Win Sound
        SoundManager.Instance.Play("WinSound");
        // Place win particles at the center of the map
        _winParticles.transform.position = Vector3.Scale(_cameraController.transform.position, new Vector3(1, 0, 1));
        // Play Win Particles
        _winParticles.Play();
    }

    private void OnDisable()
    {
        SnakeController.OnSnakeDeath -= SnakeControllerOnOnSnakeDeath;
        FruitsController.SnakeHaveEatenAllPossibleFruits -= SnakeController_OnSnakeHaveEatenAllPossibleFruits;
    }
}
