using UnityEngine;
using Interfaces;

/// <summary>
/// SoulMage: Portador jugable cuyas habilidades consumen vida.
/// </summary>
[RequireComponent(typeof(PlayableStatHolder))]
public class SoulMage : MonoBehaviour, IResourceUser
{
    private PlayableStatHolder holder => GetComponent<PlayableStatHolder>();

    public void UseResource(int amount)
    {
        holder.Health?.TakeDamage(amount);
    }

    public void RecoverResource(int amount)
    {
        holder.Health?.Heal(amount);
    }

    public int GetCurrentResource()
    {
        return holder.Health != null ? holder.Health.CurrentHealth : 0;
    }

    public int GetMaxResource()
    {
        return holder.Health != null ? holder.Health.MaxHealth : 0;
    }
}