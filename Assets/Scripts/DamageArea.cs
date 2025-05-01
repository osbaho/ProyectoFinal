using UnityEngine;
using Interfaces;

public class DamageArea : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IHealth>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
}
