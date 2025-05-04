using Base;

/// <summary>
/// SoulMage: Portador jugable cuyas habilidades consumen vida.
/// </summary>
public class SoulMage : ResourceUserAgent
{
    protected override int CurrentResource => holder.Health != null ? holder.Health.CurrentHealth : 0;
    protected override int MaxResource => holder.Health != null ? holder.Health.MaxHealth : 0;
    protected override void ConsumeResource(int amount) => holder.Health?.TakeDamage(amount);
    protected override void Recover(int amount) => holder.Health?.Heal(amount);
}
