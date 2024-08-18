using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private PlayerState so_playerState;
    private CharacterController m_controller;
    [Header("Movement attributes")]
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [Tooltip("This represents the real camera, NOT the virtual camera")]
    [SerializeField] private GameObject m_camera;
    [Tooltip("This GameObject get assing to the camera rotatation, it hold everything that need to be within the POV")]
    [SerializeField] private GameObject m_playerHead;
    private float m_verticalVelocity = 0.0f;

    private void Awake()
    {
        m_controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //CheckIfJumped();
        //JumpAndGravity();
        CheckIfGrounded();
        Move();
        ApplyGravity();
    }
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
    }

    private void Move()
    {

        Vector3 move = new Vector3(InputManager.Instance.Direction.x, m_verticalVelocity, InputManager.Instance.Direction.y);

        move = m_camera.transform.forward * move.z + m_camera.transform.right * move.x; // (vectorForward * scalarZ) + (vectorRight * scalarX)
        //move.y = 0.0f;
        m_playerHead.transform.rotation = m_camera.transform.rotation;
        m_controller.Move(so_playerState.WalkingSpeed * Time.deltaTime * move);
    }
    private void ApplyGravity()
    {
        if (Grounded) return;
        Vector3 gravityForce = new Vector3(0, Gravity, 0);
        m_controller.Move(Time.deltaTime * gravityForce);
    }
}