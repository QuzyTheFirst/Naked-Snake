using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{ 
    public void LoadNextScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            Debug.Log(operation.progress);
            yield return null;
        }
    }

    public void LoadMainMenu()
    {
        //SceneManager.LoadScene(0);
        StartCoroutine(LoadAsynchronously(0));
    }

    public void LoadScene(int sceneID)
    {
        //SceneManager.LoadScene(sceneID);
        StartCoroutine(LoadAsynchronously(sceneID));
    }
}
