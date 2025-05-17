using System;
using Interfaces;

public class ManaComponent : StatComponent, IResourceUser
{
    public int MaxMana => MaxValue;
    public int CurrentMana => CurrentValue;

    private ManaCondition _condition = ManaCondition.None;
    public ManaCondition Condition
    {
        get => _condition;
        set => _condition = value;
    }

    public event Action<int, int> OnManaChanged;

    public ManaComponent(int maxValue = 100) : base(maxValue)
    {
        // No invocar OnManaChanged aquí, los suscriptores se agregan después de la construcción.
        OnValueChanged += (cur, max) => OnManaChanged?.Invoke(cur, max);
    }

    public void UseResource(int amount)
    {
        int cost = Math.Max(0, amount);
        SetValue(CurrentMana - cost);
    }

    public void RecoverResource(int amount)
    {
        int gain = Math.Max(0, amount);
        SetValue(CurrentMana + gain);
    }

    public void SetMana(int value)
    {
        SetValue(value);
    }

    public int GetCurrentResource() => CurrentMana;
    public int GetMaxResource() => MaxMana;

    public void UpdateByCondition(float deltaTime)
    {
        switch (Condition)
        {
            case ManaCondition.ByTime:
                RecoverResource((int)(deltaTime * 1)); // Ejemplo: 1 por segundo
                break;
            // Agrega más lógica según el enum
        }
    }
}
