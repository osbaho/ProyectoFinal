using System.Collections.Generic;
using Interfaces;

public class SystemAbility
{
    public List<IAbility> AllAbilities { get; } = new();
    public int SelectedAbilityIndex { get; set; }

    private static SystemAbility _instance;
    public static SystemAbility Instance
    {
        get
        {
            if (_instance == null)
                _instance = new SystemAbility();
            return _instance;
        }
        set { _instance = value; }
    }

    private readonly Dictionary<int, int> abilityUseCounts = new();

    public IAbility GetAbilityByName(string abilityName)
    {
        return AllAbilities.Find(a => a != null && a.Name == abilityName);
    }

    public void AddAbility(IAbility ability)
    {
        if (ability != null && !AllAbilities.Contains(ability))
            AllAbilities.Add(ability);
    }

    public void RemoveAbility(IAbility ability)
    {
        if (ability != null)
            AllAbilities.Remove(ability);
    }

    public IAbility GetSelectedAbility()
    {
        if (AllAbilities.Count == 0) return null;
        int idx = UnityEngine.Mathf.Clamp(SelectedAbilityIndex, 0, AllAbilities.Count - 1);
        return AllAbilities[idx];
    }

    public void UseSelectedAbility(IAbilityUser user)
    {
        var ability = GetSelectedAbility();
        if (ability != null)
            ability.Use(user);
    }

    public void ReportAbilityUsed(IAbility ability)
    {
        int idx = AllAbilities.IndexOf(ability);
        if (idx >= 0)
        {
            if (!abilityUseCounts.ContainsKey(idx))
                abilityUseCounts[idx] = 0;
            abilityUseCounts[idx]++;
        }
    }

    public int GetAbilityUseCountByIndex(int index)
    {
        return abilityUseCounts.TryGetValue(index, out int count) ? count : 0;
    }

    public int GetAbilityUseCount(IAbility ability)
    {
        int idx = AllAbilities.IndexOf(ability);
        return GetAbilityUseCountByIndex(idx);
    }
}
