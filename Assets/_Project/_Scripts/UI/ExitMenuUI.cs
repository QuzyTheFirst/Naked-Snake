using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMenuUI : MonoBehaviour
{
    private SceneController _sceneController;

    public void Initialize(SceneController sceneController)
    {
        _sceneController = sceneController;
    }
    
    public void MainMenuBtn()
    {
        _sceneController.LoadMainMenu();
    }

    public void ExitGameBtn()
    {
        Application.Quit();
    }
}
