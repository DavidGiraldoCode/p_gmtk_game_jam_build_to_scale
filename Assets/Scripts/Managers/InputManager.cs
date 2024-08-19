using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    [SerializeField] private PlayerState so_playerState;
    [SerializeField] private ScalingProvider m_scalingProvider;
    private float m_startEvent; // A bool triggered by the Space bar to test anything
    private Vector2 m_direction; // Unit 2D vector, default state is [0,0]
    private bool m_jump;
    private Vector2 m_pointer;
    private float m_castSpell;
    private float m_mousePressed;
    //private float m_nextSpell, m_previousSpell;
    private static bool m_giveControl = false;// TODO -> false;
    public bool GiveControl { get { return m_giveControl; } set { m_giveControl = value; } }
    private Vector2 m_look;
    public Vector2 Look { get => m_look; set => m_look = value; }
    private InputAction m_mouse;
    public Vector2 Direction
    {
        get => m_direction;
        private set => m_direction = value;
    }
    public bool Jump
    {
        get => m_jump;
        private set => m_jump = value;
    }
    public float StartEvent
    {
        get => m_startEvent;
        private set
        {
            m_startEvent = value;
            m_giveControl = true;
        }
    }
    public float CastSpell
    {
        get => m_castSpell;
        private set => m_castSpell = value;
    }
    //public float NextSpell { get => m_nextSpell; private set => m_nextSpell = value; }
    //public float PrevioudSpell { get => m_previousSpell; private set => m_previousSpell = value; }
    public Vector2 Pointer
    {
        get => m_pointer;
        private set => m_pointer = value;
    }

    public float MousePressed
    {
        get => m_mousePressed;
        private set => m_mousePressed = value;
    }
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(this); // This preserves the instance through the game between scenes.
    }

    public void EnableFPSInteraction()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_giveControl = true;
        so_playerState.Init();
    }
    public void DisableFPSInteraction()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        m_giveControl = false;
        m_direction = Vector2.zero;
        m_jump = false;
    }

    public static void OnMove(InputAction.CallbackContext context)
    {
        if (!m_giveControl)
            Instance.Direction = Vector2.zero;
        else
            Instance.Direction = context.ReadValue<Vector2>();
    }

    public static void OnJump(InputAction.CallbackContext context)
    {
        if (m_giveControl && context.started)
        {
            Instance.Jump = context.ReadValueAsButton();//ReadValue<float>();
            //Debug.Log(Instance.Jump);
        }
        else if (m_giveControl && context.canceled)
        {
            Instance.Jump = context.ReadValueAsButton();//ReadValue<float>();
            //Debug.Log(Instance.Jump);
        }
    }

    public void OnStartGame(InputAction.CallbackContext context)
    {
        Instance.StartEvent = context.ReadValue<float>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        // if (!so_spellCastState.CastMode)
        // {
        Instance.Look = context.ReadValue<Vector2>();
        // }

    }
    //TODO Left click to stretch and Right click to shrink.
    public void OnStretch(InputAction.CallbackContext context)
    {
        if (m_giveControl && context.performed && m_scalingProvider)
        {
            m_scalingProvider.ShootScalingRay(1.0f, Mouse.current.position.ReadValue());
        }
    }
    public void OnShrink(InputAction.CallbackContext context)
    {
        if (m_giveControl && context.performed && m_scalingProvider)
        {
            m_scalingProvider.ShootScalingRay(-1.0f, Mouse.current.position.ReadValue());
        }
    }
    public void OnScroll(InputAction.CallbackContext context)
    {
        if (m_giveControl && context.performed)
        {
            if (context.ReadValue<Vector2>().y > 0f)
                so_playerState.ChangeFactor(1);
            if (context.ReadValue<Vector2>().y < 0f)
                so_playerState.ChangeFactor(-1);
        }
    }

}
