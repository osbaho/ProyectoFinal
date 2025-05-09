using System;
using Interfaces;
using Base;

namespace Components
{
    public class HealthComponent : StatComponent
    {
        public override void AffectValue(int value)
        {
            // Puedes personalizar la lógica aquí si lo deseas
            base.AffectValue(value);
        }
        public void TakeDamage(int amount)
        {
            AffectValue(-amount);
        }
        public void Heal(int amount)
        {
            AffectValue(amount);
        }
        
    }
}
