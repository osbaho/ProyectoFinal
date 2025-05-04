using System;

public abstract class StatComponent
{
    protected int maxValue = 100;
    protected int currentValue;

    public int MaxValue => maxValue;
    public int CurrentValue => currentValue;

    public event Action<int, int> OnValueChanged;

    public StatComponent(int maxValue = 100)
    {
        this.maxValue = maxValue;
        currentValue = maxValue;
    }

    public virtual void SetValue(int value)
    {
        int clamped = Math.Clamp(value, 0, maxValue);
        if (clamped != currentValue)
        {
            currentValue = clamped;
            OnValueChanged?.Invoke(currentValue, maxValue);
        }
    }
}
