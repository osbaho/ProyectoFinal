using UnityEngine;
using System.Collections.Generic;
using Holders;

public abstract class AbilityBase
{
    public string Name { get; protected set; }
    public Sprite Icon { get; protected set; }
    public float Cooldown { get; protected set; }
    protected float lastUseTime;
    public int ResourceCost { get; protected set; }

    public virtual bool CanUse(PlayableStatHolder user)
    {
        // Verifica cooldown
        if (Time.time < lastUseTime + Cooldown)
            return false;

        // Ya no verifica recursos por IResourceUser aquí
        return true;
    }

    public virtual void Use(PlayableStatHolder user)
    {
        if (!CanUse(user)) return;
        lastUseTime = Time.time;

        // Ya no consume recursos por IResourceUser aquí

        OnAbilityEffect(user); // Aquí se ejecuta el efecto concreto de la habilidad
    }

    // Método abstracto que define el efecto concreto de la habilidad
    protected abstract void OnAbilityEffect(PlayableStatHolder user);
}