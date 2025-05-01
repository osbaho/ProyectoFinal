using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class AreaDamageAbility : AbilityBase
{
    public AreaDamageAbility(string name, Sprite icon, IEnumerable<IAbilityEffect> effects)
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
