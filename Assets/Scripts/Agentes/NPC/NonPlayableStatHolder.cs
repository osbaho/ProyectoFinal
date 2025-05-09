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
        // Chequea la muerte cada frame
        if (Health != null && Health.CurrentValue <= 0)
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }
    // Llama a este método desde donde corresponda cuando la vida cambie.
    public void CheckDeath()
    {
        if (Health != null && Health.CurrentValue <= 0)
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}
