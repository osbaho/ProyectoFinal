using UnityEngine;
using UnityEngine.InputSystem;
using Interfaces;

public class PlayerInputHandler : MonoBehaviour, IPlayerInput
{
    [Header("Input Action Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name Reference")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name Reference")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string look = "Look";
    [SerializeField] private string jump = "Jump";
    [SerializeField] private string sprint = "Sprint";
    [SerializeField] private string habilidad1 = "Habilidad1";
    [SerializeField] private string habilidad2 = "Habilidad2";
    [SerializeField] private string habilidad3 = "Habilidad3";

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    private InputAction habilidad1Action;
    private InputAction habilidad2Action;
    private InputAction habilidad3Action;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; } 
    public bool JumpTriggered { get; private set; }
    public float SprintValue { get; private set; } 
    public bool Habilidad1Triggered { get; private set; }
    public bool Habilidad2Triggered { get; private set; }
    public bool Habilidad3Triggered { get; private set; }

    public static PlayerInputHandler Instance { get; private set; }

    // Nuevo evento para habilidad1
    public event System.Action OnHabilidad1Pressed;
    // Nuevo evento para habilidad2
    public event System.Action OnHabilidad2Pressed;
    // Nuevo evento para habilidad3
    public event System.Action OnHabilidad3Pressed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        if (transform.parent != null)
        {
            transform.SetParent(null); // Lo hace root antes de llamar a DontDestroyOnLoad
            // Ya no es necesario mostrar el warning, el objeto ya es root.
        }
        DontDestroyOnLoad(gameObject);

        if (playerControls == null)
        {
            Debug.LogError("PlayerInputHandler: Falta InputActionAsset.");
            return;
        }
        var actionMap = playerControls.FindActionMap(actionMapName);
        if (actionMap == null)
        {
            Debug.LogError($"PlayerInputHandler: No se encontró ActionMap '{actionMapName}'.");
            return;
        }

        moveAction = actionMap.FindAction(move);
        lookAction = actionMap.FindAction(look);
        jumpAction = actionMap.FindAction(jump);
        sprintAction = actionMap.FindAction(sprint);
        habilidad1Action = actionMap.FindAction(habilidad1);
        habilidad2Action = actionMap.FindAction(habilidad2);
        habilidad3Action = actionMap.FindAction(habilidad3);

        RegisterInputActions();
    }

    private void OnDestroy()
    {
        // Limpieza de eventos para evitar fugas de memoria
        OnHabilidad1Pressed = null;
        OnHabilidad2Pressed = null;
        OnHabilidad3Pressed = null;
    }

    void RegisterInputActions()
    {
        if (moveAction == null)
            Debug.LogError("PlayerInputHandler: Falta la acción de input 'Move'.");
        if (lookAction == null)
            Debug.LogError("PlayerInputHandler: Falta la acción de input 'Look'.");
        if (jumpAction == null)
            Debug.LogError("PlayerInputHandler: Falta la acción de input 'Jump'.");
        if (sprintAction == null)
            Debug.LogError("PlayerInputHandler: Falta la acción de input 'Sprint'.");
        if (habilidad1Action == null)
            Debug.LogError("PlayerInputHandler: Falta la acción de input 'Habilidad1'.");
        if (habilidad2Action == null)
            Debug.LogError("PlayerInputHandler: Falta la acción de input 'Habilidad2'.");
        if (habilidad3Action == null)
            Debug.LogError("PlayerInputHandler: Falta la acción de input 'Habilidad3'.");

        if (moveAction == null || lookAction == null || jumpAction == null || sprintAction == null ||
            habilidad1Action == null || habilidad2Action == null || habilidad3Action == null)
        {
            Debug.LogError("PlayerInputHandler: Falta alguna acción de input. Revisa el InputActionAsset y los nombres en el inspector.");
            return;
        }

        moveAction.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        moveAction.canceled += ctx => MoveInput = Vector2.zero;

        lookAction.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
        lookAction.canceled += ctx => LookInput = Vector2.zero;

        jumpAction.performed += ctx => JumpTriggered = true;
        jumpAction.canceled += ctx => JumpTriggered = false;

        sprintAction.performed += ctx => SprintValue = ctx.ReadValue<float>();
        sprintAction.canceled += ctx => SprintValue = 0f;

        habilidad1Action.performed += ctx => {
            Habilidad1Triggered = true;
            OnHabilidad1Pressed?.Invoke();
        };
        habilidad1Action.canceled += ctx => Habilidad1Triggered = false;

        habilidad2Action.performed += ctx => {
            Habilidad2Triggered = true;
            OnHabilidad2Pressed?.Invoke();
        };
        habilidad2Action.canceled += ctx => Habilidad2Triggered = false;

        habilidad3Action.performed += ctx => {
            Habilidad3Triggered = true;
            OnHabilidad3Pressed?.Invoke();
        };
        habilidad3Action.canceled += ctx => Habilidad3Triggered = false;
    }

    private void OnEnable()
    {
        moveAction?.Enable();
        lookAction?.Enable();
        jumpAction?.Enable();
        sprintAction?.Enable();
        habilidad1Action?.Enable();
        habilidad2Action?.Enable();
        habilidad3Action?.Enable();
    }

    private void OnDisable()
    {
        moveAction?.Disable();
        lookAction?.Disable();
        jumpAction?.Disable();
        sprintAction?.Disable();
        habilidad1Action?.Disable();
        habilidad2Action?.Disable(); 
        habilidad3Action?.Disable();
    }
}
