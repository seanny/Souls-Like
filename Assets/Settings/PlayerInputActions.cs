// GENERATED AUTOMATICALLY FROM 'Assets/Settings/PlayerInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace SoulsLike
{
    public class @PlayerInputActions : IInputActionCollection, IDisposable
    {
        private InputActionAsset asset;
        public @PlayerInputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""Player Controls"",
            ""id"": ""f1937f31-11c8-45a0-87ec-9584a40bdd7e"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""02cca3e0-189d-48f7-a888-71c32d893ade"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraMovement"",
                    ""type"": ""Value"",
                    ""id"": ""6b920591-bf66-4e9e-9d6e-84f5252587b9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GeneralAttack"",
                    ""type"": ""Button"",
                    ""id"": ""b55a6703-d3b6-42fd-a70d-d29d0cfc8182"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PowerAttack"",
                    ""type"": ""Button"",
                    ""id"": ""0707ed66-10f7-47e5-b226-26975d462056"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LockOn"",
                    ""type"": ""Button"",
                    ""id"": ""36b7aeca-7835-4c8c-ac48-a7ccac9c0f47"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""26d5a1d5-a5a9-4018-9ba1-bd565334f020"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""bb4c6130-4bfd-4420-b627-805094e837dd"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""e92066b8-74b9-4117-8e60-14fa3abb8dae"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c56b564b-0c58-411b-82ed-9a6298011b16"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9a565693-e379-4ed3-a08e-5d64c8237a13"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""289d56ad-b374-4b71-85ed-510f9c420cef"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2,ScaleVector2"",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9feec3f4-8b20-46b1-a2d8-3d027cab1e6a"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""14531a51-3ebb-4e1c-a912-81e1aa2fd95f"",
                    ""path"": ""<XInputController>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=50,y=50)"",
                    ""groups"": """",
                    ""action"": ""CameraMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0a299a45-e518-4dfc-8775-bf1d957853a6"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GeneralAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""76d1e7b6-22f7-44c6-81a3-ca2208917eb1"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": ""Press(behavior=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GeneralAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""747cff72-151a-4729-a5b8-7c5beb8d2aa8"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PowerAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c4934cc1-ca1d-46a0-8f4a-6282c5971c69"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PowerAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7ed8ab84-8432-46b9-93f4-43551bd2f5d1"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockOn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d9e89d84-0593-4f87-ab41-a958a343ffac"",
                    ""path"": ""<XInputController>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LockOn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Player Controls
            m_PlayerControls = asset.FindActionMap("Player Controls", throwIfNotFound: true);
            m_PlayerControls_Movement = m_PlayerControls.FindAction("Movement", throwIfNotFound: true);
            m_PlayerControls_CameraMovement = m_PlayerControls.FindAction("CameraMovement", throwIfNotFound: true);
            m_PlayerControls_GeneralAttack = m_PlayerControls.FindAction("GeneralAttack", throwIfNotFound: true);
            m_PlayerControls_PowerAttack = m_PlayerControls.FindAction("PowerAttack", throwIfNotFound: true);
            m_PlayerControls_LockOn = m_PlayerControls.FindAction("LockOn", throwIfNotFound: true);
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

        // Player Controls
        private readonly InputActionMap m_PlayerControls;
        private IPlayerControlsActions m_PlayerControlsActionsCallbackInterface;
        private readonly InputAction m_PlayerControls_Movement;
        private readonly InputAction m_PlayerControls_CameraMovement;
        private readonly InputAction m_PlayerControls_GeneralAttack;
        private readonly InputAction m_PlayerControls_PowerAttack;
        private readonly InputAction m_PlayerControls_LockOn;
        public struct PlayerControlsActions
        {
            private @PlayerInputActions m_Wrapper;
            public PlayerControlsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Movement => m_Wrapper.m_PlayerControls_Movement;
            public InputAction @CameraMovement => m_Wrapper.m_PlayerControls_CameraMovement;
            public InputAction @GeneralAttack => m_Wrapper.m_PlayerControls_GeneralAttack;
            public InputAction @PowerAttack => m_Wrapper.m_PlayerControls_PowerAttack;
            public InputAction @LockOn => m_Wrapper.m_PlayerControls_LockOn;
            public InputActionMap Get() { return m_Wrapper.m_PlayerControls; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(PlayerControlsActions set) { return set.Get(); }
            public void SetCallbacks(IPlayerControlsActions instance)
            {
                if (m_Wrapper.m_PlayerControlsActionsCallbackInterface != null)
                {
                    @Movement.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovement;
                    @Movement.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnMovement;
                    @CameraMovement.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnCameraMovement;
                    @CameraMovement.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnCameraMovement;
                    @CameraMovement.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnCameraMovement;
                    @GeneralAttack.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnGeneralAttack;
                    @GeneralAttack.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnGeneralAttack;
                    @GeneralAttack.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnGeneralAttack;
                    @PowerAttack.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPowerAttack;
                    @PowerAttack.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPowerAttack;
                    @PowerAttack.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnPowerAttack;
                    @LockOn.started -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLockOn;
                    @LockOn.performed -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLockOn;
                    @LockOn.canceled -= m_Wrapper.m_PlayerControlsActionsCallbackInterface.OnLockOn;
                }
                m_Wrapper.m_PlayerControlsActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Movement.started += instance.OnMovement;
                    @Movement.performed += instance.OnMovement;
                    @Movement.canceled += instance.OnMovement;
                    @CameraMovement.started += instance.OnCameraMovement;
                    @CameraMovement.performed += instance.OnCameraMovement;
                    @CameraMovement.canceled += instance.OnCameraMovement;
                    @GeneralAttack.started += instance.OnGeneralAttack;
                    @GeneralAttack.performed += instance.OnGeneralAttack;
                    @GeneralAttack.canceled += instance.OnGeneralAttack;
                    @PowerAttack.started += instance.OnPowerAttack;
                    @PowerAttack.performed += instance.OnPowerAttack;
                    @PowerAttack.canceled += instance.OnPowerAttack;
                    @LockOn.started += instance.OnLockOn;
                    @LockOn.performed += instance.OnLockOn;
                    @LockOn.canceled += instance.OnLockOn;
                }
            }
        }
        public PlayerControlsActions @PlayerControls => new PlayerControlsActions(this);
        public interface IPlayerControlsActions
        {
            void OnMovement(InputAction.CallbackContext context);
            void OnCameraMovement(InputAction.CallbackContext context);
            void OnGeneralAttack(InputAction.CallbackContext context);
            void OnPowerAttack(InputAction.CallbackContext context);
            void OnLockOn(InputAction.CallbackContext context);
        }
    }
}
