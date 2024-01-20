using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelEditorInputHandler : MonoBehaviour
{
    protected event EventHandler OnObstaclePerformed;
    protected event EventHandler OnSpawnpointPerformed;
    protected event EventHandler OnWalkablePerformed;
    protected event EventHandler OnEraserPerformed;
    
    protected event EventHandler OnMousePerformed;
    protected event EventHandler OnMouseCanceled;
    
    private LevelEditorControls _levelEditorControls;

    protected virtual void Awake()
    {
        _levelEditorControls = new LevelEditorControls();
    }

    protected virtual void OnEnable()
    {
        _levelEditorControls.Enable();
        
        _levelEditorControls.Map.Obstacle.performed += ObstacleOnPerformed;
        _levelEditorControls.Map.Spawnpoint.performed += SpawnpointOnPerformed;
        _levelEditorControls.Map.Walkable.performed += WalkableOnPerformed;
        _levelEditorControls.Map.Eraser.performed += EraserOnPerformed;
        
        _levelEditorControls.Map.Mouse.performed += MouseOnPerformed;
        _levelEditorControls.Map.Mouse.canceled += MouseOnCanceled;
    }

    private void MouseOnCanceled(InputAction.CallbackContext obj)
    {
        OnMouseCanceled?.Invoke(this, EventArgs.Empty);
    }

    private void MouseOnPerformed(InputAction.CallbackContext obj)
    { 
        OnMousePerformed?.Invoke(this, EventArgs.Empty);
    }

    private void EraserOnPerformed(InputAction.CallbackContext obj)
    {
        OnEraserPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void WalkableOnPerformed(InputAction.CallbackContext obj)
    {
        OnWalkablePerformed?.Invoke(this, EventArgs.Empty);
    }

    private void SpawnpointOnPerformed(InputAction.CallbackContext obj)
    {
        OnSpawnpointPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void ObstacleOnPerformed(InputAction.CallbackContext obj)
    {
        OnObstaclePerformed?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnDisable()
    {
        _levelEditorControls.Map.Obstacle.performed -= ObstacleOnPerformed;
        _levelEditorControls.Map.Spawnpoint.performed -= SpawnpointOnPerformed;
        _levelEditorControls.Map.Walkable.performed -= WalkableOnPerformed;
        _levelEditorControls.Map.Eraser.performed -= EraserOnPerformed;
        
        _levelEditorControls.Map.Mouse.performed -= MouseOnPerformed;
        _levelEditorControls.Map.Mouse.canceled -= MouseOnCanceled;
        
        _levelEditorControls.Disable();
    }
}
