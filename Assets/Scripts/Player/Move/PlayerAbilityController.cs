using UnityEngine;
using Holders;

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
        inputHandler = PlayerInputHandler.Instance;
        statHolder = GetComponent<PlayableStatHolder>();
        playerUI = Object.FindFirstObjectByType<PlayerUI>();
    }

    private void OnEnable()
    {
        if (inputHandler != null) {
            inputHandler.OnHabilidad1Pressed += OnHabilidad1;
            inputHandler.OnHabilidad2Pressed += OnHabilidad2;
            inputHandler.OnHabilidad3Pressed += OnHabilidad3;
        }
    }

    private void OnDisable()
    {
        if (inputHandler != null) {
            inputHandler.OnHabilidad1Pressed -= OnHabilidad1;
            inputHandler.OnHabilidad2Pressed -= OnHabilidad2;
            inputHandler.OnHabilidad3Pressed -= OnHabilidad3;
        }
    }

    private void UseAbilityAndShowCooldown(int index)
    {
        if (statHolder != null)
        {
            var ability = statHolder.AbilitySystem.AllAbilities.Count > index ? statHolder.AbilitySystem.AllAbilities[index] : null;
            if (ability != null)
            {
                statHolder.UseAbility(index);
                if (playerUI != null)
                    playerUI.TriggerAbilityCooldown(index, ability.Cooldown);
            }
        }
    }

    private void OnHabilidad1()
    {
        UseAbilityAndShowCooldown(ProjectileAbilityIndex);
    }

    private void OnHabilidad2()
    {
        UseAbilityAndShowCooldown(HealAbilityIndex);
    }

    private void OnHabilidad3()
    {
        UseAbilityAndShowCooldown(AreaDamageAbilityIndex);
    }
}
