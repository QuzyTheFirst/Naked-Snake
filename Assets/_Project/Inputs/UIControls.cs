//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/_Project/Inputs/UIControls.inputactions
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

public partial class @UIControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @UIControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""UIControls"",
    ""maps"": [
        {
            ""name"": ""Map"",
            ""id"": ""ee48244c-7fc3-4fd3-bdda-ce6d2a9ec6b7"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""d770566a-a3c4-4d25-9ff4-ee50fb7f5cf6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""StartGame"",
                    ""type"": ""Button"",
                    ""id"": ""d944e5a4-f8ae-4566-9e75-715230636af3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ba9fdc84-1704-46e4-94b0-e63df46e09f0"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67268021-6b6c-4d54-b81b-3bd1e8033f29"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec8ec51c-6315-44ea-99be-014d40585906"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0627e163-e67c-4ab8-a51c-2104fabbf105"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""StartGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4abe4bb5-f94d-402c-89cd-596ceed75ba3"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""StartGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Map
        m_Map = asset.FindActionMap("Map", throwIfNotFound: true);
        m_Map_Pause = m_Map.FindAction("Pause", throwIfNotFound: true);
        m_Map_StartGame = m_Map.FindAction("StartGame", throwIfNotFound: true);
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
    private readonly InputAction m_Map_Pause;
    private readonly InputAction m_Map_StartGame;
    public struct MapActions
    {
        private @UIControls m_Wrapper;
        public MapActions(@UIControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_Map_Pause;
        public InputAction @StartGame => m_Wrapper.m_Map_StartGame;
        public InputActionMap Get() { return m_Wrapper.m_Map; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MapActions set) { return set.Get(); }
        public void AddCallbacks(IMapActions instance)
        {
            if (instance == null || m_Wrapper.m_MapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MapActionsCallbackInterfaces.Add(instance);
            @Pause.started += instance.OnPause;
            @Pause.performed += instance.OnPause;
            @Pause.canceled += instance.OnPause;
            @StartGame.started += instance.OnStartGame;
            @StartGame.performed += instance.OnStartGame;
            @StartGame.canceled += instance.OnStartGame;
        }

        private void UnregisterCallbacks(IMapActions instance)
        {
            @Pause.started -= instance.OnPause;
            @Pause.performed -= instance.OnPause;
            @Pause.canceled -= instance.OnPause;
            @StartGame.started -= instance.OnStartGame;
            @StartGame.performed -= instance.OnStartGame;
            @StartGame.canceled -= instance.OnStartGame;
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
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IMapActions
    {
        void OnPause(InputAction.CallbackContext context);
        void OnStartGame(InputAction.CallbackContext context);
    }
}
