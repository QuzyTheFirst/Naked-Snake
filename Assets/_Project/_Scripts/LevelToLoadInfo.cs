using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelToLoadInfo : MonoBehaviour
{
    private static LevelToLoadInfo _instance;

    private Sprite _levelSprite;

    public Sprite LevelSprite { get { return _levelSprite; } }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public void SetLevelSprite(Sprite levelSprite)
    {
        _levelSprite = levelSprite;
    }

}
