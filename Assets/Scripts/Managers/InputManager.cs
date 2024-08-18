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
    private float m_jump;
    private Vector2 m_pointer;
    private float m_castSpell;
    private float m_mousePressed;
    //private float m_nextSpell, m_previousSpell;
    private static bool m_giveControl = true;// TODO -> false;
    public bool GiveControl { get { return m_giveControl; } set { m_giveControl = value; } }
    private Vector2 m_look;
    public Vector2 Look { get => m_look; set => m_look = value; }
    private InputAction m_mouse;
    public Vector2 Direction
    {
        get => m_direction;
        private set => m_direction = value;
    }
    public float Jump
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
        so_playerState.Init();
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

    public static void OnMove(InputAction.CallbackContext context)
    {
        if (!m_giveControl)
            Instance.Direction = Vector2.zero;
        else
            Instance.Direction = context.ReadValue<Vector2>();
    }

    public static void OnJump(InputAction.CallbackContext context)
    {
        if (m_giveControl && context.performed)
        {
            Instance.Jump = context.ReadValue<float>();
        
        }
        else
            Instance.Jump = 0;
    }

    public void OnStartGame(InputAction.CallbackContext context)
    {
        Instance.StartEvent = context.ReadValue<float>();
    }

    // public void OnSpell(InputAction.CallbackContext context)
    // {
    //     Instance.CastSpell = context.ReadValue<float>();
    //     if (context.performed)
    //     {

    //         so_spellCastState.ToggleCastMode();
    //     }

    // }
    // public void TestPickUpBook()
    // {
    //     so_spellCastState.PickUpBook(); //TODO Simulation
    // }

    public void OnLook(InputAction.CallbackContext context)
    {
        // if (!so_spellCastState.CastMode)
        // {
        Instance.Look = context.ReadValue<Vector2>();
        // }

    }
    public void OnFire(InputAction.CallbackContext context)
    {
        // if (context.performed)
        // {
        //     //Debug.Log(context.ReadValue<float>());
        //     Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        //     RaycastHit hit;
        //     if (Physics.Raycast(ray, out hit))
        //     {
        //         //Debug.DrawLine(ray.origin, hit.point);
        //         //Debug.Log(hit.collider.gameObject.name);
        //         GameObject geometry = hit.collider.gameObject;
        //         //geometry.GetComponent<ScalingController>().TriggerScaling(2);
        //         ScalingController scaler;
        //         if (!geometry.TryGetComponent<ScalingController>(out scaler)) return;
        //         scaler.TriggerScaling(1f + so_playerState.CurrentScaleFactor);
        //         // Rigidbody selectedDiskRB;
        //         // if (!hit.collider) return;
        //         // if (!hit.collider.gameObject.TryGetComponent<Rigidbody>(out selectedDiskRB)) return;
        //     }

        // }
    }
    //TODO Left click to stretch and Right click to shrink.
    public void OnStretch(InputAction.CallbackContext context)
    {
        if (context.performed && m_scalingProvider)
        {
            m_scalingProvider.ShootScalingRay(1.0f, Mouse.current.position.ReadValue());
        }
    }
    public void OnShrink(InputAction.CallbackContext context)
    {
        if (context.performed && m_scalingProvider)
        {
            m_scalingProvider.ShootScalingRay(-1.0f, Mouse.current.position.ReadValue());
        }
    }
    //TODO Ray visual efect
    // [SerializeField] private Transform m_raySpawnPointAtGun;
    // public void RenderScalingRayTracer(Vector2 screenPosition)
    // {
    //     if(!m_raySpawnPointAtGun) return;

    //     Ray ray = Camera.main.ScreenPointToRay(screenPosition);
    //     Debug.DrawLine(m_raySpawnPointAtGun.position, ray.direction + m_raySpawnPointAtGun.position);
    // }
    //TODO
    public void OnScroll(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (context.ReadValue<Vector2>().y > 0f)
                so_playerState.ChangeFactor(1);
            if (context.ReadValue<Vector2>().y < 0f)
                so_playerState.ChangeFactor(-1);
        }
    }
    /// <summary>
    /// The OnNextSpell() is binded to the 'E' key an is beging trigger two times once it is pressed.
    /// The first time represents the start of the action -> context.started
    /// The second time respresents the performing of the action -> context.performed
    /// And a third time when released, meaning the cancellation or end. -> context.canceled
    /// 
    /// </summary>
    /// <method name="context"></method>
    // public void OnNextSpell(InputAction.CallbackContext context)
    // {
    //     if (context.performed)
    //         so_spellCastState.GoToNextSpell();
    // }
    // public void OnPreviousSpell(InputAction.CallbackContext context)
    // {
    //     if (context.performed)
    //         so_spellCastState.GoToPreviousSpell();
    // }
    public static void OnUIPoint(InputAction.CallbackContext context)
    {
        if (m_giveControl)
            Instance.Pointer = context.ReadValue<Vector2>();
    }

    public static void OnUIPressedPoint(InputAction.CallbackContext context)
    {
        if (m_giveControl)
            Instance.MousePressed = context.ReadValue<float>();
    }

    public static void OnUIRayCast(InputAction.CallbackContext context)
    {
        Instance.Pointer = context.ReadValue<Vector2>();
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Instance.Pointer);
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.Log(hit.point);
            Debug.DrawLine(ray.origin, hit.point);
            //if (SpellInvocationManager.Instance)
            //SpellInvocationManager.Instance.UpdateTracer(hit.point);

            // if(SpellTracerManager.Instance)
            // {
            //     SpellTracerManager.Instance.SetPoint(1, hit.point);
            // }
        }

    }
}
