using System;
using Interfaces;

public class HealthComponent : StatComponent, IHealth
{
    public int MaxHealth => MaxValue;
    public int CurrentHealth => CurrentValue;

    public event Action<int, int> OnHealthChanged;

    public HealthComponent(int maxValue = 100) : base(maxValue > 0 ? maxValue : 1)
    {
        // No invocar OnHealthChanged aquí, los suscriptores se agregan después de la construcción.
        OnValueChanged += (cur, max) => OnHealthChanged?.Invoke(cur, max);
    }

    public void TakeDamage(int amount)
    {
        int dmg = Math.Max(0, amount);
        SetValue(CurrentHealth - dmg);
    }

    public void Heal(int amount)
    {
        int heal = Math.Max(0, amount);
        SetValue(CurrentHealth + heal);
    }

    public void SetHealth(int value)
    {
        SetValue(value);
    }
}
