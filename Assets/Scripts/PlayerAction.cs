// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/PlayerAction.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerAction : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerAction()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerAction"",
    ""maps"": [
        {
            ""name"": ""Player1"",
            ""id"": ""9ece1011-acd5-4752-a073-1b784d508174"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""46b420b8-e7e6-42f2-ad47-0efe448c10f0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""RotateR"",
                    ""type"": ""Button"",
                    ""id"": ""df79cc6e-9d6e-420f-9819-2a79830187f6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""RotateL"",
                    ""type"": ""Button"",
                    ""id"": ""ad2b4f21-601c-4268-8332-9606bcd0c565"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""bf55016d-b2bc-426b-a055-6bcfc6770d19"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5b8c9b6b-2bbc-4bac-89d1-2e46b0049daf"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""488152d5-9e45-4fb2-948e-c3e644968a52"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""588bccb4-55b1-4311-91f8-4e73c33a73d4"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""89139d71-1fde-4fa3-a146-8d7892628800"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9056a9af-49ea-4b93-be5e-43d8dc57c2b4"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e188750-0e6e-43a0-9d47-aba16aafeaab"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateR"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""322842d6-771b-48d9-8d52-394f103979cc"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateR"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d53be43-dfc4-40a8-8ce1-6bc21609a200"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateL"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba0ace75-18df-4386-b8bf-aa12ab03caa2"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateL"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player2"",
            ""id"": ""491aafd4-e39c-46c3-868f-972e18dd2562"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""b9124e6d-1922-46af-80e1-8e04eee305ff"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""RotateR"",
                    ""type"": ""Button"",
                    ""id"": ""0ccb3847-8091-4a14-b657-bf61490b3daa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""RotateL"",
                    ""type"": ""Button"",
                    ""id"": ""982b6364-15e9-4aa4-bc85-2a06b599dc3e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""29ced7a5-4b16-4e30-aed8-55a27e5d0ec9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c8a8bf52-27ca-40dd-a2a6-f53e070f4a86"",
                    ""path"": ""<Keyboard>/numpad0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0637bf20-1f9a-4b7b-8ac7-19c5279ca990"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2eb4a4b-5ff9-4365-b333-2025908f30ce"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""58cb1d8d-9364-4c02-9095-a83fae62f152"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateR"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a60ad14c-2eea-4f4e-be8c-a44b7a9544d5"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateL"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player1
        m_Player1 = asset.FindActionMap("Player1", throwIfNotFound: true);
        m_Player1_Move = m_Player1.FindAction("Move", throwIfNotFound: true);
        m_Player1_RotateR = m_Player1.FindAction("RotateR", throwIfNotFound: true);
        m_Player1_RotateL = m_Player1.FindAction("RotateL", throwIfNotFound: true);
        m_Player1_Shoot = m_Player1.FindAction("Shoot", throwIfNotFound: true);
        // Player2
        m_Player2 = asset.FindActionMap("Player2", throwIfNotFound: true);
        m_Player2_Move = m_Player2.FindAction("Move", throwIfNotFound: true);
        m_Player2_RotateR = m_Player2.FindAction("RotateR", throwIfNotFound: true);
        m_Player2_RotateL = m_Player2.FindAction("RotateL", throwIfNotFound: true);
        m_Player2_Shoot = m_Player2.FindAction("Shoot", throwIfNotFound: true);
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

    // Player1
    private readonly InputActionMap m_Player1;
    private IPlayer1Actions m_Player1ActionsCallbackInterface;
    private readonly InputAction m_Player1_Move;
    private readonly InputAction m_Player1_RotateR;
    private readonly InputAction m_Player1_RotateL;
    private readonly InputAction m_Player1_Shoot;
    public struct Player1Actions
    {
        private @PlayerAction m_Wrapper;
        public Player1Actions(@PlayerAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player1_Move;
        public InputAction @RotateR => m_Wrapper.m_Player1_RotateR;
        public InputAction @RotateL => m_Wrapper.m_Player1_RotateL;
        public InputAction @Shoot => m_Wrapper.m_Player1_Shoot;
        public InputActionMap Get() { return m_Wrapper.m_Player1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player1Actions set) { return set.Get(); }
        public void SetCallbacks(IPlayer1Actions instance)
        {
            if (m_Wrapper.m_Player1ActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnMove;
                @RotateR.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnRotateR;
                @RotateR.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnRotateR;
                @RotateR.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnRotateR;
                @RotateL.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnRotateL;
                @RotateL.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnRotateL;
                @RotateL.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnRotateL;
                @Shoot.started -= m_Wrapper.m_Player1ActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_Player1ActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_Player1ActionsCallbackInterface.OnShoot;
            }
            m_Wrapper.m_Player1ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @RotateR.started += instance.OnRotateR;
                @RotateR.performed += instance.OnRotateR;
                @RotateR.canceled += instance.OnRotateR;
                @RotateL.started += instance.OnRotateL;
                @RotateL.performed += instance.OnRotateL;
                @RotateL.canceled += instance.OnRotateL;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
            }
        }
    }
    public Player1Actions @Player1 => new Player1Actions(this);

    // Player2
    private readonly InputActionMap m_Player2;
    private IPlayer2Actions m_Player2ActionsCallbackInterface;
    private readonly InputAction m_Player2_Move;
    private readonly InputAction m_Player2_RotateR;
    private readonly InputAction m_Player2_RotateL;
    private readonly InputAction m_Player2_Shoot;
    public struct Player2Actions
    {
        private @PlayerAction m_Wrapper;
        public Player2Actions(@PlayerAction wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player2_Move;
        public InputAction @RotateR => m_Wrapper.m_Player2_RotateR;
        public InputAction @RotateL => m_Wrapper.m_Player2_RotateL;
        public InputAction @Shoot => m_Wrapper.m_Player2_Shoot;
        public InputActionMap Get() { return m_Wrapper.m_Player2; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player2Actions set) { return set.Get(); }
        public void SetCallbacks(IPlayer2Actions instance)
        {
            if (m_Wrapper.m_Player2ActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnMove;
                @RotateR.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnRotateR;
                @RotateR.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnRotateR;
                @RotateR.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnRotateR;
                @RotateL.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnRotateL;
                @RotateL.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnRotateL;
                @RotateL.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnRotateL;
                @Shoot.started -= m_Wrapper.m_Player2ActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_Player2ActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_Player2ActionsCallbackInterface.OnShoot;
            }
            m_Wrapper.m_Player2ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @RotateR.started += instance.OnRotateR;
                @RotateR.performed += instance.OnRotateR;
                @RotateR.canceled += instance.OnRotateR;
                @RotateL.started += instance.OnRotateL;
                @RotateL.performed += instance.OnRotateL;
                @RotateL.canceled += instance.OnRotateL;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
            }
        }
    }
    public Player2Actions @Player2 => new Player2Actions(this);
    public interface IPlayer1Actions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRotateR(InputAction.CallbackContext context);
        void OnRotateL(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
    }
    public interface IPlayer2Actions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRotateR(InputAction.CallbackContext context);
        void OnRotateL(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
    }
}
