using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInputHandler : MonoBehaviour
{
    protected event EventHandler OnPauseButtonPressed;
    protected event EventHandler OnStartGameButtonPerformed; 
    
    private UIControls _uiControls;
    private void Awake()
    {
        _uiControls = new UIControls();
    }

    protected virtual void OnEnable()
    {
        _uiControls.Enable();
        
        _uiControls.Map.Pause.performed += OnPausePerformed;
        _uiControls.Map.StartGame.performed += OnStartGamePerformed;
    }

    private void OnStartGamePerformed(InputAction.CallbackContext obj)
    {
        OnStartGameButtonPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void OnPausePerformed(InputAction.CallbackContext obj)
    {
        OnPauseButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnDisable()
    {
        _uiControls.Disable();
        
        _uiControls.Map.Pause.performed -= OnPausePerformed;
        _uiControls.Map.StartGame.performed -= OnStartGamePerformed;
    }
}
