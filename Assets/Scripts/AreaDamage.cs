using UnityEngine;
using Interfaces;

public class AreaDamage : MonoBehaviour
{
    private int damage;

    public void SetDamage(int dmg) => damage = dmg;

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IHealth>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
}
