using UnityEngine;
using System;
using Components;

/// <summary>
/// Portador no jugable: solo emplea vida, destruido al morir.
/// </summary>
public class NonPlayableStatHolder : MonoBehaviour
{
    [Header("Stats")]
    public HealthComponent Health;

    public event Action OnDeath;

    private void Start()
    {
        if (Health != null)
            Health.OnHealthChanged += CheckDeath;
    }

    private void OnDestroy()
    {
        if (Health != null)
            Health.OnHealthChanged -= CheckDeath;
    }

    private void CheckDeath(int current, int max)
    {
        if (current <= 0)
        {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }
}
