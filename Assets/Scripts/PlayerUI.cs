using UnityEngine;
using UnityEngine.UI;
using Interfaces;

public class PlayerUI : MonoBehaviour
{
    public Slider healthBar;
    public Slider resourceBar;
    public Image[] abilityIcons;
    public Text[] abilityNames;

    public IAbilityUser playerComponent;
    private IAbilityUser statHolder;
    private HealthComponent health;
    private ManaComponent mana;

    void Start()
    {
        statHolder = playerComponent;

        // Acceso a Health y Mana por el sistema de componentes
        health = (statHolder as PlayableStatHolder)?.GetStat<HealthComponent>();
        mana = (statHolder as PlayableStatHolder)?.GetStat<ManaComponent>();

        if (statHolder != null)
        {
            if (health != null)
            {
                health.OnHealthChanged += UpdateHealthBar;
                UpdateHealthBar(health.CurrentHealth, health.MaxHealth);
            }
            if (mana != null)
            {
                mana.OnManaChanged += UpdateResourceBar;
                UpdateResourceBar(mana.CurrentMana, mana.MaxMana);
            }
            UpdateAbilities();
        }

        if (statHolder is PlayableStatHolder psh)
        {
            psh.OnAbilityAdded += _ => UpdateAbilities();
            psh.OnAbilityRemoved += _ => UpdateAbilities();
        }
    }

    void OnDestroy()
    {
        if (health != null)
            health.OnHealthChanged -= UpdateHealthBar;
        if (mana != null)
            mana.OnManaChanged -= UpdateResourceBar;

        if (statHolder is PlayableStatHolder psh)
        {
            psh.OnAbilityAdded -= _ => UpdateAbilities();
            psh.OnAbilityRemoved -= _ => UpdateAbilities();
        }
    }

    void UpdateHealthBar(int current, int max)
    {
        UIUtils.SetSliderValue(healthBar, current, max);
    }

    void UpdateResourceBar(int current, int max)
    {
        UIUtils.SetSliderValue(resourceBar, current, max);
    }

    void UpdateAbilities()
    {
        if (statHolder == null) return;
        var abilities = statHolder.Abilities;
        if (abilities != null && abilityIcons != null && abilityNames != null)
        {
            int count = Mathf.Min(abilities.Count, abilityIcons.Length, abilityNames.Length);
            for (int i = 0; i < count; i++)
            {
                var ability = abilities[i];
                if (ability != null)
                {
                    if (abilityIcons[i] != null)
                        abilityIcons[i].sprite = ability.Icon;
                    if (abilityNames[i] != null)
                        abilityNames[i].text = ability.Name;
                }
            }
        }
    }
}
