using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO playerStats;
    [Header("Movement")]
    private float currentHorizontalSpeed;
    [SerializeField] private float inputSmoothTime = 0.025f;


    [Header("Jumping")]
    [SerializeField] private float jumpHeight;
    [SerializeField] private float gravityScale;
    
    private CharacterController _characterController;
    
    private float xMovementInput;
    private float zMovementInput;
    private Vector3 normalizedInput;
    private Vector3 smoothInput;
    private Vector3 smoothedInputRef;
    private Vector3 moveDirection;

    private float yVelocity;

    private bool movementInputEnabled = true;
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (movementInputEnabled)
        {
            DetectHorizontalInput();
            DetectJump();
            DetectSprint();
        }
        HandleFallPhysics();
        ManipulateController();
    }

    private void DetectHorizontalInput()
    {
        xMovementInput = Input.GetAxisRaw("Horizontal");
        zMovementInput = Input.GetAxisRaw("Vertical");
        normalizedInput = new Vector3(xMovementInput, 0, zMovementInput).normalized;
        smoothInput = Vector3.SmoothDamp(smoothInput, normalizedInput, ref smoothedInputRef, inputSmoothTime);
    }

    private void DetectJump()
    {
        if (_characterController.isGrounded && Input.GetButton("Jump"))
        {
            Jump();
        }
    }

    private void DetectSprint()
    {
        if (_characterController.isGrounded && zMovementInput > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            currentHorizontalSpeed = playerStats.SprintSpeed;
        }
        else
        {
            currentHorizontalSpeed = playerStats.WalkSpeed;
        }
    }

    private void ManipulateController()
    {
        moveDirection = transform.rotation * smoothInput;
        moveDirection *= currentHorizontalSpeed;
        moveDirection.y = yVelocity;
        moveDirection *= Time.deltaTime;
        _characterController.Move(moveDirection);
    }

    private void HandleFallPhysics()
    {
        if (yVelocity <= 0 && _characterController.isGrounded)
        {
            yVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            yVelocity += Physics.gravity.y * gravityScale * Time.deltaTime;
        }
    }

    private void Jump()
    {
        yVelocity = Mathf.Sqrt(jumpHeight * -2 * Physics.gravity.y);
    }
}
