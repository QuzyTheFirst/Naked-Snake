using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelToLoadInfo : MonoBehaviour
{
    public int LevelID = 0;
    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
