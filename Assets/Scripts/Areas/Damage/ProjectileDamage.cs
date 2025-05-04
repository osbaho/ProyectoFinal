using UnityEngine;
using Interfaces;

/// <summary>
/// ProjectileDamage
/// Es un componente que se adjunta al prefab del proyectil.
/// Su función es aplicar daño cuando el proyectil colisiona con un objeto que implemente IHealth.
/// Se encarga de la lógica de colisión y destrucción del proyectil tras impactar.
/// </summary>
public class ProjectileDamage : MonoBehaviour
{
    private int damage;

    public void SetDamage(int dmg) => damage = dmg;

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IHealth>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
