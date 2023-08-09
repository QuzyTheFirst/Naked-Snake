using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _allLevelsMenu;

    private SceneController _sceneController;
    
    public void Initialize(SceneController sceneController)
    {
        _sceneController = sceneController;
    }
    
    public void StartGameBtn()
    {
        _sceneController.LoadNextScene();
    }

    public void LoadLevelBtn(int levelID)
    {
        _sceneController.LoadScene(levelID);
    }
    
    public void ExitGameBtn()
    {
        Application.Quit();
    }

    public void ToggleAllLevelsMenu(bool value)
    {
        _allLevelsMenu.SetActive(value);
    }
}
