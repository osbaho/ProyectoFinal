using UnityEngine;
using Interfaces;
using Holders;

public class FPSController : MonoBehaviour, IMovable
{
    [Header("Movement Speed")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 2f;

    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Look Sensitivity")]
    [SerializeField] private float lookSensitivity = 2f;
    [SerializeField] private float upDownRange = 80f;

    private CharacterController characterController;
    private Camera mainCamera;
    private PlayerInputHandler inputHandler;
    private PlayableStatHolder playerStats;
    private PlayerAbilityController abilityController;
    private Vector3 currentMovement = Vector3.zero;
    private float verticalRotation;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        inputHandler = PlayerInputHandler.Instance;
        playerStats = GetComponent<PlayableStatHolder>();
        abilityController = GetComponent<PlayerAbilityController>(); // Puede ser null si no está en el mismo GameObject
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleJumping();
    }

    public void Move(Vector3 direction, float speed)
    {
        Vector3 worldDirection = transform.TransformDirection(direction);
        worldDirection.Normalize();
        currentMovement.x = worldDirection.x * speed;
        currentMovement.z = worldDirection.z * speed;
        characterController.Move(currentMovement * Time.deltaTime);
    }

    void HandleMovement()
    {
        float speed = walkSpeed * (inputHandler.SprintValue > 0 ? sprintMultiplier : 1f);
        Vector3 inputDirection = new Vector3(inputHandler.MoveInput.x, 0f, inputHandler.MoveInput.y);
        Move(inputDirection, speed);
    }

    void HandleJumping()
    {
        if (characterController.isGrounded)
        {
            currentMovement.y = -0.5f;
            if (inputHandler.JumpTriggered)
            {
                currentMovement.y = jumpForce;
            }
        }
        else
        {
            currentMovement.y -= gravity * Time.deltaTime;
        }
    }

    void HandleRotation()
    {
        float mouseXRotation = inputHandler.LookInput.x * lookSensitivity;
        transform.Rotate(0, mouseXRotation, 0);

        verticalRotation -= inputHandler.LookInput.y * lookSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        mainCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
}
