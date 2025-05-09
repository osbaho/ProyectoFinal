using UnityEngine;
using Abilities;
using System;
using System.Collections.Generic;
using Holders;
using Components;

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
            var health = user.GetComponent<HealthComponent>();
            if (user == null || health == null || health.CurrentValue >= health.MaxValue)
                return false;
            return base.CanUse(user);
        }

        protected override void OnAbilityEffect(PlayableStatHolder user)
        {
            var health = user.GetComponent<HealthComponent>();
            if (health == null) return;

            int max = health.MaxValue;
            int current = health.CurrentValue;

            if (current >= 0.9f * max)
            {
                health.Heal(max - current);
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
