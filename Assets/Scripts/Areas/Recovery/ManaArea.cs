using UnityEngine;
using Holders;
using Components;
using System.Collections;
using System.Collections.Generic;

public class ManaArea : MonoBehaviour
{
    public int manaAmount = 20;
    public float interval = 1f;

    private Dictionary<ManaComponent, Coroutine> manaCoroutines = new();

    private void OnTriggerEnter(Collider other)
    {
        var statHolder = other.GetComponent<PlayableStatHolder>();
        var mana = statHolder != null ? statHolder.Mana : null;
        if (mana != null && !manaCoroutines.ContainsKey(mana))
        {
            Coroutine coroutine = StartCoroutine(ManaCoroutine(mana));
            manaCoroutines.Add(mana, coroutine);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var statHolder = other.GetComponent<PlayableStatHolder>();
        var mana = statHolder != null ? statHolder.Mana : null;
        if (mana != null && manaCoroutines.TryGetValue(mana, out Coroutine coroutine))
        {
            StopCoroutine(coroutine);
            manaCoroutines.Remove(mana);
        }
    }

    private IEnumerator ManaCoroutine(ManaComponent mana)
    {
        while (true)
        {
            mana.AffectValue(manaAmount, Enums.ManaCondition.Instant);
            Debug.Log($"Recupera {manaAmount} de maná en el área de recuperación.");
            yield return new WaitForSeconds(interval);
        }
    }
}
