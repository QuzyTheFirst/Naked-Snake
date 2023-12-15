using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInputHandler : MonoBehaviour
{
    protected event EventHandler OnPauseButtonPressed;
    protected event EventHandler OnReturnButtonPressed;
    
    private UIControls _uiControls;
    private void Awake()
    {
        _uiControls = new UIControls();
    }

    protected virtual void OnEnable()
    {
        _uiControls.Enable();
        
        _uiControls.Map.Pause.performed += OnPausePerformed;
        _uiControls.Map.Return.performed += OnReturnPerformed;
    }

    private void OnPausePerformed(InputAction.CallbackContext obj)
    {
        OnPauseButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    private void OnReturnPerformed(InputAction.CallbackContext obj)
    {
        OnReturnButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnDisable()
    {
        _uiControls.Disable();
        
        _uiControls.Map.Pause.performed -= OnPausePerformed;
        _uiControls.Map.Return.performed -= OnReturnPerformed;
    }
}
