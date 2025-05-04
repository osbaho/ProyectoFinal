using UnityEngine;
using Abilities;
using Holders;

namespace Abilities.Habilities
{
    /// <summary>
    /// ProjectileAbility
    /// Es una clase de habilidad (deriva de AbilityBase).
    /// Su función es crear e instanciar un proyectil cuando el jugador usa la habilidad.
    /// Se encarga de la lógica de lanzamiento: posición de aparición, dirección, velocidad, etc.
    /// No maneja el daño directamente, solo crea el proyectil.
    /// </summary>
    public class ProjectileAbility : AbilityBase
    {
        public GameObject projectilePrefab;
        public float projectileSpeed = 20f;

        public ProjectileAbility(string name, Sprite icon, GameObject projectilePrefab = null, float projectileSpeed = 20f, int resourceCost = 0)
        {
            Name = name;
            Icon = icon;
            this.projectilePrefab = projectilePrefab;
            this.projectileSpeed = projectileSpeed;
            ResourceCost = resourceCost;
        }

        public override bool CanUse(PlayableStatHolder user)
        {
            // Lógica de validación de recursos, cooldown, etc.
            return base.CanUse(user);
        }

        protected override void OnAbilityEffect(PlayableStatHolder user)
        {
            // Instanciar un proyectil y lanzarlo hacia adelante desde el usuario
            if (projectilePrefab != null && user != null)
            {
                var spawnPos = user.transform.position + user.transform.forward * 1.5f;
                var projectile = GameObject.Instantiate(projectilePrefab, spawnPos, user.transform.rotation);
                var rb = projectile.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = user.transform.forward * projectileSpeed;
                }
            }
        }
    }
}
