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

    private void Awake()
    {
        m_controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        JumpAndGravity();
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
        // groundedPlayer = m_controller.isGrounded;
        // if (groundedPlayer && playerVelocity.y < 0)
        // {
        //     playerVelocity.y = 0f;
        // }


        /*
        In this model the body lays on the [0,0,0]
        The radious of the character controller is 0.5
        The sphere radious is 0.5, and is elevated 0.4 units from the ground, meaning, 0.1 units are always in contact with the ground.
        */
        //TODO
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + GroundedOffset, transform.position.z);

        Debug.DrawLine(transform.position, spherePosition, Color.yellow);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }

    private void Move()
    {
        Vector3 move = new Vector3(InputManager.Instance.Direction.x, 0, InputManager.Instance.Direction.y);

        move = m_camera.transform.forward * move.z + m_camera.transform.right * move.x; // (vectorForward * scalarZ) + (vectorRight * scalarX)
        move.y = 0.0f;
        m_playerHead.transform.rotation = m_camera.transform.rotation;
        m_controller.Move(so_playerState.WalkingSpeed * Time.fixedDeltaTime * move);
    }
    private void ApplyGravity()
    {
        if(Grounded) return;
        Vector3 gravityForce = new Vector3(0, -9.81f, 0);
        m_controller.Move(Time.fixedDeltaTime * gravityForce);
    }
    //TODO
    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
	public float JumpTimeout = 0.1f;
	[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
	public float FallTimeout = 0.15f;
    // player
    private float _speed;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;

    // timeout deltatime
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    private void JumpAndGravity()
    {
        if (Grounded)
        {
            // reset the fall timeout timer
            _fallTimeoutDelta = FallTimeout;

            // stop our velocity dropping infinitely when grounded
            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            // Ensure the landing sound is played once when grounded again
            // if (!_landSoundPlayed)
            // {
            //     _playerSound.PlayLandSound();
            //     _landSoundPlayed = true;
            // }

            // Jump
            if (/*_input.IsJumpping() && */ _jumpTimeoutDelta <= 0.0f)
            {
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                _jumpTimeoutDelta = JumpTimeout;
                //_playerSound.PlayJumpSound();
                // _landSoundPlayed = false; // Remove this line
            }

            // jump timeout
            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.fixedDeltaTime;
            }
        }
        else
        {
            // reset the jump timeout timer
            _jumpTimeoutDelta = JumpTimeout;

            // fall timeout
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.fixedDeltaTime;
            }

            // if we are not grounded, do not jump
            //_input.SetJump(false);

            // _landSoundPlayed = false; // Remove this line
        }

        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += Gravity * Time.fixedDeltaTime;
        }

        // Reset _landSoundPlayed flag when falling below 0 velocity
        if (!Grounded && _verticalVelocity < 0.0f && _fallTimeoutDelta < 0)
        {
            //_landSoundPlayed = false;
        }
    }

}