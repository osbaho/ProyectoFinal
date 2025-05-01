using System.Collections.Generic;
using Interfaces;

/// <summary>
/// Portador jugable: puede tener habilidades, vida y/o maná por asociación.
/// </summary>
public class PlayableStatHolder : StatHolder, IAbilityUser
{
    private readonly List<IAbility> abilities = new();
    public IReadOnlyList<IAbility> Abilities => abilities.AsReadOnly();

    public event System.Action<IAbility> OnAbilityAdded;
    public event System.Action<IAbility> OnAbilityRemoved;

    public PlayableStatHolder(IEnumerable<IAbility> initialAbilities = null, params StatComponent[] components)
        : base(components)
    {
        if (initialAbilities != null)
            abilities.AddRange(initialAbilities);
    }

    public void AddAbility(IAbility ability)
    {
        if (ability != null && !abilities.Contains(ability))
        {
            abilities.Add(ability);
            OnAbilityAdded?.Invoke(ability);
        }
    }

    public void RemoveAbility(IAbility ability)
    {
        if (ability != null && abilities.Remove(ability))
        {
            OnAbilityRemoved?.Invoke(ability);
        }
    }

    public void ClearAbilities()
    {
        abilities.Clear();
    }

    public void UseAbility(int index)
    {
        if (index >= 0 && index < abilities.Count)
            abilities[index].Use(this);
    }

    public void TakeDamage(int amount)
    {
        var health = GetStat<HealthComponent>();
        health?.TakeDamage(amount);
    }
}
