using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 7f;
    public float crouchSpeed = 2.5f;
    public float jumpForce = 8f;
    public float gravity = 20f;
    
    [Header("Mouse Look Settings")]
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 80f;
    
    [Header("Crouch Settings")]
    public float crouchHeight = 1f;
    public float standHeight = 2f;
    
    private CharacterController characterController;
    private Camera playerCamera;
    private Vector3 moveDirection = Vector3.zero;
    private float verticalRotation = 0f;
    private bool isCrouching = false;
    
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();
        
        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void Update()
    {
        HandleMovement();
        HandleMouseLook();
        HandleCrouch();
    }
    
    private void HandleMovement()
    {
        // Get input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        
        // Determine speed based on state
        float currentSpeed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
            currentSpeed = sprintSpeed;
        else if (isCrouching)
            currentSpeed = crouchSpeed;
        
        // Calculate movement direction
        if (characterController.isGrounded)
        {
            Vector3 move = transform.right * moveX + transform.forward * moveZ;
            moveDirection = move * currentSpeed;
            
            // Jump
            if (Input.GetButtonDown("Jump") && !isCrouching)
            {
                moveDirection.y = jumpForce;
            }
        }
        
        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;
        
        // Move the character
        characterController.Move(moveDirection * Time.deltaTime);
    }
    
    private void HandleMouseLook()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        // Rotate player horizontally
        transform.Rotate(Vector3.up * mouseX);
        
        // Rotate camera vertically
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxLookAngle, maxLookAngle);
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
    
    private void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;
            
            if (isCrouching)
            {
                characterController.height = crouchHeight;
            }
            else
            {
                characterController.height = standHeight;
            }
        }
    }
    
    public bool IsCrouching()
    {
        return isCrouching;
    }
    
    public bool IsSprinting()
    {
        return Input.GetKey(KeyCode.LeftShift) && !isCrouching && characterController.isGrounded;
    }
}
