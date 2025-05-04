using UnityEngine;
using Interfaces;
using System.Collections.Generic;

public class AreaDamage : MonoBehaviour
{
    [Tooltip("Daño infligido al entrar en el área")]
    public int damage = 10;
    [Tooltip("Intervalo en segundos para aplicar daño continuo")]
    public float damageInterval = 1f;

    // Almacena los objetos dentro del trigger y su tiempo acumulado
    private readonly Dictionary<IHealth, float> inside = new();

    public void SetDamage(int dmg) => damage = dmg;

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IHealth>();
        if (damageable != null && !inside.ContainsKey(damageable))
        {
            damageable.TakeDamage(damage); // Daño inmediato al entrar
            inside[damageable] = 0f; // Inicia el temporizador para daño continuo
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var damageable = other.GetComponent<IHealth>();
        if (damageable != null && inside.ContainsKey(damageable))
        {
            inside.Remove(damageable);
        }
    }

    private void Update()
    {
        // Copia para evitar modificación durante la iteración
        var keys = new List<IHealth>(inside.Keys);
        foreach (var health in keys)
        {
            inside[health] += Time.deltaTime;
            if (inside[health] >= damageInterval)
            {
                health.TakeDamage(damage);
                inside[health] = 0f;
            }
        }
    }
}
