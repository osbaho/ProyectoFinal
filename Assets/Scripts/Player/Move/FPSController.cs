using UnityEngine;
using Interfaces;
using Holders;

public class FPSController : MonoBehaviour, IMovable
{
    [Header("Movement Speed")]
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float sprintMultiplier = 2f;

    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float gravity = 10f;

    [Header("Look Sensitivity")]
    [SerializeField] private float lookSensitivity = 0.8f;
    [SerializeField] private float upDownRange = 80f;

    [Header("Camera Follow")]
    [SerializeField] private Camera assignedCamera; // Cámara asignada dinámicamente.

    private CharacterController characterController;
    private Camera mainCamera;
    private PlayerInputHandler inputHandler;
    private PlayableStatHolder playerStats;
    private PlayerAbilityController abilityController;
    private Vector3 currentMovement = Vector3.zero;
    private float verticalRotation;
    private float horizontalRotation;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        // Si no se asignó una cámara, intenta buscar una automáticamente.
        if (assignedCamera == null && Camera.main != null)
        {
            assignedCamera = Camera.main;
        }
        mainCamera = assignedCamera != null ? assignedCamera : Camera.main;
        inputHandler = PlayerInputHandler.Instance;
        playerStats = GetComponent<PlayableStatHolder>();
        abilityController = GetComponent<PlayerAbilityController>(); // Puede ser null si no está en el mismo GameObject

        if (characterController == null)
            Debug.LogError("FPSController: Falta CharacterController.");
        if (inputHandler == null)
            Debug.LogError("FPSController: Falta PlayerInputHandler.");
        if (playerStats == null)
            Debug.LogWarning("FPSController: Falta PlayableStatHolder.");
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleJumping();
    }

    public void Move(Vector3 direction, float speed)
    {
        if (characterController == null) return;
        Vector3 worldDirection = transform.TransformDirection(direction);
        worldDirection.Normalize();
        currentMovement.x = worldDirection.x * speed;
        currentMovement.z = worldDirection.z * speed;
        characterController.Move(currentMovement * Time.deltaTime);
    }

    void HandleMovement()
    {
        if (inputHandler == null) return;
        float speed = walkSpeed * (inputHandler.SprintValue > 0 ? sprintMultiplier : 1f);
        Vector3 inputDirection = new Vector3(inputHandler.MoveInput.x, 0f, inputHandler.MoveInput.y);
        Move(inputDirection, speed);
    }

    void HandleJumping()
    {
        if (characterController == null || inputHandler == null) return;
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
        if (inputHandler == null) return;

        // Acumula la rotación horizontal y vertical
        horizontalRotation += inputHandler.LookInput.x * lookSensitivity;
        verticalRotation -= inputHandler.LookInput.y * lookSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);

        // Aplica la rotación al transform y a la cámara
        transform.localRotation = Quaternion.Euler(0, horizontalRotation, 0);

        Camera cam = assignedCamera ? assignedCamera : mainCamera;
        if (cam)
        {
            cam.transform.position = transform.position;
            cam.transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
        }
    }
}
