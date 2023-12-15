using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelToLoadInfo : MonoBehaviour
{
    private Sprite _levelSprite;

    public Sprite LevelSprite { get { return _levelSprite; } }

    public void SetLevelSprite(Sprite levelSprite)
    {
        _levelSprite = levelSprite;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
