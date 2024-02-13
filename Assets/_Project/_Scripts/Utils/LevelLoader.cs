using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator _transition;
    [SerializeField] private float _transitionTime = 1f;

    private static LevelLoader _instance;

    private bool _isInTransition = false;
    
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        
        DontDestroyOnLoad(gameObject);
    }

    public void LoadGameScene()
    {
        StartCoroutine(LoadLevel(2));
    }

    public void LoadLevelEditor()
    {
        StartCoroutine(LoadLevel(1));
    }
    
    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevel(0));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        if (_isInTransition)
            yield break;

        _isInTransition = true;
        
        // Start Transition animation
        _transition.SetTrigger("Start");
        // Start Loading New Level
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;
        // Wait while start transition ended
        yield return new WaitForSecondsRealtime(_transitionTime);
        // When new level is loaded start end transition animation
        asyncLoad.allowSceneActivation = true;
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        _transition.SetTrigger("End");
        
        _isInTransition = false;
    }

    private void OnCompleted(AsyncOperation obj)
    {
        throw new NotImplementedException();
    }
}
