// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerActionControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActionControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""OnVirus"",
            ""id"": ""48ff668a-5c2a-46c0-87bf-28b4d3e6f8ed"",
            ""actions"": [
                {
                    ""name"": ""MoveSideways"",
                    ""type"": ""Value"",
                    ""id"": ""93794a87-6fae-481d-abcc-ef9ac8720c7c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CursorHorizontalControl"",
                    ""type"": ""Value"",
                    ""id"": ""d66eb146-35b4-49eb-aff9-feafc93f1189"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveForward"",
                    ""type"": ""Value"",
                    ""id"": ""1e7570fd-1ad1-44ee-b596-0254d6282af9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CursorVerticalControl"",
                    ""type"": ""Value"",
                    ""id"": ""3cb09cd7-aff4-4926-87c7-edeb5aa6c611"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TriggerScan"",
                    ""type"": ""Button"",
                    ""id"": ""8564e153-ab6c-4bf2-a284-8db99b9a2a4e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TriggerBomb"",
                    ""type"": ""Button"",
                    ""id"": ""dd5111d3-f9d1-4b41-8e7b-388b95d0183b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TriggerDash"",
                    ""type"": ""Button"",
                    ""id"": ""a93bf445-74b9-4617-85b1-13527fdaec86"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TriggerCombatMode"",
                    ""type"": ""Value"",
                    ""id"": ""c30b5caa-4c3d-4d8f-a2a7-977170c55bae"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Accelerate"",
                    ""type"": ""Value"",
                    ""id"": ""294bef35-bca9-4b13-afc5-2b328a319fc7"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TriggerStatue"",
                    ""type"": ""Button"",
                    ""id"": ""c6f4dd0e-670a-412a-85d2-5d5197a8ca9c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TriggerMenu"",
                    ""type"": ""Button"",
                    ""id"": ""ba7d9702-6220-4f1f-ab58-f0703826ba53"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""SidewaysGamepad"",
                    ""id"": ""e93004b7-bbec-4e97-966b-f145111e10e2"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": """",
                    ""action"": ""MoveSideways"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""83954099-edec-42e8-aadd-d7d78e4bb280"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveSideways"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""999a2b9b-c71c-456e-863c-a4d70ed5b79c"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveSideways"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""SidewaysKeyboard"",
                    ""id"": ""cc8420a9-2db4-404f-9f00-956d383c8768"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveSideways"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""41cce8e4-1bb9-441e-b209-0f2b66c4af89"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveSideways"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""3848b546-c1bf-4fc2-8c76-ea90e6cd63c7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveSideways"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""HorizontalGamepadCursor"",
                    ""id"": ""856c0c31-1404-4326-a041-d3091bbbc76b"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorHorizontalControl"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""53350e5a-b1b7-4747-ae13-ff6242e7593c"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""CursorHorizontalControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""62914745-e097-4e6b-a44e-c0b5f53e404c"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""CursorHorizontalControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""06e6b823-7658-4f16-ac53-1b6893422464"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone(min=0.325),Normalize(min=-0.8,max=0.8)"",
                    ""groups"": """",
                    ""action"": ""CursorHorizontalControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""ForwardGamepad"",
                    ""id"": ""8fbe2881-059f-46b6-9b95-a8d170fee99c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": """",
                    ""action"": ""MoveForward"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""28d07657-7c30-4ca1-b2ba-1b4f14c8138e"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""2fc422ef-42e4-4371-9077-31841d60f63c"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ForwardKeyboard"",
                    ""id"": ""d2b040d9-0a74-40c2-b20f-538ca83b6da9"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveForward"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""6b2ba381-23ca-43c8-84d0-8e71a8511e4a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""5146f406-126f-4a52-9b1b-f2941eea2904"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""VerticalGamepadCursor"",
                    ""id"": ""8663ea5a-ecc4-42e1-9b96-c1276e00a651"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CursorVerticalControl"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0a669ca5-3bc2-4460-8703-966f126cbdbb"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""CursorVerticalControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""0cbf290a-c580-4647-8216-d6ff4a16f573"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""CursorVerticalControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""78501236-6bc9-45ec-98f1-08da253940f5"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TriggerScan"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d5272d4f-32ad-4ff0-aa85-12304ef3a71e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TriggerScan"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9719654a-0a05-4285-a927-a600855382e1"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TriggerBomb"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2aee9f4-5c48-436c-b693-4113acdc3121"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TriggerDash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eadcb1ee-cec7-4dba-a1f1-c9681af9ace8"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TriggerDash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f7058be5-db71-48a5-a9df-400ede3a04e4"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TriggerCombatMode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eb192afc-c010-46ce-a984-0e80662024f8"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5b0e1f42-b588-4fb6-b701-34d9ae009148"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TriggerStatue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08a1621c-8be3-447e-bc5d-0a7ece9ed5b5"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TriggerStatue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""defb0d48-9d83-4f5a-b91c-46962d6047e2"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TriggerMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // OnVirus
        m_OnVirus = asset.FindActionMap("OnVirus", throwIfNotFound: true);
        m_OnVirus_MoveSideways = m_OnVirus.FindAction("MoveSideways", throwIfNotFound: true);
        m_OnVirus_CursorHorizontalControl = m_OnVirus.FindAction("CursorHorizontalControl", throwIfNotFound: true);
        m_OnVirus_MoveForward = m_OnVirus.FindAction("MoveForward", throwIfNotFound: true);
        m_OnVirus_CursorVerticalControl = m_OnVirus.FindAction("CursorVerticalControl", throwIfNotFound: true);
        m_OnVirus_TriggerScan = m_OnVirus.FindAction("TriggerScan", throwIfNotFound: true);
        m_OnVirus_TriggerBomb = m_OnVirus.FindAction("TriggerBomb", throwIfNotFound: true);
        m_OnVirus_TriggerDash = m_OnVirus.FindAction("TriggerDash", throwIfNotFound: true);
        m_OnVirus_TriggerCombatMode = m_OnVirus.FindAction("TriggerCombatMode", throwIfNotFound: true);
        m_OnVirus_Accelerate = m_OnVirus.FindAction("Accelerate", throwIfNotFound: true);
        m_OnVirus_TriggerStatue = m_OnVirus.FindAction("TriggerStatue", throwIfNotFound: true);
        m_OnVirus_TriggerMenu = m_OnVirus.FindAction("TriggerMenu", throwIfNotFound: true);
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

    // OnVirus
    private readonly InputActionMap m_OnVirus;
    private IOnVirusActions m_OnVirusActionsCallbackInterface;
    private readonly InputAction m_OnVirus_MoveSideways;
    private readonly InputAction m_OnVirus_CursorHorizontalControl;
    private readonly InputAction m_OnVirus_MoveForward;
    private readonly InputAction m_OnVirus_CursorVerticalControl;
    private readonly InputAction m_OnVirus_TriggerScan;
    private readonly InputAction m_OnVirus_TriggerBomb;
    private readonly InputAction m_OnVirus_TriggerDash;
    private readonly InputAction m_OnVirus_TriggerCombatMode;
    private readonly InputAction m_OnVirus_Accelerate;
    private readonly InputAction m_OnVirus_TriggerStatue;
    private readonly InputAction m_OnVirus_TriggerMenu;
    public struct OnVirusActions
    {
        private @PlayerActionControls m_Wrapper;
        public OnVirusActions(@PlayerActionControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveSideways => m_Wrapper.m_OnVirus_MoveSideways;
        public InputAction @CursorHorizontalControl => m_Wrapper.m_OnVirus_CursorHorizontalControl;
        public InputAction @MoveForward => m_Wrapper.m_OnVirus_MoveForward;
        public InputAction @CursorVerticalControl => m_Wrapper.m_OnVirus_CursorVerticalControl;
        public InputAction @TriggerScan => m_Wrapper.m_OnVirus_TriggerScan;
        public InputAction @TriggerBomb => m_Wrapper.m_OnVirus_TriggerBomb;
        public InputAction @TriggerDash => m_Wrapper.m_OnVirus_TriggerDash;
        public InputAction @TriggerCombatMode => m_Wrapper.m_OnVirus_TriggerCombatMode;
        public InputAction @Accelerate => m_Wrapper.m_OnVirus_Accelerate;
        public InputAction @TriggerStatue => m_Wrapper.m_OnVirus_TriggerStatue;
        public InputAction @TriggerMenu => m_Wrapper.m_OnVirus_TriggerMenu;
        public InputActionMap Get() { return m_Wrapper.m_OnVirus; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OnVirusActions set) { return set.Get(); }
        public void SetCallbacks(IOnVirusActions instance)
        {
            if (m_Wrapper.m_OnVirusActionsCallbackInterface != null)
            {
                @MoveSideways.started -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnMoveSideways;
                @MoveSideways.performed -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnMoveSideways;
                @MoveSideways.canceled -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnMoveSideways;
                @CursorHorizontalControl.started -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnCursorHorizontalControl;
                @CursorHorizontalControl.performed -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnCursorHorizontalControl;
                @CursorHorizontalControl.canceled -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnCursorHorizontalControl;
                @MoveForward.started -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnMoveForward;
                @MoveForward.performed -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnMoveForward;
                @MoveForward.canceled -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnMoveForward;
                @CursorVerticalControl.started -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnCursorVerticalControl;
                @CursorVerticalControl.performed -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnCursorVerticalControl;
                @CursorVerticalControl.canceled -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnCursorVerticalControl;
                @TriggerScan.started -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnTriggerScan;
                @TriggerScan.performed -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnTriggerScan;
                @TriggerScan.canceled -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnTriggerScan;
                @TriggerBomb.started -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnTriggerBomb;
                @TriggerBomb.performed -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnTriggerBomb;
                @TriggerBomb.canceled -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnTriggerBomb;
                @TriggerDash.started -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnTriggerDash;
                @TriggerDash.performed -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnTriggerDash;
                @TriggerDash.canceled -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnTriggerDash;
                @TriggerCombatMode.started -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnTriggerCombatMode;
                @TriggerCombatMode.performed -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnTriggerCombatMode;
                @TriggerCombatMode.canceled -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnTriggerCombatMode;
                @Accelerate.started -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnAccelerate;
                @Accelerate.performed -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnAccelerate;
                @Accelerate.canceled -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnAccelerate;
                @TriggerStatue.started -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnTriggerStatue;
                @TriggerStatue.performed -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnTriggerStatue;
                @TriggerStatue.canceled -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnTriggerStatue;
                @TriggerMenu.started -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnTriggerMenu;
                @TriggerMenu.performed -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnTriggerMenu;
                @TriggerMenu.canceled -= m_Wrapper.m_OnVirusActionsCallbackInterface.OnTriggerMenu;
            }
            m_Wrapper.m_OnVirusActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveSideways.started += instance.OnMoveSideways;
                @MoveSideways.performed += instance.OnMoveSideways;
                @MoveSideways.canceled += instance.OnMoveSideways;
                @CursorHorizontalControl.started += instance.OnCursorHorizontalControl;
                @CursorHorizontalControl.performed += instance.OnCursorHorizontalControl;
                @CursorHorizontalControl.canceled += instance.OnCursorHorizontalControl;
                @MoveForward.started += instance.OnMoveForward;
                @MoveForward.performed += instance.OnMoveForward;
                @MoveForward.canceled += instance.OnMoveForward;
                @CursorVerticalControl.started += instance.OnCursorVerticalControl;
                @CursorVerticalControl.performed += instance.OnCursorVerticalControl;
                @CursorVerticalControl.canceled += instance.OnCursorVerticalControl;
                @TriggerScan.started += instance.OnTriggerScan;
                @TriggerScan.performed += instance.OnTriggerScan;
                @TriggerScan.canceled += instance.OnTriggerScan;
                @TriggerBomb.started += instance.OnTriggerBomb;
                @TriggerBomb.performed += instance.OnTriggerBomb;
                @TriggerBomb.canceled += instance.OnTriggerBomb;
                @TriggerDash.started += instance.OnTriggerDash;
                @TriggerDash.performed += instance.OnTriggerDash;
                @TriggerDash.canceled += instance.OnTriggerDash;
                @TriggerCombatMode.started += instance.OnTriggerCombatMode;
                @TriggerCombatMode.performed += instance.OnTriggerCombatMode;
                @TriggerCombatMode.canceled += instance.OnTriggerCombatMode;
                @Accelerate.started += instance.OnAccelerate;
                @Accelerate.performed += instance.OnAccelerate;
                @Accelerate.canceled += instance.OnAccelerate;
                @TriggerStatue.started += instance.OnTriggerStatue;
                @TriggerStatue.performed += instance.OnTriggerStatue;
                @TriggerStatue.canceled += instance.OnTriggerStatue;
                @TriggerMenu.started += instance.OnTriggerMenu;
                @TriggerMenu.performed += instance.OnTriggerMenu;
                @TriggerMenu.canceled += instance.OnTriggerMenu;
            }
        }
    }
    public OnVirusActions @OnVirus => new OnVirusActions(this);
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IOnVirusActions
    {
        void OnMoveSideways(InputAction.CallbackContext context);
        void OnCursorHorizontalControl(InputAction.CallbackContext context);
        void OnMoveForward(InputAction.CallbackContext context);
        void OnCursorVerticalControl(InputAction.CallbackContext context);
        void OnTriggerScan(InputAction.CallbackContext context);
        void OnTriggerBomb(InputAction.CallbackContext context);
        void OnTriggerDash(InputAction.CallbackContext context);
        void OnTriggerCombatMode(InputAction.CallbackContext context);
        void OnAccelerate(InputAction.CallbackContext context);
        void OnTriggerStatue(InputAction.CallbackContext context);
        void OnTriggerMenu(InputAction.CallbackContext context);
    }
}
