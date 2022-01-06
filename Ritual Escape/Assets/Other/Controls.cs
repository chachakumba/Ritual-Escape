// GENERATED AUTOMATICALLY FROM 'Assets/Other/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Basic"",
            ""id"": ""9095a07a-7bb0-4b54-b4e0-fb81cb5a135a"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""0dabd3a4-18fb-4965-bb3f-9c8036861807"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""8beaff14-52aa-475e-b3f8-845ca3d8b10a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shout"",
                    ""type"": ""Button"",
                    ""id"": ""f4466194-8685-4387-8048-2693ed696a1b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Use"",
                    ""type"": ""Button"",
                    ""id"": ""fe7ddcb4-6d58-4498-8ea1-2ac8febd04e4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UseItem"",
                    ""type"": ""Button"",
                    ""id"": ""71900b6c-8254-4b5a-a1bb-66ec9c05d48b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""b24e7549-1bf1-497d-87e9-c11580f933e7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""cf80716c-613d-4034-a0bc-b591055ac68b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3166021d-ee5f-4c8a-b8f3-69a4e87d0dec"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d98a61ca-dc1d-443c-a225-a31726bb585d"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9b14dc5c-7944-4909-a5a6-692f8389ff0e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""83046260-9249-456d-bf51-0446e72a1b3d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6982d3d2-006a-4650-ba15-48a4e1cb05b7"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5bd29e79-d39c-49bb-a0cd-6f584cd5be9e"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shout"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bb93b6ef-0043-4577-94e5-b62348696544"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4596e4a4-a065-4cea-bedd-c9e0c38dbf8f"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""750983a6-046e-4683-8396-7a8f8195ef06"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Basic
        m_Basic = asset.FindActionMap("Basic", throwIfNotFound: true);
        m_Basic_Move = m_Basic.FindAction("Move", throwIfNotFound: true);
        m_Basic_Look = m_Basic.FindAction("Look", throwIfNotFound: true);
        m_Basic_Shout = m_Basic.FindAction("Shout", throwIfNotFound: true);
        m_Basic_Use = m_Basic.FindAction("Use", throwIfNotFound: true);
        m_Basic_UseItem = m_Basic.FindAction("UseItem", throwIfNotFound: true);
        m_Basic_Run = m_Basic.FindAction("Run", throwIfNotFound: true);
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

    // Basic
    private readonly InputActionMap m_Basic;
    private IBasicActions m_BasicActionsCallbackInterface;
    private readonly InputAction m_Basic_Move;
    private readonly InputAction m_Basic_Look;
    private readonly InputAction m_Basic_Shout;
    private readonly InputAction m_Basic_Use;
    private readonly InputAction m_Basic_UseItem;
    private readonly InputAction m_Basic_Run;
    public struct BasicActions
    {
        private @Controls m_Wrapper;
        public BasicActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Basic_Move;
        public InputAction @Look => m_Wrapper.m_Basic_Look;
        public InputAction @Shout => m_Wrapper.m_Basic_Shout;
        public InputAction @Use => m_Wrapper.m_Basic_Use;
        public InputAction @UseItem => m_Wrapper.m_Basic_UseItem;
        public InputAction @Run => m_Wrapper.m_Basic_Run;
        public InputActionMap Get() { return m_Wrapper.m_Basic; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BasicActions set) { return set.Get(); }
        public void SetCallbacks(IBasicActions instance)
        {
            if (m_Wrapper.m_BasicActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_BasicActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_BasicActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_BasicActionsCallbackInterface.OnMove;
                @Look.started -= m_Wrapper.m_BasicActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_BasicActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_BasicActionsCallbackInterface.OnLook;
                @Shout.started -= m_Wrapper.m_BasicActionsCallbackInterface.OnShout;
                @Shout.performed -= m_Wrapper.m_BasicActionsCallbackInterface.OnShout;
                @Shout.canceled -= m_Wrapper.m_BasicActionsCallbackInterface.OnShout;
                @Use.started -= m_Wrapper.m_BasicActionsCallbackInterface.OnUse;
                @Use.performed -= m_Wrapper.m_BasicActionsCallbackInterface.OnUse;
                @Use.canceled -= m_Wrapper.m_BasicActionsCallbackInterface.OnUse;
                @UseItem.started -= m_Wrapper.m_BasicActionsCallbackInterface.OnUseItem;
                @UseItem.performed -= m_Wrapper.m_BasicActionsCallbackInterface.OnUseItem;
                @UseItem.canceled -= m_Wrapper.m_BasicActionsCallbackInterface.OnUseItem;
                @Run.started -= m_Wrapper.m_BasicActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_BasicActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_BasicActionsCallbackInterface.OnRun;
            }
            m_Wrapper.m_BasicActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Shout.started += instance.OnShout;
                @Shout.performed += instance.OnShout;
                @Shout.canceled += instance.OnShout;
                @Use.started += instance.OnUse;
                @Use.performed += instance.OnUse;
                @Use.canceled += instance.OnUse;
                @UseItem.started += instance.OnUseItem;
                @UseItem.performed += instance.OnUseItem;
                @UseItem.canceled += instance.OnUseItem;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
            }
        }
    }
    public BasicActions @Basic => new BasicActions(this);
    public interface IBasicActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnShout(InputAction.CallbackContext context);
        void OnUse(InputAction.CallbackContext context);
        void OnUseItem(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
    }
}
