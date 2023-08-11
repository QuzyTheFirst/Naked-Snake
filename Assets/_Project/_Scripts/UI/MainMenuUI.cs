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
        LevelToLoadInfo lvl = FindObjectOfType<LevelToLoadInfo>();
        lvl.LevelID = 0;
        _sceneController.LoadNextScene();
        SoundManager.Instance.Play("ButtonClick");
    }

    public void LoadLevelBtn(int levelID)
    {
        LevelToLoadInfo lvl = FindObjectOfType<LevelToLoadInfo>();
        lvl.LevelID = levelID;
        _sceneController.LoadNextScene();
        SoundManager.Instance.Play("ButtonClick");
    }
    
    public void ExitGameBtn()
    {
        Application.Quit();
        SoundManager.Instance.Play("ButtonClick");
    }

    public void ToggleAllLevelsMenu(bool value)
    {
        _allLevelsMenu.SetActive(value);
    }
}
