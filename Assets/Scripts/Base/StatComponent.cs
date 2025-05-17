using System;
using UnityEngine;

namespace Base
{
    [Serializable]
    public abstract class StatComponent
    {
        private event Action<StatComponent> _onValueChanged;
        [SerializeField] private int maxValue = 100;
        [SerializeField, HideInInspector] private int currentValue;

        public int MaxValue => maxValue;
        public int CurrentValue => currentValue;

        // Exponer el evento solo para suscripción, no para asignación externa
        public event Action<StatComponent> OnValueChanged
        {
            add { _onValueChanged += value; }
            remove { _onValueChanged -= value; }
        }

        public virtual void Awake()
        {
            // Si currentValue es 0 al iniciar, iguala a maxValue
            if (currentValue <= 0)
                currentValue = maxValue;
        }

        public virtual void AffectValue(int value)
        {
            currentValue = Mathf.Clamp(currentValue + value, 0, maxValue);
            _onValueChanged?.Invoke(this);
        }
    }
}