using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Portador jugable: puede tener habilidades, vida y/o maná por asociación.
/// </summary>
public class PlayableStatHolder : BaseStatHolder
{
    [Header("Stats")]
    public HealthComponent Health;
    public ManaComponent Mana;

    [Header("Abilities")]
    public SystemAbility AbilitySystem = new SystemAbility();

    public void Initialize(HealthComponent health = null, ManaComponent mana = null)
    {
        Health = health;
        Mana = mana;
    }

    public void AddAbility(AbilityBase ability)
    {
        AbilitySystem.AddAbility(ability);
    }

    public void RemoveAbility(AbilityBase ability)
    {
        AbilitySystem.RemoveAbility(ability);
    }

    public void UseAbility(int index)
    {
        AbilitySystem.SelectedAbilityIndex = index;
        AbilitySystem.UseSelectedAbility(this);
    }

    public void TakeDamage(int amount)
    {
        Health?.TakeDamage(amount);
    }
}
