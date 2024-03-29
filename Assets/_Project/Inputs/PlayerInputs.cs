//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/_Project/Inputs/PlayerInputs.inputactions
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

public partial class @PlayerInputs: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputs"",
    ""maps"": [
        {
            ""name"": ""Map"",
            ""id"": ""e483aaf3-12e5-4684-ab9b-84dedb9b78fa"",
            ""actions"": [
                {
                    ""name"": ""UP"",
                    ""type"": ""Button"",
                    ""id"": ""95463e89-5c65-4897-8212-6772ab70f705"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DOWN"",
                    ""type"": ""Button"",
                    ""id"": ""ce81c1cd-f51b-403a-8c7f-1575f58db0c5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RIGHT"",
                    ""type"": ""Button"",
                    ""id"": ""dcde5f3b-616a-4971-a7b1-719c69f5d504"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LEFT"",
                    ""type"": ""Button"",
                    ""id"": ""87dbc1a6-5d78-4172-b70e-eab0805d958b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SPEEDBOOST"",
                    ""type"": ""Button"",
                    ""id"": ""ab88e4b3-c4b2-43a4-8382-ac476fe9e153"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d165f112-343d-463a-a7fb-b8bab2e9b057"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""UP"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fd1ff74a-5e62-4b60-ab5b-e7666e120a8c"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""UP"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e4ea085-e293-463e-9991-ea1950608f43"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""UP"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8aff29ca-59ec-4bd9-b37d-f224414da920"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""UP"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b350245a-ee5c-460c-9b62-3d8276a504d3"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""DOWN"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c14ed9ae-a744-400d-8ac1-8c188f935e0d"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""DOWN"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""496e2d3c-1ce3-4509-bd81-52ccf0ec36d9"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""DOWN"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6683ad3b-2965-46a5-8aa0-b634fa68e9a0"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""DOWN"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c77e119-625e-4472-8b67-eb6184f8d1d3"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""RIGHT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd472059-94fe-4eb4-a1af-327c2f4d87db"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RIGHT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3d11b3bb-bc0c-47c7-ac66-2d1cd80b2a1d"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RIGHT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""314e29ed-993e-4647-8465-704a5519b8f4"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""RIGHT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""91eadf99-a809-4a9f-a8a7-f912878fc2d7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""LEFT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8f32b32-fea7-4e50-96a3-313843bad181"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""LEFT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4edf06c7-3060-4ee7-a804-f00085b0aee8"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""LEFT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""73934867-bad3-4550-8134-b4a8671785b5"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""LEFT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e45ee3b7-a028-4a26-839a-8d7019d210f7"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SPEEDBOOST"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b22d0fc4-2afb-4861-9160-0759427e3047"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SPEEDBOOST"",
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
        m_Map_UP = m_Map.FindAction("UP", throwIfNotFound: true);
        m_Map_DOWN = m_Map.FindAction("DOWN", throwIfNotFound: true);
        m_Map_RIGHT = m_Map.FindAction("RIGHT", throwIfNotFound: true);
        m_Map_LEFT = m_Map.FindAction("LEFT", throwIfNotFound: true);
        m_Map_SPEEDBOOST = m_Map.FindAction("SPEEDBOOST", throwIfNotFound: true);
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
    private readonly InputAction m_Map_UP;
    private readonly InputAction m_Map_DOWN;
    private readonly InputAction m_Map_RIGHT;
    private readonly InputAction m_Map_LEFT;
    private readonly InputAction m_Map_SPEEDBOOST;
    public struct MapActions
    {
        private @PlayerInputs m_Wrapper;
        public MapActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @UP => m_Wrapper.m_Map_UP;
        public InputAction @DOWN => m_Wrapper.m_Map_DOWN;
        public InputAction @RIGHT => m_Wrapper.m_Map_RIGHT;
        public InputAction @LEFT => m_Wrapper.m_Map_LEFT;
        public InputAction @SPEEDBOOST => m_Wrapper.m_Map_SPEEDBOOST;
        public InputActionMap Get() { return m_Wrapper.m_Map; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MapActions set) { return set.Get(); }
        public void AddCallbacks(IMapActions instance)
        {
            if (instance == null || m_Wrapper.m_MapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MapActionsCallbackInterfaces.Add(instance);
            @UP.started += instance.OnUP;
            @UP.performed += instance.OnUP;
            @UP.canceled += instance.OnUP;
            @DOWN.started += instance.OnDOWN;
            @DOWN.performed += instance.OnDOWN;
            @DOWN.canceled += instance.OnDOWN;
            @RIGHT.started += instance.OnRIGHT;
            @RIGHT.performed += instance.OnRIGHT;
            @RIGHT.canceled += instance.OnRIGHT;
            @LEFT.started += instance.OnLEFT;
            @LEFT.performed += instance.OnLEFT;
            @LEFT.canceled += instance.OnLEFT;
            @SPEEDBOOST.started += instance.OnSPEEDBOOST;
            @SPEEDBOOST.performed += instance.OnSPEEDBOOST;
            @SPEEDBOOST.canceled += instance.OnSPEEDBOOST;
        }

        private void UnregisterCallbacks(IMapActions instance)
        {
            @UP.started -= instance.OnUP;
            @UP.performed -= instance.OnUP;
            @UP.canceled -= instance.OnUP;
            @DOWN.started -= instance.OnDOWN;
            @DOWN.performed -= instance.OnDOWN;
            @DOWN.canceled -= instance.OnDOWN;
            @RIGHT.started -= instance.OnRIGHT;
            @RIGHT.performed -= instance.OnRIGHT;
            @RIGHT.canceled -= instance.OnRIGHT;
            @LEFT.started -= instance.OnLEFT;
            @LEFT.performed -= instance.OnLEFT;
            @LEFT.canceled -= instance.OnLEFT;
            @SPEEDBOOST.started -= instance.OnSPEEDBOOST;
            @SPEEDBOOST.performed -= instance.OnSPEEDBOOST;
            @SPEEDBOOST.canceled -= instance.OnSPEEDBOOST;
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
        void OnUP(InputAction.CallbackContext context);
        void OnDOWN(InputAction.CallbackContext context);
        void OnRIGHT(InputAction.CallbackContext context);
        void OnLEFT(InputAction.CallbackContext context);
        void OnSPEEDBOOST(InputAction.CallbackContext context);
    }
}
