using Components;
using Holders;

/// <summary>
/// ManaKnight: Portador jugable cuyas habilidades consumen maná.
/// </summary>
public class ManaKnight : PlayableStatHolder
{
    public ManaComponent ManaComponent;

    public void UseMana(int amount)
    {
        ManaComponent?.UseResource(amount);
    }

    public int GetCurrentMana() => ManaComponent != null ? ManaComponent.CurrentValue : 0;
    public int GetMaxMana() => ManaComponent != null ? ManaComponent.MaxValue : 0;
}
