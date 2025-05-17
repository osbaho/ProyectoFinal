using UnityEngine;
using System;
using Components;
using Base;

/// <summary>
/// Portador no jugable: solo emplea vida, destruido al morir.
/// </summary>
public class NonPlayableStatHolder : BaseStatHolder
{
    public event Action OnDeath;

    private void Update()
    {
        if (Health != null && Health.CurrentValue <= 0)
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }

    public void CheckDeath()
    {
        if (Health != null && Health.CurrentValue <= 0)
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}
