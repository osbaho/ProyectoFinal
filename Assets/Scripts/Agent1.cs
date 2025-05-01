using UnityEngine;
using Interfaces;

/// <summary>
/// SoulMage: Portador jugable cuyas habilidades consumen vida.
/// </summary>
public class SoulMage : PlayableStatHolder
{
    protected ResourceType resourceType = ResourceType.Health;

    public SoulMage(HealthComponent health, ManaComponent mana = null)
        : base(null, health, mana)
    {
        resourceType = ResourceType.Health;
        // Lógica adicional para SoulMage si es necesario
    }
}