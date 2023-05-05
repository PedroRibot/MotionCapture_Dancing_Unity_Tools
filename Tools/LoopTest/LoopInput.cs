//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Tools/LoopTest/LoopInput.inputactions
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

public partial class @LoopInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @LoopInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""LoopInput"",
    ""maps"": [
        {
            ""name"": ""JoyCon Input"",
            ""id"": ""a6c76e53-c096-42bf-9b4b-68c1e15a651a"",
            ""actions"": [
                {
                    ""name"": ""Start Recording"",
                    ""type"": ""Button"",
                    ""id"": ""bbe9fd81-0a23-4ffe-bf0d-6236820bd84b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Stop Recording"",
                    ""type"": ""Button"",
                    ""id"": ""4eb8d652-b3cd-4289-a7f9-81803cbaedde"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reset"",
                    ""type"": ""Button"",
                    ""id"": ""4af84b14-0a71-4316-be4e-558bfa89e7ae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ControlReproductionSpeed"",
                    ""type"": ""Button"",
                    ""id"": ""c22b0f48-a311-4e1f-8285-70a2b257ab24"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""451ceb27-e4f3-45bb-b061-7045c2dfec78"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button16"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start Recording"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""744c0d1e-3e7b-49db-884d-64ac1c013dd2"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button15"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start Recording"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c4c9aee6-cc9b-4094-a31a-ba3b4f63321b"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button16"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start Recording"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""66acbee6-d8a6-45f0-a259-3b838b774414"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button15"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start Recording"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d957060f-0a13-4a49-a315-bef8267c9bb4"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start Recording"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac62b1a1-71c8-4b58-897e-243d40a6f0a5"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stop Recording"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8f73457-25e7-4e64-8b14-d3d50b8a8e5f"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stop Recording"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""df76ae03-1abf-4c1d-aa54-b88713f221bf"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stop Recording"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3c4d5bf-a3cb-49aa-a0ae-3945fca53602"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stop Recording"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b52661c-20a7-4cf9-bd95-bda3a96893fa"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stop Recording"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""033c842a-e3ca-4beb-a121-5d47d9e0f4bb"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stop Recording"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8cf26dd9-d54a-4c9a-9a7e-526ae3f9e97e"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stop Recording"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6524ece2-fc64-4089-b1c5-cd62dc909316"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stop Recording"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""892d0028-e02d-41f8-819c-e5c73edb7bb0"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reset"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7c44805c-f3a0-4d05-8077-6c0867b1ae6f"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button10"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reset"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe893a98-a7c8-4bb6-993e-9640a58f8b46"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reset"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2ae93834-7675-41f1-aab6-1ef00c504d34"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button13"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ControlReproductionSpeed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cc6283ad-2eca-4a09-aaf8-7fbabf4f4954"",
                    ""path"": ""<HID::Nintendo Wireless Gamepad>/button14"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ControlReproductionSpeed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d60aa4c2-f67f-42be-8320-2806e9432b7b"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ControlReproductionSpeed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // JoyCon Input
        m_JoyConInput = asset.FindActionMap("JoyCon Input", throwIfNotFound: true);
        m_JoyConInput_StartRecording = m_JoyConInput.FindAction("Start Recording", throwIfNotFound: true);
        m_JoyConInput_StopRecording = m_JoyConInput.FindAction("Stop Recording", throwIfNotFound: true);
        m_JoyConInput_Reset = m_JoyConInput.FindAction("Reset", throwIfNotFound: true);
        m_JoyConInput_ControlReproductionSpeed = m_JoyConInput.FindAction("ControlReproductionSpeed", throwIfNotFound: true);
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

    // JoyCon Input
    private readonly InputActionMap m_JoyConInput;
    private IJoyConInputActions m_JoyConInputActionsCallbackInterface;
    private readonly InputAction m_JoyConInput_StartRecording;
    private readonly InputAction m_JoyConInput_StopRecording;
    private readonly InputAction m_JoyConInput_Reset;
    private readonly InputAction m_JoyConInput_ControlReproductionSpeed;
    public struct JoyConInputActions
    {
        private @LoopInput m_Wrapper;
        public JoyConInputActions(@LoopInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @StartRecording => m_Wrapper.m_JoyConInput_StartRecording;
        public InputAction @StopRecording => m_Wrapper.m_JoyConInput_StopRecording;
        public InputAction @Reset => m_Wrapper.m_JoyConInput_Reset;
        public InputAction @ControlReproductionSpeed => m_Wrapper.m_JoyConInput_ControlReproductionSpeed;
        public InputActionMap Get() { return m_Wrapper.m_JoyConInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(JoyConInputActions set) { return set.Get(); }
        public void SetCallbacks(IJoyConInputActions instance)
        {
            if (m_Wrapper.m_JoyConInputActionsCallbackInterface != null)
            {
                @StartRecording.started -= m_Wrapper.m_JoyConInputActionsCallbackInterface.OnStartRecording;
                @StartRecording.performed -= m_Wrapper.m_JoyConInputActionsCallbackInterface.OnStartRecording;
                @StartRecording.canceled -= m_Wrapper.m_JoyConInputActionsCallbackInterface.OnStartRecording;
                @StopRecording.started -= m_Wrapper.m_JoyConInputActionsCallbackInterface.OnStopRecording;
                @StopRecording.performed -= m_Wrapper.m_JoyConInputActionsCallbackInterface.OnStopRecording;
                @StopRecording.canceled -= m_Wrapper.m_JoyConInputActionsCallbackInterface.OnStopRecording;
                @Reset.started -= m_Wrapper.m_JoyConInputActionsCallbackInterface.OnReset;
                @Reset.performed -= m_Wrapper.m_JoyConInputActionsCallbackInterface.OnReset;
                @Reset.canceled -= m_Wrapper.m_JoyConInputActionsCallbackInterface.OnReset;
                @ControlReproductionSpeed.started -= m_Wrapper.m_JoyConInputActionsCallbackInterface.OnControlReproductionSpeed;
                @ControlReproductionSpeed.performed -= m_Wrapper.m_JoyConInputActionsCallbackInterface.OnControlReproductionSpeed;
                @ControlReproductionSpeed.canceled -= m_Wrapper.m_JoyConInputActionsCallbackInterface.OnControlReproductionSpeed;
            }
            m_Wrapper.m_JoyConInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @StartRecording.started += instance.OnStartRecording;
                @StartRecording.performed += instance.OnStartRecording;
                @StartRecording.canceled += instance.OnStartRecording;
                @StopRecording.started += instance.OnStopRecording;
                @StopRecording.performed += instance.OnStopRecording;
                @StopRecording.canceled += instance.OnStopRecording;
                @Reset.started += instance.OnReset;
                @Reset.performed += instance.OnReset;
                @Reset.canceled += instance.OnReset;
                @ControlReproductionSpeed.started += instance.OnControlReproductionSpeed;
                @ControlReproductionSpeed.performed += instance.OnControlReproductionSpeed;
                @ControlReproductionSpeed.canceled += instance.OnControlReproductionSpeed;
            }
        }
    }
    public JoyConInputActions @JoyConInput => new JoyConInputActions(this);
    public interface IJoyConInputActions
    {
        void OnStartRecording(InputAction.CallbackContext context);
        void OnStopRecording(InputAction.CallbackContext context);
        void OnReset(InputAction.CallbackContext context);
        void OnControlReproductionSpeed(InputAction.CallbackContext context);
    }
}
