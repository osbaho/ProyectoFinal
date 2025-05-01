using System.Collections.Generic;
using UnityEngine;
using Interfaces;

public class ProjectileAbility : AbilityBase
{
    public ProjectileAbility(string name, Sprite icon, IEnumerable<IAbilityEffect> effects)
    {
        Name = name;
        Icon = icon;
        this.effects.AddRange(effects ?? new List<IAbilityEffect>());
    }

    public override bool CanUse(IAbilityUser user)
    {
        return true;
    }
}
