using Holders;
using Components;
using UnityEngine;

/// <summary>
/// SoulMage: Portador jugable cuyas habilidades consumen vida.
/// </summary>
public class SoulMage : PlayableStatHolder
{
    // Usa la propiedad Health heredada de PlayableStatHolder
    public void UseHealth(int amount)
    {
        Health?.AffectValue(-amount);
    }

    public int GetCurrentHealth() => Health != null ? Health.CurrentValue : 0;
    public int GetMaxHealth() => Health != null ? Health.MaxValue : 0;

    internal void RobustInitialize(HealthComponent healthComponent, GameObject explosionVFX = null, GameObject projectilePrefab = null)
    {
        // Inicializa solo con Health, ya que no usa Mana
        Initialize(healthComponent, null);
        if (explosionVFX != null)
            SetExplosionVFXPrefab(explosionVFX);
        if (projectilePrefab != null)
            SetProjectilePrefab(projectilePrefab);
        Debug.Log($"SoulMage: RobustInitialize completado. Health asignado: {Health != null}");
    }

    internal void SetExplosionVFXPrefab(GameObject prefab)
    {
        typeof(PlayableStatHolder)
            .GetField("explosionVFXPrefab", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(this, prefab);
    }
    internal void SetProjectilePrefab(GameObject prefab)
    {
        typeof(PlayableStatHolder)
            .GetField("projectilePrefab", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(this, prefab);
    }
}
