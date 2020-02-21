// GENERATED AUTOMATICALLY FROM 'Assets/Code/Inputs/MenuInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MenuInput : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public @MenuInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MenuInput"",
    ""maps"": [
        {
            ""name"": ""Menu"",
            ""id"": ""0ec84224-54a5-4ff4-ac63-7c5082bdbc15"",
            ""actions"": [
                {
                    ""name"": ""MenuMovement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5f63826f-d0ce-4e17-b963-4567b48e2fbc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Joining"",
                    ""type"": ""Button"",
                    ""id"": ""77d10590-8300-46b8-8eb6-f3bc7248f920"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Leaving"",
                    ""type"": ""Button"",
                    ""id"": ""b865d8cb-87fb-4985-a7fe-ac2b74c6ac62"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ColorPicking"",
                    ""type"": ""Button"",
                    ""id"": ""861cc70c-4fea-440c-b305-8509a100074e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""666ea405-5f57-4da4-9793-c2c7e7aabaf7"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MenuMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d15c470e-b812-4b62-97a6-eff030cc42e7"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MenuMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1f34497e-3361-44ef-a53d-fb27b7c2e67f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MenuMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a6429a02-f502-413d-9f1a-76c6c4457dc6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MenuMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""55422a56-ad14-4c12-a49b-61e17974e31f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MenuMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e374f01f-8a7f-48d9-9cf4-56b584fb3816"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MenuMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""00808c14-dd44-4497-80a8-903dbc1ec792"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Joining"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46f23923-e550-4e05-8521-8df3589226a8"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Joining"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e3aa30e-824d-48f0-84a9-d3268ce443d7"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Leaving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ebff31b7-1090-4388-815a-90dd8cc6a2dd"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Leaving"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""217d3596-0e10-4f34-9b04-3a1501b08134"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""ColorPicking"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""93f0e5d1-bebe-4da6-aebb-13bb7b232b6b"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ColorPicking"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
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
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_MenuMovement = m_Menu.FindAction("MenuMovement", throwIfNotFound: true);
        m_Menu_Joining = m_Menu.FindAction("Joining", throwIfNotFound: true);
        m_Menu_Leaving = m_Menu.FindAction("Leaving", throwIfNotFound: true);
        m_Menu_ColorPicking = m_Menu.FindAction("ColorPicking", throwIfNotFound: true);
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

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_MenuMovement;
    private readonly InputAction m_Menu_Joining;
    private readonly InputAction m_Menu_Leaving;
    private readonly InputAction m_Menu_ColorPicking;
    public struct MenuActions
    {
        private @MenuInput m_Wrapper;
        public MenuActions(@MenuInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MenuMovement => m_Wrapper.m_Menu_MenuMovement;
        public InputAction @Joining => m_Wrapper.m_Menu_Joining;
        public InputAction @Leaving => m_Wrapper.m_Menu_Leaving;
        public InputAction @ColorPicking => m_Wrapper.m_Menu_ColorPicking;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @MenuMovement.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnMenuMovement;
                @MenuMovement.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnMenuMovement;
                @MenuMovement.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnMenuMovement;
                @Joining.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnJoining;
                @Joining.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnJoining;
                @Joining.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnJoining;
                @Leaving.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnLeaving;
                @Leaving.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnLeaving;
                @Leaving.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnLeaving;
                @ColorPicking.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnColorPicking;
                @ColorPicking.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnColorPicking;
                @ColorPicking.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnColorPicking;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MenuMovement.started += instance.OnMenuMovement;
                @MenuMovement.performed += instance.OnMenuMovement;
                @MenuMovement.canceled += instance.OnMenuMovement;
                @Joining.started += instance.OnJoining;
                @Joining.performed += instance.OnJoining;
                @Joining.canceled += instance.OnJoining;
                @Leaving.started += instance.OnLeaving;
                @Leaving.performed += instance.OnLeaving;
                @Leaving.canceled += instance.OnLeaving;
                @ColorPicking.started += instance.OnColorPicking;
                @ColorPicking.performed += instance.OnColorPicking;
                @ColorPicking.canceled += instance.OnColorPicking;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
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
    public interface IMenuActions
    {
        void OnMenuMovement(InputAction.CallbackContext context);
        void OnJoining(InputAction.CallbackContext context);
        void OnLeaving(InputAction.CallbackContext context);
        void OnColorPicking(InputAction.CallbackContext context);
    }
}
