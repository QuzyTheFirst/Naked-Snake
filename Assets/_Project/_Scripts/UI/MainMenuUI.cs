using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuUI : UIInputHandler
{
    [Header("Properties")]
    [SerializeField] private GameObject _levelsMenu;
    [SerializeField] private GameObject _levelsButtonsPanel;

    [SerializeField] private Transform _levelButtonPf;

    [Header("First Selected Options")]
    [SerializeField] private GameObject _mainMenuFirst;
    private GameObject _levelChooseMenuFirst;

    private SceneController _sceneController;
    
    public void Initialize(SceneController sceneController, Sprite[] levelsSprites)
    {
        _sceneController = sceneController;

        GenerateLevelButtons(levelsSprites);

        EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
    }
    
    public void ExitGameBtn()
    {
        Application.Quit();
        SoundManager.Instance.Play("ButtonClick");
    }

    public void OpenAllLevelsMenu()
    {
        EventSystem.current.SetSelectedGameObject(_levelChooseMenuFirst);
        _levelsMenu.SetActive(true);
    }

    public void CloseAllLevelsMenu()
    {
        EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
        _levelsMenu.SetActive(false);
    }

    private void GenerateLevelButtons(Sprite[] levelsSprites)
    {
        for(int i = 0; i < levelsSprites.Length; i++)
        {
            Button button = Instantiate(_levelButtonPf, _levelsButtonsPanel.transform).GetComponent<Button>();

            if(i == 0)
            {
                _levelChooseMenuFirst = button.gameObject;
            }

            button.transform.Find("Image").GetComponent<Image>().sprite = levelsSprites[i];

            int temp = i;

            button.onClick.AddListener(() =>
            {
                Debug.Log(temp);

                LevelToLoadInfo levelInfo = FindObjectOfType<LevelToLoadInfo>();
                levelInfo.SetLevelSprite(levelsSprites[temp]);

                SoundManager.Instance.Play("ButtonClick");

                _sceneController.LoadGameScene();
            });
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        OnReturnButtonPressed += MainMenuUI_OnReturnButtonPressed;
    }

    private void MainMenuUI_OnReturnButtonPressed(object sender, System.EventArgs e)
    {
        if(_levelsMenu.activeSelf)
        {
            CloseAllLevelsMenu();
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        OnReturnButtonPressed -= MainMenuUI_OnReturnButtonPressed;
    }
}
