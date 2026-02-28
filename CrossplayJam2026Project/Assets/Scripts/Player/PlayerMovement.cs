using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private PlayerInput input;

    private CharacterController controller;
    private Vector2 move;
    private Vector3 playerVel;
    public bool grounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        //ground check and saftey adjustment
        grounded = controller.isGrounded;
        if (grounded && playerVel.y < 0)
        {
            playerVel.y = -2f;
        }

        //move logic
        move = input.actions["Move"].ReadValue<Vector2>();
        //if not locked on, move normally, if locked on, move perpinduclar to target
        Vector3 moveDirection = Vector3.zero;
        moveDirection = (transform.right * move.x + transform.forward * move.y).normalized;

        controller.Move(moveDirection * playerSpeed * Time.deltaTime);

        //jump logic
        if (grounded && input.actions["Jump"].triggered)
        {
            playerVel.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //apply gravity
        playerVel.y += gravity * Time.deltaTime;
        controller.Move(playerVel * Time.deltaTime);
    }
}