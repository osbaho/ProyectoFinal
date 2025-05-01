using System.Collections.Generic;
using UnityEngine;

namespace Interfaces
{
    public interface IAbility
    {
        string Name { get; }
        Sprite Icon { get; }
        bool CanUse(IAbilityUser user);
        void Use(IAbilityUser user);
        IReadOnlyList<IAbilityEffect> Effects { get; }
    }

    public interface IAbilityEffect
    {
        void Apply(IAbilityUser user, IAbilityUser target);
    }
}
