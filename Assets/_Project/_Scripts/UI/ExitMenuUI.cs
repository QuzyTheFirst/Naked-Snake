using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMenuUI : MonoBehaviour
{
    private LevelLoader _levelLoader;

    public void Initialize(LevelLoader levelLoader)
    {
        _levelLoader = levelLoader;
    }
    
    public void MainMenuBtn()
    {
        _levelLoader.LoadMainMenu();
    }

    public void ExitGameBtn()
    {
        Application.Quit();
    }
}
