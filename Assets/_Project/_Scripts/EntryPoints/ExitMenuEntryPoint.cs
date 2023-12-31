using UnityEngine;

public class ExitMenuEntryPoint : MonoBehaviour
{
    [SerializeField] private SceneController _sceneController;
    [SerializeField] private ExitMenuUI _exitMenuUI;
    private void Start()
    {
        _exitMenuUI.Initialize(_sceneController);
    }
}
