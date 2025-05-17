using UnityEngine;
using Holders;
using System.Linq;

public class PlayerAbilityController : MonoBehaviour
{
    private PlayerInputHandler inputHandler;
    private PlayableStatHolder statHolder;
    private PlayerUI playerUI;

    // Índices de habilidades según el orden de inicialización
    private const int ProjectileAbilityIndex = 2;
    private const int HealAbilityIndex = 0;
    private const int AreaDamageAbilityIndex = 1;

    private void Awake()
    {
        // Busca el input handler en toda la escena (singleton)
        inputHandler = PlayerInputHandler.Instance;
        // Busca el statHolder en este objeto o en hijos
        statHolder = GetComponentInChildren<PlayableStatHolder>();
        // No busques PlayerUI aquí
    }

    private void Start()
    {
        // Busca la UI en toda la escena
        playerUI = Object.FindFirstObjectByType<PlayerUI>();
        if (statHolder == null)
            Debug.LogWarning("PlayerAbilityController: Falta PlayableStatHolder (ni en este objeto ni en hijos).");
        if (playerUI == null)
            Debug.LogWarning("PlayerAbilityController: Falta PlayerUI.");
    }

    private void OnEnable()
    {
        if (inputHandler != null)
        {
            inputHandler.OnHabilidad1Pressed += OnHabilidad1;
            inputHandler.OnHabilidad2Pressed += OnHabilidad2;
            inputHandler.OnHabilidad3Pressed += OnHabilidad3;
        }
    }

    private void OnDisable()
    {
        if (inputHandler != null)
        {
            inputHandler.OnHabilidad1Pressed -= OnHabilidad1;
            inputHandler.OnHabilidad2Pressed -= OnHabilidad2;
            inputHandler.OnHabilidad3Pressed -= OnHabilidad3;
        }
    }

    private void OnHabilidad1()
    {
        Debug.Log("PlayerAbilityController: OnHabilidad1 invocado");
        UseAbilityAndShowCooldown(0);
    }
    private void OnHabilidad2()
    {
        Debug.Log("PlayerAbilityController: OnHabilidad2 invocado");
        UseAbilityAndShowCooldown(1);
    }
    private void OnHabilidad3()
    {
        Debug.Log("PlayerAbilityController: OnHabilidad3 invocado");
        UseAbilityAndShowCooldown(2);
    }

    private void UseAbilityAndShowCooldown(int index)
    {
        Debug.Log($"PlayerAbilityController: Intentando usar habilidad {index}");
        if (statHolder != null)
        {
            var abilities = statHolder.AbilitySystem.AllAbilities;
            var ability = abilities.Count > index ? abilities[index] : null;
            if (ability != null)
            {
                Debug.Log($"PlayerAbilityController: Usando habilidad {ability.Name}");
                bool used = statHolder.UseAbility(index);
                if (used && playerUI != null)
                    playerUI.TriggerAbilityCooldown(index, ability.Cooldown);
            }
            else
            {
                string abilityNames = abilities.Count > 0 ? string.Join(", ", abilities.Select(a => a.Name)) : "Ninguna";
                Debug.LogWarning($"PlayerAbilityController: No hay habilidad en el índice {index}. Total habilidades: {abilities.Count}. Habilidades: {abilityNames}");
            }
        }
        else
        {
            Debug.LogWarning("PlayerAbilityController: statHolder es null");
        }
    }
}
