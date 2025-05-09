using UnityEngine;
using Abilities;
using Holders;
using Components;

namespace Abilities.Habilities
{
    public class AreaDamageAbility : AbilityBase
    {
        private float explosionRadius = 5f; // Radio fijo de la explosión
        private int damage = 30; // Daño de la explosión
        private float explosionChance = 0.9f; // 90% de probabilidad

        public AreaDamageAbility(string name, Sprite icon, float radius = 5f, int damage = 30, int resourceCost = 0)
        {
            Name = name;
            Icon = icon;
            this.explosionRadius = radius;
            this.damage = damage;
            ResourceCost = resourceCost;
        }

        public override bool CanUse(PlayableStatHolder user)
        {
            // Lógica de validación de recursos, cooldown, etc.
            return base.CanUse(user);
        }

        protected override void OnAbilityEffect(PlayableStatHolder user)
        {
            // Determinar si la explosión ocurre (90% probabilidad)
            if (UnityEngine.Random.value <= explosionChance)
            {
                // Ejecutar explosión en área
                Vector3 center = user.transform.position;
                Collider[] hits = Physics.OverlapSphere(center, explosionRadius);
                foreach (var hit in hits)
                {
                    if (hit.gameObject == user.gameObject) continue; // No dañarse a sí mismo
                    var health = hit.GetComponent<HealthComponent>();
                    if (health != null)
                    {
                        health.TakeDamage(damage);
                    }
                }
                // Efecto visual de explosión (opcional)
                // GameObject explosionVFX = ... // Asigna tu prefab de efecto visual
                // GameObject.Instantiate(explosionVFX, center, Quaternion.identity);
            }
            // Si la explosión no ocurre, igual se consumen los recursos (ya lo hace AbilityBase)
        }
    }
}
