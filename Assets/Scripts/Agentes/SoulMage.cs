using Holders;
using Components;

/// <summary>
/// SoulMage: Portador jugable cuyas habilidades consumen vida.
/// </summary>
public class SoulMage : PlayableStatHolder
{
    public HealthComponent HealthComponent;

    public void UseHealth(int amount)
    {
        HealthComponent?.TakeDamage(amount);
    }

    public void RecoverHealth(int amount)
    {
        HealthComponent?.Heal(amount);
    }

    public int GetCurrentHealth() => HealthComponent != null ? HealthComponent.CurrentValue : 0;
    public int GetMaxHealth() => HealthComponent != null ? HealthComponent.MaxValue : 0;
}
