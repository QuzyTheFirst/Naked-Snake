using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    protected event EventHandler OnUpPressed;
    protected event EventHandler OnDownPressed;
    protected event EventHandler OnRightPressed;
    protected event EventHandler OnLeftPressed;
    
    private PlayerInputs _playerInputs;
    private void Awake()
    {
        _playerInputs = new PlayerInputs();
        
        _playerInputs.Map.UP.performed+= OnUpPerformed;
        _playerInputs.Map.DOWN.performed += OnDownPerformed;
        _playerInputs.Map.RIGHT.performed += OnRightPerformed;
        _playerInputs.Map.LEFT.performed += OnLeftPerformed;
    }

    private void OnLeftPerformed(InputAction.CallbackContext obj)
    {
        OnLeftPressed?.Invoke(this, EventArgs.Empty);
    }

    private void OnRightPerformed(InputAction.CallbackContext obj)
    {
        OnRightPressed?.Invoke(this, EventArgs.Empty);
    }

    private void OnDownPerformed(InputAction.CallbackContext obj)
    {
        OnDownPressed?.Invoke(this, EventArgs.Empty);
    }

    private void OnUpPerformed(InputAction.CallbackContext obj)
    {
        OnUpPressed?.Invoke(this, EventArgs.Empty);
    }

    private void OnEnable()
    {
        _playerInputs.Enable();
    }

    private void OnDisable()
    {
        _playerInputs.Disable();
    }
}
