using UnityEngine;
using Abilities;
using System;
using System.Collections.Generic;
using Holders;

namespace Abilities.Habilities
{
    public class HealAbility : AbilityBase
    {
        public HealAbility(string name, Sprite icon, int resourceCost = 0)
        {
            Name = name;
            Icon = icon;
            ResourceCost = resourceCost;
        }

        public override bool CanUse(PlayableStatHolder user)
        {
            // Puede usarse si el usuario tiene componente de salud y no está ya al máximo y tiene recursos suficientes
            if (user == null || user.Health == null || user.Health.CurrentHealth >= user.Health.MaxHealth)
                return false;
            return base.CanUse(user);
        }

        // No es necesario consumir recursos aquí, ya lo hace AbilityBase.Use
        protected override void OnAbilityEffect(PlayableStatHolder user)
        {
            var health = user.Health;
            if (health == null) return;

            int max = health.MaxHealth;
            int current = health.CurrentHealth;

            if (current >= 0.9f * max)
            {
                health.SetHealth(max);
            }
            else
            {
                int missing = max - current;
                int healAmount = Mathf.CeilToInt(missing * 0.5f);
                health.Heal(healAmount);
            }
        }
    }
}
