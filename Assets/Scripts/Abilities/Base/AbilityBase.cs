using UnityEngine;
using System.Collections.Generic;
using Holders;

public abstract class AbilityBase
{
    private readonly string _name;
    private readonly Sprite _icon;
    private readonly float _cooldown;
    private readonly int _resourceCost;
    [System.NonSerialized]
    protected float lastUseTime;

    public string Name => _name;
    public Sprite Icon => _icon;
    public float Cooldown => _cooldown;
    public int ResourceCost => _resourceCost;

    protected AbilityBase(string name, Sprite icon, float cooldown, int resourceCost)
    {
        _name = name;
        _icon = icon;
        _cooldown = cooldown;
        lastUseTime = 0f;
        _resourceCost = resourceCost;
    }

    public virtual bool CanUse(PlayableStatHolder user)
    {
        if (Time.time < lastUseTime + _cooldown)
            return false;
        // Si la habilidad requiere maná, verifica que haya suficiente
        if (ResourceCost > 0 && user != null && user.Mana != null && user.Mana.CurrentValue < ResourceCost)
            return false;
        return true;
    }

    public virtual void Use(PlayableStatHolder user)
    {
        if (!CanUse(user))
        {
            Debug.Log($"AbilityBase: No se puede usar {Name} (cooldown o condición)");
            return;
        }
        // Solo se actualiza el cooldown si la habilidad se usa realmente
        lastUseTime = Time.time;
        Debug.Log($"AbilityBase: Usando habilidad {Name}");
        OnAbilityEffect(user);
    }

    protected abstract void OnAbilityEffect(PlayableStatHolder user);
}