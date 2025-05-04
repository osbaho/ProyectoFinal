using Base;

/// <summary>
/// ManaKnight: Portador jugable cuyas habilidades consumen maná.
/// </summary>
public class ManaKnight : ResourceUserAgent
{
    protected override int CurrentResource => holder.Mana != null ? holder.Mana.CurrentMana : 0;
    protected override int MaxResource => holder.Mana != null ? holder.Mana.MaxMana : 0;
    protected override void ConsumeResource(int amount) => holder.Mana?.UseResource(amount);
    protected override void Recover(int amount) => holder.Mana?.RecoverResource(amount);
}
