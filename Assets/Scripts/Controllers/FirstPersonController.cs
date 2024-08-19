using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private PlayerState so_playerState;
    private CharacterController m_controller;
    private InputManager m_inputManager;
    [Header("Movement attributes")]
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [Tooltip("This represents the real camera, NOT the virtual camera")]
    [SerializeField] private GameObject m_camera;
    [Tooltip("This GameObject get assing to the camera rotatation, it hold everything that need to be within the POV")]
    [SerializeField] private GameObject m_playerHead;
    private float m_verticalVelocity = 0.0f;
    private Vector3 m_currentMovementVector;

    //* WIP jumping --------------------------------------
    [Header("Gravity")]
    [SerializeField] private float m_gravity = -9.80f;
    [SerializeField] private float m_groundedGravity = -0.05f; //TODO
    private bool m_isGrounded = true;
    [Header("Jump")]
    [SerializeField] private float m_maxJumpHeight = 1.0f;
    [SerializeField] private float m_maxJumpTime = 0.5f;
    private bool m_isJumping = false;
    private bool m_isJumpPress = false;
    private float m_initialJumpVelocity;



    //* __________________________________________________
    private void Awake()
    {
        m_controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_currentMovementVector = Vector3.zero;

        //Jump and gravity setup
        float timeToApex = m_maxJumpTime / 2.0f;
        m_gravity = -2 * m_maxJumpHeight / Mathf.Pow(timeToApex, 2);
        m_initialJumpVelocity = 2 * m_maxJumpHeight / timeToApex;
    }
    private void Start()
    {
        //m_inputManager is initialized here because InputManager might be initiatialized after this context.
        if (InputManager.Instance)
            m_inputManager = InputManager.Instance;
        else
            throw new System.NullReferenceException("The Input Manager is missing");
    }

    private void Update()
    {
        //CheckIfJumped();
        //JumpAndGravity();
        CheckIfGrounded();
        Move();
        // /ApplyGravity(); moved inside Move();
        //Debug.Log(m_currentMovementVector);
    }
    private void Move()
    {

        //Vector3 moveTo = new Vector3(InputManager.Instance.Direction.x, m_verticalVelocity, InputManager.Instance.Direction.y);
        //moveTo = m_camera.transform.forward * moveTo.z + m_camera.transform.right * moveTo.x; // (vectorForward * scalarZ) + (vectorRight * scalarX)
        //moveTo.y = 0.0f;
        //m_controller.Move(so_playerState.WalkingSpeed * Time.deltaTime * moveTo);

        //m_currentMovementVector = new Vector3(InputManager.Instance.Direction.x, m_verticalVelocity, InputManager.Instance.Direction.y);
        m_currentMovementVector.x = m_inputManager.Direction.x * so_playerState.WalkingSpeed; // A and D keys
        m_currentMovementVector.z = m_inputManager.Direction.y * so_playerState.WalkingSpeed; //W and S keys

        //m_currentMovementVector = moveTo;// (m_camera.transform.forward * moveTo.z) + (m_camera.transform.right * moveTo.x);
        
        //?m_currentMovementVector = m_camera.transform.forward * m_currentMovementVector.z + m_camera.transform.right * m_currentMovementVector.x;
        //?m_currentMovementVector = so_playerState.WalkingSpeed * m_currentMovementVector * Time.deltaTime;

        // if (m_isGrounded) Handled by ApplyGravity
        //     m_currentMovementVector.y = m_groundedGravity;

        ApplyGravity();
        HandleJump();
        m_controller.Move(m_currentMovementVector  * Time.deltaTime);

        m_playerHead.transform.rotation = m_camera.transform.rotation;

    }
    private void ApplyGravity()
    {
        // if (Grounded) return;
        // Vector3 gravityForce = new Vector3(0, Gravity, 0);
        // m_controller.Move(Time.deltaTime * gravityForce);

        if (m_isGrounded)
            m_currentMovementVector.y = m_groundedGravity;
        else
            m_currentMovementVector.y += m_gravity * Time.deltaTime;
    }


    //* WIP jumping --------------------------------------

    private void HandleJump()
    {
        if (!m_isJumping && m_inputManager.Jump && m_isGrounded)
        {
            m_isJumping = true;
            m_currentMovementVector.y = m_initialJumpVelocity;
        }
        else if(m_isJumping && !m_inputManager.Jump && m_isGrounded)
        {
            m_isJumping = false;
        }
    }

    //* __________________________________________________
    //TODO WIP 
    [Space(10)]
    [Tooltip("The height the player can jump")]
    public float JumpHeight = 1.2f;
    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    public float Gravity = -15.0f;
    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    public bool Grounded = true;
    [Tooltip("Useful for rough ground")]
    public float GroundedOffset = 0.4f;//-0.14f;
    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    public float GroundedRadius = 0.5f;
    [Tooltip("What layers the character uses as ground")]
    public LayerMask GroundLayers;

    private void CheckIfGrounded()
    {
        /*
        In this model the body lays on the [0,0,0]
        The radious of the character controller is 0.5
        The sphere radious is 0.5, and is elevated 0.4 units from the ground, meaning, 0.1 units are always in contact with the ground.
        */
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + GroundedOffset, transform.position.z);
        Debug.DrawLine(transform.position, spherePosition, Color.yellow);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
        m_isGrounded = Grounded; //TODO remove Grounded
        // if (Grounded)
        // {
        //     m_isJumping = false;
        //     m_currentMovementVector.y = m_groundedGravity;
        // }
        // else
        // {
        //     m_currentMovementVector.y = m_gravity;
        // }

    }

    
}