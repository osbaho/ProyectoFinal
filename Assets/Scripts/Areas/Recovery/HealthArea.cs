using UnityEngine;
using Components;
using System.Collections.Generic;

public class HealthArea : MonoBehaviour
{
    public int healthAmount = 20;
    public float interval = 2f;

    private readonly Dictionary<HealthComponent, float> inside = new();

    private void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<HealthComponent>();
        if (health != null && !inside.ContainsKey(health))
        {
            health.Heal(healthAmount); // Cura al entrar
            inside[health] = 0f;
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
        var keys = new List<HealthComponent>(inside.Keys);
        foreach (var health in keys)
        {
            inside[health] += Time.deltaTime;
            if (inside[health] >= interval)
            {
                health.Heal(healthAmount);
                inside[health] = 0f;
            }
        }
    }
}
