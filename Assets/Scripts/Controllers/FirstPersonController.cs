using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    private CharacterController m_characterController;
    private InputManager m_inputManager;
    [SerializeField] private PlayerState so_playerState;
    [Space(10)]
    [Header("Movement attributes")]
    [Tooltip("This represents the real camera, NOT the virtual camera")]
    [SerializeField] private GameObject m_camera;
    [Tooltip("This GameObject get assing to the camera rotatation, it hold everything that need to be within the POV")]
    [SerializeField] private GameObject m_playerHead;
    private Vector3 m_currentMovementVector;
    [Header("Player Grounded")]
    [Tooltip("Useful for rough ground")]
    [SerializeField] private float m_groundedOffset = 0.4f;//-0.14f;
    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    [SerializeField] private float m_groundedRadius = 0.5f;
    [Tooltip("What layers the character uses as ground")]
    [SerializeField] private LayerMask m_groundLayers;

    //* WIP jumping --------------------------------------
    [Header("Gravity")]
    [SerializeField] private float m_gravity = -9.80f;
    [SerializeField] private float m_groundedGravity = -0.05f;
    private bool m_isGrounded = true;
    [Header("Jump")]
    [SerializeField] private float m_maxJumpHeight = 1.0f;
    [SerializeField] private float m_maxJumpTime = 0.5f;
    private bool m_isJumping = false;
    private float m_initialJumpVelocity;



    //* __________________________________________________
    private void Awake()
    {
        m_characterController = GetComponent<CharacterController>();
        m_characterController.detectCollisions = true;
        
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
        CheckIfGrounded();
        Move();
    }
    private void Move()
    {
        ApplyGravity();
        HandleJump();
        float directionZ = m_inputManager.Direction.y;
        float directionX = m_inputManager.Direction.x;

        m_currentMovementVector.z = (m_camera.transform.forward.z * directionZ + m_camera.transform.right.z * directionX) * so_playerState.WalkingSpeed;
        m_currentMovementVector.x = (m_camera.transform.right.x * directionX + m_camera.transform.forward.x * directionZ) * so_playerState.WalkingSpeed;

        m_characterController.Move(m_currentMovementVector * Time.deltaTime);

        //Aligns the head to the camera and move the gun
        m_playerHead.transform.rotation = m_camera.transform.rotation;
    }
    private void HandleJump()
    {
        if (!m_isJumping && m_inputManager.Jump && m_isGrounded)
        {
            m_isJumping = true;
            m_currentMovementVector.y = m_initialJumpVelocity;
        }
        else if (m_isJumping && !m_inputManager.Jump && m_isGrounded)
        {
            m_isJumping = false;

        }
    }

    private void ApplyGravity()
    {

        if (m_isGrounded)
            m_currentMovementVector.y = m_groundedGravity;
        else
            m_currentMovementVector.y += m_gravity * Time.deltaTime;
    }

    private void CheckIfGrounded()
    {
        /*
        In this model the body lays on the [0,0,0]
        The radious of the character controller is 0.5
        The sphere radious is 0.5, and is elevated 0.4 units from the ground, meaning, 0.1 units are always in contact with the ground.
        */
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + m_groundedOffset, transform.position.z);
        Debug.DrawLine(transform.position, spherePosition, Color.yellow);
        m_isGrounded = Physics.CheckSphere(spherePosition, m_groundedRadius, m_groundLayers, QueryTriggerInteraction.Ignore);
    }


}