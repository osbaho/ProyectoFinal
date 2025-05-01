using Interfaces;

public class DamageEffect : IAbilityEffect
{
    private int amount;
    public DamageEffect(int amount) { this.amount = amount; }

    public void Apply(IAbilityUser user, IAbilityUser target)
    {
        target?.TakeDamage(amount);
    }
}
