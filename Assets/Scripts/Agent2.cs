using UnityEngine;
using Interfaces;

/// <summary>
/// ManaKnight: Portador jugable cuyas habilidades consumen maná.
/// </summary>
public class ManaKnight : PlayableStatHolder
{
    protected ResourceType resourceType = ResourceType.Mana;

    public ManaKnight(HealthComponent health = null, ManaComponent mana = null)
        : base(null, health, mana)
    {
        resourceType = ResourceType.Mana;
        // Add any additional logic here if needed
    }
}