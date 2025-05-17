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
        private readonly GameObject _projectilePrefab;
        private readonly float _projectileSpeed;
        private readonly int _projectileDamage;

        public GameObject ProjectilePrefab => _projectilePrefab;
        public float ProjectileSpeed => _projectileSpeed;
        public int ProjectileDamage => _projectileDamage;

        public ProjectileAbility(string name, Sprite icon, int cooldown, int resourceCost, GameObject projectilePrefab, float projectileSpeed, int projectileDamage) 
        : base(name, icon, cooldown, resourceCost)
        {
            _projectilePrefab = projectilePrefab;
            _projectileSpeed = projectileSpeed;
            _projectileDamage = projectileDamage;
        }

        public override bool CanUse(PlayableStatHolder user)
        {
            // Lógica de validación de recursos, cooldown, etc.
            return base.CanUse(user);
        }

        protected override void OnAbilityEffect(PlayableStatHolder user)
        {
            // Si el usuario es SoulMage, consume vida; si es ManaKnight u otro, consume maná
            if (user is SoulMage soulMage)
            {
                if (soulMage.Health == null || soulMage.Health.CurrentValue < ResourceCost)
                    return;
                soulMage.UseHealth(ResourceCost);
            }
            else if (user.Mana != null && user.Mana.CurrentValue >= ResourceCost)
            {
                user.Mana.AffectValue(-ResourceCost, Enums.ManaCondition.Instant);
            }
            else
            {
                return; // No hay suficiente recurso
            }

            if (_projectilePrefab != null && user != null)
            {
                var spawnPos = user.transform.position + user.transform.forward * 1.5f;
                var projectile = GameObject.Instantiate(_projectilePrefab, spawnPos, user.transform.rotation);

                // Ignorar colisión con todos los colliders del jugador que lanza el proyectil
                var projectileCollider = projectile.GetComponent<Collider>();
                var userColliders = user.GetComponentsInChildren<Collider>();
                if (projectileCollider != null && userColliders != null)
                {
                    foreach (var col in userColliders)
                        Physics.IgnoreCollision(projectileCollider, col, true);
                }

                var rb = projectile.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.linearVelocity = user.transform.forward * _projectileSpeed;
                }
                var projDamage = projectile.GetComponent<ProjectileDamage>();
                if (projDamage != null)
                {
                    projDamage.SetDamage(_projectileDamage);
                    projDamage.SetOwner(user.gameObject); // <-- Asigna el lanzador
                }
            }
        }
    }
}
