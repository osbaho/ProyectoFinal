using System.Collections.Generic;
using UnityEngine;
using Interfaces; 

public abstract class AbilityBase : IAbility
{
    public string Name { get; protected set; }
    public Sprite Icon { get; protected set; }
    protected List<IAbilityEffect> effects = new();
    public IReadOnlyList<IAbilityEffect> Effects => effects;

    public abstract bool CanUse(IAbilityUser user);
    public virtual void Use(IAbilityUser user)
    {
        foreach (var effect in Effects)
            effect.Apply(user, null);
    }
}
