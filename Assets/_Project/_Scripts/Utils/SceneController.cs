using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{ 
    public void LoadGameScene()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadLevelEditor()
    {
        SceneManager.LoadScene(1);
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
