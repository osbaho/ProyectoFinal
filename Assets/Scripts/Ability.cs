using UnityEngine;
using Interfaces;

[System.Obsolete("Usa AbilityBase y el sistema de IAbility para nuevas habilidades.")]
public abstract class Ability : ScriptableObject
{
    public string abilityName;
    public Sprite icon;
    public float cooldown;
    protected float lastUseTime;
    public int resourceCost;
    public int useCount = 0; // Conteo individual

    public virtual bool CanUse(PlayableStatHolder user = null)
    {
        // Si requiere recurso, verifica que tenga suficiente
        var resourceUser = user as IResourceUser;
        if (resourceUser != null && resourceCost > 0)
        {
            if (resourceUser.GetCurrentResource() < resourceCost)
                return false;
        }
        return Time.time >= lastUseTime + cooldown;
    }

    // Lógica común para consumir recurso
    protected bool TryConsumeResource(PlayableStatHolder user)
    {
        var resourceUser = user as IResourceUser;
        if (resourceUser == null) return true; // Si no requiere recurso, permite usar
        if (resourceUser.GetCurrentResource() < resourceCost) return false;
        resourceUser.UseResource(resourceCost);
        return true;
    }

    // Lógica común al usar la habilidad (puede ser sobrescrita para efectos, logs, etc.)
    protected virtual void OnAbilityUsed(PlayableStatHolder user)
    {
        useCount++;
        // SystemAbility.Instance?.ReportAbilityUsed(this); // Comentado: tipos incompatibles
    }

    public virtual void Use(PlayableStatHolder user)
    {
        if (!CanUse(user)) return;
        if (!TryConsumeResource(user)) return;
        lastUseTime = Time.time;
        OnAbilityUsed(user);
    }
}
