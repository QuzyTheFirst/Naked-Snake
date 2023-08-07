using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Serialization;

public class SnakeHeadMovement : PlayerInputHandler
{
    enum GoingDirection { WS, AD }

    private GoingDirection _goingDirection = GoingDirection.AD;
    public enum SnakeState { Alive, Dead}

    private SnakeState _snakeState = SnakeState.Alive;

    [SerializeField] private float moveEach = .5f;

    private Vector2 _currentGridPosition;

    public void Initialize(Vector2 gridPosition)
    {
        _currentGridPosition = gridPosition;
    }

}
