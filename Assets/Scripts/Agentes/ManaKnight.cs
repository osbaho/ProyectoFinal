using UnityEngine;
using Components;
using Holders;

/// <summary>
/// ManaKnight: Portador jugable cuyas habilidades consumen maná.
/// </summary>
public class ManaKnight : PlayableStatHolder
{
    // Usa la propiedad Mana heredada de PlayableStatHolder
    public void UseMana(int amount)
    {
        Mana?.AffectValue(-amount);
    }

    public int GetCurrentMana() => Mana != null ? Mana.CurrentValue : 0;
    public int GetMaxMana() => Mana != null ? Mana.MaxValue : 0;
}
