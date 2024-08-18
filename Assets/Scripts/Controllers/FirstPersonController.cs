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

    private void Update()
    {
        CheckIfGrounded();
        Move();
    }

    private void CheckIfGrounded()
    {
        groundedPlayer = m_controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
    }

    private void Move()
    {
        Vector3 move = new Vector3(InputManager.Instance.Direction.x, 0, InputManager.Instance.Direction.y);

        move = m_camera.transform.forward * move.z + m_camera.transform.right * move.x; // (vectorForward * scalarZ) + (vectorRight * scalarX)
        move.y = 0.0f;
        m_playerHead.transform.rotation = m_camera.transform.rotation;
        m_controller.Move(so_playerState.WalkingSpeed * Time.deltaTime * move);
    }

}