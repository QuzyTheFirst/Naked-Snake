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

    private LevelLoader _levelLoader;
    
    public void Initialize(LevelLoader levelLoader, Sprite[] levelsSprites)
    {
        _levelLoader = levelLoader;

        GenerateLevelButtons(levelsSprites);

        EventSystem.current.SetSelectedGameObject(_mainMenuFirst);
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

    public void OpenLevelEditor()
    {
        _levelLoader.LoadLevelEditor();
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
                LevelToLoadInfo levelInfo = FindObjectOfType<LevelToLoadInfo>();
                levelInfo.SetLevelSprite(levelsSprites[temp]);

                SoundManager.Instance.Play("ButtonClick");

                _levelLoader.LoadGameScene();
            });
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        OnPauseButtonPressed += MainMenuUI_OnPauseButtonPressed;
    }

    private void MainMenuUI_OnPauseButtonPressed(object sender, System.EventArgs e)
    {
        if(_levelsMenu.activeSelf)
        {
            CloseAllLevelsMenu();
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        OnPauseButtonPressed -= MainMenuUI_OnPauseButtonPressed;
    }
}
