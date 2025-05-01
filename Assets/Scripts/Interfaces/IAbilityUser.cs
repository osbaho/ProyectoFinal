using System.Collections.Generic;
using Interfaces;

namespace Interfaces
{
    public interface IAbilityUser
    {
        void AddAbility(IAbility ability);
        void RemoveAbility(IAbility ability);
        void UseAbility(int index);
        IReadOnlyList<IAbility> Abilities { get; }
        void TakeDamage(int amount);
    }
}
