using UnityEngine;
using System;
using System.Collections.Generic;

public class HealAbility : AbilityBase
{
    public HealAbility(string name, Sprite icon)
    {
        Name = name;
        Icon = icon;
    }

    public override bool CanUse(PlayableStatHolder user)
    {
        // Puede usarse si el usuario tiene componente de salud y no está ya al máximo
        return user != null && user.Health != null && user.Health.CurrentHealth < user.Health.MaxHealth;
    }

    public override void Use(PlayableStatHolder user)
    {
        if (!CanUse(user)) return;
        lastUseTime = Time.time;

        OnAbilityEffect(user);
    }

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
