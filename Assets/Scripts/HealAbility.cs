using UnityEngine;
using System.Collections.Generic;
using Interfaces;

public class HealAbility : AbilityBase
{
    public HealAbility(string name, Sprite icon, IEnumerable<IAbilityEffect> effects)
    {
        Name = name;
        Icon = icon;
        this.effects.AddRange(effects ?? new List<IAbilityEffect>());
    }

    public override bool CanUse(IAbilityUser user)
    {
        // Lógica de validación de recursos, cooldown, etc.
        return true;
    }
}
