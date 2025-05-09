using UnityEngine;
using Components;
using System.Collections.Generic;

public class ManaArea : MonoBehaviour
{
    public int manaAmount = 20;
    public float interval = 2f;

    private readonly Dictionary<ManaComponent, float> inside = new();

    private void OnTriggerEnter(Collider other)
    {
        var mana = other.GetComponent<ManaComponent>();
        if (mana != null && !inside.ContainsKey(mana))
        {
            mana.RecoverResource(manaAmount); // Recupera al entrar
            inside[mana] = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var mana = other.GetComponent<ManaComponent>();
        if (mana != null && inside.ContainsKey(mana))
        {
            inside.Remove(mana);
        }
    }

    private void Update()
    {
        var keys = new List<ManaComponent>(inside.Keys);
        foreach (var mana in keys)
        {
            inside[mana] += Time.deltaTime;
            if (inside[mana] >= interval)
            {
                mana.RecoverResource(manaAmount);
                inside[mana] = 0f;
            }
        }
    }
}
