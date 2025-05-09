using UnityEngine;
using System.Collections.Generic;
using Components;

public class AreaDamage : MonoBehaviour
{
    [Tooltip("Daño infligido al entrar en el área")]
    public int damage = 10;
    [Tooltip("Intervalo en segundos para aplicar daño continuo")]
    public float damageInterval = 1f;

    // Almacena los objetos dentro del trigger y su tiempo acumulado
    private readonly Dictionary<HealthComponent, float> inside = new();

    public void SetDamage(int dmg) => damage = dmg;

    private void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<HealthComponent>();
        if (health != null && !inside.ContainsKey(health))
        {
            health.TakeDamage(damage); // Daño inmediato al entrar
            inside[health] = 0f; // Inicia el temporizador para daño continuo
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var health = other.GetComponent<HealthComponent>();
        if (health != null && inside.ContainsKey(health))
        {
            inside.Remove(health);
        }
    }

    private void Update()
    {
        // Copia para evitar modificación durante la iteración
        var keys = new List<HealthComponent>(inside.Keys);
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
