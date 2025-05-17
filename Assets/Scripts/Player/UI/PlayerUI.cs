using UnityEngine;
using UnityEngine.UI;
using Components;
using Holders;
using Utils;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image healthFill;
    [SerializeField] private Image resourceFill;

    public void SetStatHolder(PlayableStatHolder statHolder)
    {
        if (psh != null || statHolder == null) return;
        psh = statHolder;
        Initialize();
    }

    private HealthComponent health;
    private ManaComponent mana;

    [SerializeField] private AbilityIconUI[] abilityIcons; // Asigna en el inspector, orden: Heal, Area, Projectile

    private PlayableStatHolder psh;

    public void Initialize()
    {        
        health = psh.Health;
        mana = psh.Mana;
        SetupAbilityIcons();
        
        if (healthFill == null)
            Debug.LogWarning("PlayerUI: healthFill no asignado.");
        if (resourceFill == null)
            Debug.LogWarning("PlayerUI: resourceFill no asignado.");

        if (health != null)
        {
            UpdateHealthBar(health.CurrentValue, health.MaxValue);
            health.OnValueChanged += OnHealthChanged;
        }
        if (mana != null)
        {
            UpdateResourceBar(mana.CurrentValue, mana.MaxValue);
            mana.OnValueChanged += OnManaChanged;
        }
    }

    private void SetupAbilityIcons()
    {
        if (psh == null || abilityIcons == null) return;
        var abilities = psh.AbilitySystem.AllAbilities;
        for (int i = 0; i < abilityIcons.Length && i < abilities.Count; i++)
        {
            abilityIcons[i].SetIcon(abilities[i].Icon);
        }
    }

    public void TriggerAbilityCooldown(int index, float cooldown)
    {
        if (abilityIcons != null && index >= 0 && index < abilityIcons.Length)
        {
            abilityIcons[index].StartCooldown(cooldown);
        }
    }

    void OnDestroy()
    {
        if (health != null)
            health.OnValueChanged -= OnHealthChanged;
        if (mana != null)
            mana.OnValueChanged -= OnManaChanged;
    }

    private void OnHealthChanged(Base.StatComponent stat)
    {
        UpdateHealthBar(health.CurrentValue, health.MaxValue);
    }

    private void OnManaChanged(Base.StatComponent stat)
    {
        UpdateResourceBar(mana.CurrentValue, mana.MaxValue);
    }

    public void UpdateHealthBar(int current, int max)
    {
        if (healthFill != null)
            UIUtils.SetFillAmount(healthFill, current, max);
    }

    public void UpdateResourceBar(int current, int max)
    {
        if (resourceFill != null)
            UIUtils.SetFillAmount(resourceFill, current, max);
    }
}
