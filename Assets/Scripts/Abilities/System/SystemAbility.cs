using System.Collections.Generic;
using Holders;

public class SystemAbility
{
    private readonly List<AbilityBase> allAbilities = new();
    public IReadOnlyList<AbilityBase> AllAbilities => allAbilities;
    public int SelectedAbilityIndex { get; set; }

    public AbilityBase GetAbilityByName(string abilityName)
    {
        return allAbilities.Find(a => a != null && a.Name == abilityName);
    }

    public void AddAbility(AbilityBase ability)
    {
        if (ability != null && !allAbilities.Contains(ability))
            allAbilities.Add(ability);
    }

    public void RemoveAbility(AbilityBase ability)
    {
        if (ability != null)
            allAbilities.Remove(ability);
    }

    public AbilityBase GetSelectedAbility()
    {
        if (allAbilities.Count == 0) return null;
        int idx = System.Math.Clamp(SelectedAbilityIndex, 0, allAbilities.Count - 1);
        return allAbilities[idx];
    }

    public void UseSelectedAbility(PlayableStatHolder user)
    {
        var ability = GetSelectedAbility();
        if (ability != null)
            ability.Use(user);
    }
}
