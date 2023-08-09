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
    protected event EventHandler OnBoostButtonPressed;
    protected event EventHandler OnBoostButtonCanceled;
    
    private PlayerInputs _playerInputs;
    protected virtual void Awake()
    {
        _playerInputs = new PlayerInputs();
    }

    protected virtual void OnEnable()
    {
        _playerInputs.Enable();
        
        _playerInputs.Map.UP.performed+= OnUpPerformed;
        _playerInputs.Map.DOWN.performed += OnDownPerformed;
        _playerInputs.Map.RIGHT.performed += OnRightPerformed;
        _playerInputs.Map.LEFT.performed += OnLeftPerformed;
        
        _playerInputs.Map.SPEEDBOOST.performed += SPEEDBOOSTOnperformed;
        _playerInputs.Map.SPEEDBOOST.canceled += SPEEDBOOSTOncanceled;
    }
    
    private void SPEEDBOOSTOncanceled(InputAction.CallbackContext obj)
    {
        OnBoostButtonCanceled?.Invoke(this,EventArgs.Empty);
    }

    private void SPEEDBOOSTOnperformed(InputAction.CallbackContext obj)
    {
        OnBoostButtonPressed?.Invoke(this, EventArgs.Empty);
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
    

    protected virtual void OnDisable()
    {
        _playerInputs.Map.UP.performed -= OnUpPerformed;
        _playerInputs.Map.DOWN.performed -= OnDownPerformed;
        _playerInputs.Map.RIGHT.performed -= OnRightPerformed;
        _playerInputs.Map.LEFT.performed -= OnLeftPerformed;
        
        _playerInputs.Map.SPEEDBOOST.performed -= SPEEDBOOSTOnperformed;
        _playerInputs.Map.SPEEDBOOST.canceled -= SPEEDBOOSTOncanceled;
        
        _playerInputs.Disable();
    }
}
