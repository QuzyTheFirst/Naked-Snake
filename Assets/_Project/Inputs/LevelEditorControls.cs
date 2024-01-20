//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/_Project/Inputs/LevelEditorControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @LevelEditorControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @LevelEditorControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""LevelEditorControls"",
    ""maps"": [
        {
            ""name"": ""Map"",
            ""id"": ""db91b76a-30ba-4a44-a4a4-3c34d51ddefe"",
            ""actions"": [
                {
                    ""name"": ""Obstacle"",
                    ""type"": ""Value"",
                    ""id"": ""2d01a845-9168-4217-b9b4-235abb22cf8e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Spawnpoint"",
                    ""type"": ""Button"",
                    ""id"": ""8ef153c2-5172-4b00-915d-be179f9a77e7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Walkable"",
                    ""type"": ""Button"",
                    ""id"": ""8a153d85-3af0-4322-a68a-42305d677b52"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Eraser"",
                    ""type"": ""Button"",
                    ""id"": ""986e0845-fe4a-40b0-bef0-f84c52833b9a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Mouse"",
                    ""type"": ""Button"",
                    ""id"": ""52487367-72e4-4826-9204-68e064d5233e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ec67e915-7211-4d6b-8595-e72b2738df1a"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Obstacle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""96f49a54-1b83-4113-a846-c6a1aba587b4"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Spawnpoint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""826fa135-182c-4954-bdd3-f1fe6c1956b3"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Walkable"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""524d65ef-3fdd-4161-aaf0-71a38eb05eb0"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Eraser"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5f4b02f6-646e-41ba-adcf-0f1803231b85"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyboardMouse"",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyboardMouse"",
            ""bindingGroup"": ""KeyboardMouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Map
        m_Map = asset.FindActionMap("Map", throwIfNotFound: true);
        m_Map_Obstacle = m_Map.FindAction("Obstacle", throwIfNotFound: true);
        m_Map_Spawnpoint = m_Map.FindAction("Spawnpoint", throwIfNotFound: true);
        m_Map_Walkable = m_Map.FindAction("Walkable", throwIfNotFound: true);
        m_Map_Eraser = m_Map.FindAction("Eraser", throwIfNotFound: true);
        m_Map_Mouse = m_Map.FindAction("Mouse", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Map
    private readonly InputActionMap m_Map;
    private List<IMapActions> m_MapActionsCallbackInterfaces = new List<IMapActions>();
    private readonly InputAction m_Map_Obstacle;
    private readonly InputAction m_Map_Spawnpoint;
    private readonly InputAction m_Map_Walkable;
    private readonly InputAction m_Map_Eraser;
    private readonly InputAction m_Map_Mouse;
    public struct MapActions
    {
        private @LevelEditorControls m_Wrapper;
        public MapActions(@LevelEditorControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Obstacle => m_Wrapper.m_Map_Obstacle;
        public InputAction @Spawnpoint => m_Wrapper.m_Map_Spawnpoint;
        public InputAction @Walkable => m_Wrapper.m_Map_Walkable;
        public InputAction @Eraser => m_Wrapper.m_Map_Eraser;
        public InputAction @Mouse => m_Wrapper.m_Map_Mouse;
        public InputActionMap Get() { return m_Wrapper.m_Map; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MapActions set) { return set.Get(); }
        public void AddCallbacks(IMapActions instance)
        {
            if (instance == null || m_Wrapper.m_MapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MapActionsCallbackInterfaces.Add(instance);
            @Obstacle.started += instance.OnObstacle;
            @Obstacle.performed += instance.OnObstacle;
            @Obstacle.canceled += instance.OnObstacle;
            @Spawnpoint.started += instance.OnSpawnpoint;
            @Spawnpoint.performed += instance.OnSpawnpoint;
            @Spawnpoint.canceled += instance.OnSpawnpoint;
            @Walkable.started += instance.OnWalkable;
            @Walkable.performed += instance.OnWalkable;
            @Walkable.canceled += instance.OnWalkable;
            @Eraser.started += instance.OnEraser;
            @Eraser.performed += instance.OnEraser;
            @Eraser.canceled += instance.OnEraser;
            @Mouse.started += instance.OnMouse;
            @Mouse.performed += instance.OnMouse;
            @Mouse.canceled += instance.OnMouse;
        }

        private void UnregisterCallbacks(IMapActions instance)
        {
            @Obstacle.started -= instance.OnObstacle;
            @Obstacle.performed -= instance.OnObstacle;
            @Obstacle.canceled -= instance.OnObstacle;
            @Spawnpoint.started -= instance.OnSpawnpoint;
            @Spawnpoint.performed -= instance.OnSpawnpoint;
            @Spawnpoint.canceled -= instance.OnSpawnpoint;
            @Walkable.started -= instance.OnWalkable;
            @Walkable.performed -= instance.OnWalkable;
            @Walkable.canceled -= instance.OnWalkable;
            @Eraser.started -= instance.OnEraser;
            @Eraser.performed -= instance.OnEraser;
            @Eraser.canceled -= instance.OnEraser;
            @Mouse.started -= instance.OnMouse;
            @Mouse.performed -= instance.OnMouse;
            @Mouse.canceled -= instance.OnMouse;
        }

        public void RemoveCallbacks(IMapActions instance)
        {
            if (m_Wrapper.m_MapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMapActions instance)
        {
            foreach (var item in m_Wrapper.m_MapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MapActions @Map => new MapActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("KeyboardMouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IMapActions
    {
        void OnObstacle(InputAction.CallbackContext context);
        void OnSpawnpoint(InputAction.CallbackContext context);
        void OnWalkable(InputAction.CallbackContext context);
        void OnEraser(InputAction.CallbackContext context);
        void OnMouse(InputAction.CallbackContext context);
    }
}
