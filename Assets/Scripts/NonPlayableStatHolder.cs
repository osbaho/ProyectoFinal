using UnityEngine;
using System;

/// <summary>
/// Portador no jugable: solo emplea vida, destruido al morir.
/// </summary>
public class NonPlayableStatHolder : StatHolder
{
    public event Action OnDeath;

    public NonPlayableStatHolder(params StatComponent[] components)
        : base(components)
    {
    }

    protected void Start()
    {
        var health = GetStat<HealthComponent>();
        if (health != null)
            health.OnHealthChanged += CheckDeath;
    }

    private void OnDestroy()
    {
        var health = GetStat<HealthComponent>();
        if (health != null)
            health.OnHealthChanged -= CheckDeath;
    }

    private void CheckDeath(int current, int max)
    {
        if (current <= 0)
        {
            OnDeath?.Invoke();
        }
    }
}
