using System.Collections.Generic;

public class SystemAbility
{
    public List<AbilityBase> AllAbilities { get; } = new();
    public int SelectedAbilityIndex { get; set; }

    public AbilityBase GetAbilityByName(string abilityName)
    {
        return AllAbilities.Find(a => a != null && a.Name == abilityName);
    }

    public void AddAbility(AbilityBase ability)
    {
        if (ability != null && !AllAbilities.Contains(ability))
            AllAbilities.Add(ability);
    }

    public void RemoveAbility(AbilityBase ability)
    {
        if (ability != null)
            AllAbilities.Remove(ability);
    }

    public AbilityBase GetSelectedAbility()
    {
        if (AllAbilities.Count == 0) return null;
        int idx = System.Math.Clamp(SelectedAbilityIndex, 0, AllAbilities.Count - 1);
        return AllAbilities[idx];
    }

    public void UseSelectedAbility(PlayableStatHolder user)
    {
        var ability = GetSelectedAbility();
        if (ability != null)
            ability.Use(user);
    }
}
