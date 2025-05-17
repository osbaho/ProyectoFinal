using UnityEngine;
using Interfaces;

/// <summary>
/// ManaKnight: Portador jugable cuyas habilidades consumen maná.
/// </summary>
[RequireComponent(typeof(PlayableStatHolder))]
public class ManaKnight : MonoBehaviour, IResourceUser
{
    private PlayableStatHolder holder => GetComponent<PlayableStatHolder>();

    public void UseResource(int amount)
    {
        holder.Mana?.UseResource(amount);
    }

    public void RecoverResource(int amount)
    {
        holder.Mana?.RecoverResource(amount);
    }

    public int GetCurrentResource()
    {
        return holder.Mana != null ? holder.Mana.CurrentMana : 0;
    }

    public int GetMaxResource()
    {
        return holder.Mana != null ? holder.Mana.MaxMana : 0;
    }
}