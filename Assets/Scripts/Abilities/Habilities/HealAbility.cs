using UnityEngine;
using Holders;
using Components;

namespace Abilities.Habilities
{
    public class HealAbility : AbilityBase
    {
        public HealAbility(string name, Sprite icon, int cooldown, int resourceCost) : base(name, icon, cooldown, resourceCost)
        {
        }

        public override bool CanUse(PlayableStatHolder user)
        {
            var health = user.Health;
            if (user == null || health == null || health.CurrentValue >= health.MaxValue)
                return false;
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

            var health = user.Health;
            if (health == null) return;

            int max = health.MaxValue;
            int current = health.CurrentValue;
            
            if (current >= 0.9f * max)
            {
                health.AffectValue(max);
            }
            else
            {
                int missing = max - current;
                int healAmount = Mathf.CeilToInt(missing * 0.5f);
                health.AffectValue(healAmount);
            }
        }
    }
}
