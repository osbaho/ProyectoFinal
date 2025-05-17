using UnityEngine;
using Components;
using Holders;

/// <summary>
/// ManaKnight: Portador jugable cuyas habilidades consumen maná.
/// </summary>
public class ManaKnight : PlayableStatHolder
{
    // Usa la propiedad Mana heredada de PlayableStatHolder
    public void UseMana(int amount)
    {
        Mana?.AffectValue(-amount);
    }

    public int GetCurrentMana() => Mana != null ? Mana.CurrentValue : 0;
    public int GetMaxMana() => Mana != null ? Mana.MaxValue : 0;

    // Métodos de inicialización robusta y setters de prefabs pueden ser internal para limitar acceso externo
    internal void RobustInitialize(ManaComponent manaComponent, HealthComponent healthComponent, GameObject explosionVFX = null, GameObject projectilePrefab = null)
    {
        // Usa los métodos públicos de PlayableStatHolder para inicializar
        Initialize(healthComponent, manaComponent);
        if (explosionVFX != null)
            SetExplosionVFXPrefab(explosionVFX);
        if (projectilePrefab != null)
            SetProjectilePrefab(projectilePrefab);
        Debug.Log($"ManaKnight: RobustInitialize completado. Mana asignado: {Mana != null}, Health asignado: {Health != null}");
    }

    // Métodos para exponer la asignación de prefabs de forma segura
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
