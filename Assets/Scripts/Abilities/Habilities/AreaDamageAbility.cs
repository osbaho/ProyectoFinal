using UnityEngine;
using Abilities;
using Holders;
using Components;
using Base;

namespace Abilities.Habilities
{
    public class AreaDamageAbility : AbilityBase
    {
        private readonly float _explosionRadius;
        private readonly int _damage;
        private readonly float _explosionChance = 0.9f;
        private readonly GameObject _explosionVFXPrefab;

        public float ExplosionRadius => _explosionRadius;
        public int Damage => _damage;
        public float ExplosionChance => _explosionChance;

        public AreaDamageAbility(
            string name, Sprite icon, float cooldown, int resourceCost,
            float radius, int damage, GameObject explosionVFXPrefab = null)
            : base(name, icon, cooldown, resourceCost)
        {
            _explosionRadius = radius;
            _damage = damage;
            _explosionVFXPrefab = explosionVFXPrefab;
        }

        public override bool CanUse(PlayableStatHolder user)
        {
            return base.CanUse(user);
        }

        protected override void OnAbilityEffect(PlayableStatHolder user)
        {
            // Consumir maná antes de ejecutar la habilidad
            if (user.Mana != null && user.Mana.CurrentValue >= ResourceCost)
                user.Mana.AffectValue(-ResourceCost, Enums.ManaCondition.Instant);
            else
                return; // No hay suficiente maná, no ejecutar la habilidad

            if (Random.value <= _explosionChance)
            {
                Vector3 center = user.transform.position;
                Collider[] hits = Physics.OverlapSphere(center, _explosionRadius);
                foreach (var hit in hits)
                {
                    if (hit.gameObject == user.gameObject) continue;
                    if (hit.TryGetComponent(out BaseStatHolder holder))
                    {
                        holder.Health?.AffectValue(-_damage);
                        Debug.Log($"AreaDamageAbility: {hit.gameObject.name} recibió {_damage} de daño por habilidad de área.");
                    }
                }
                // Instancia el VFX de explosión si está asignado
                if (_explosionVFXPrefab != null)
                    GameObject.Instantiate(_explosionVFXPrefab, center, Quaternion.identity);
            }
        }
    }
}
