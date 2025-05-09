using System.Collections.Generic;
using UnityEngine;
using Components;

namespace Base
{
    public abstract class BaseStatHolder : MonoBehaviour
    {
        // Parámetros comunes para todos los holders
        [Header("Stats")]
        [SerializeField] protected HealthComponent health;

        // Permite acceso público de solo lectura a health
        public HealthComponent Health => health;

        // Método común para recibir daño
        public virtual void TakeDamage(int amount)
        {
            Health?.TakeDamage(amount);
        }
    }
}