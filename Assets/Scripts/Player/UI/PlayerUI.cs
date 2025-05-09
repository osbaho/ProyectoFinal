using UnityEngine;
using UnityEngine.UI;
using Components;
using Holders;
using Utils;

public class PlayerUI : MonoBehaviour
{
    public Slider healthBar;
    public Slider resourceBar;

    public MonoBehaviour playerComponent;
    private HealthComponent health;
    private ManaComponent mana;

    public AbilityIconUI[] abilityIcons; // Asigna en el inspector, orden: Heal, Area, Projectile

    private PlayableStatHolder psh;

    void Start()
    {
        var holderMB = playerComponent as MonoBehaviour;
        PlayableStatHolder psh = null;

        if (playerComponent is PlayableStatHolder p)
        {
            psh = p;
            health = psh.Health;
            mana = psh.Mana;
            SetupAbilityIcons();
        }
        else if (playerComponent is NonPlayableStatHolder npsh)
        {
            health = npsh.Health;
            mana = null;
        }
        else if (holderMB != null)
        {
            health = holderMB.GetComponent<HealthComponent>();
            mana = holderMB.GetComponent<ManaComponent>();
        }
        else
        {
            Debug.LogError("PlayerUI: playerComponent no es un tipo válido.");
            health = null;
            mana = null;
        }

        if (playerComponent != null)
        {
            if (health != null)
            {
                UpdateHealthBar(health.CurrentValue, health.MaxValue);
                // Suscribirse a cambios de vida si existe el evento
                health.OnValueChanged += OnHealthChanged;
            }
            if (mana != null)
            {
                UpdateResourceBar(mana.CurrentValue, mana.MaxValue);
                mana.OnValueChanged += OnManaChanged;
            }
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
        UIUtils.SetSliderValue(healthBar, current, max);
    }

    public void UpdateResourceBar(int current, int max)
    {
        UIUtils.SetSliderValue(resourceBar, current, max);
    }
}
