using System;
using UnityEngine;

namespace Base
{
    [Serializable]
    public abstract class StatComponent
    {
        public event Action<StatComponent> OnValueChanged;
        [SerializeField] private int maxValue = 100;
        private int currentValue;

        public int MaxValue => maxValue;
        public int CurrentValue => currentValue; 

        public virtual void AffectValue(int value)
        {
            currentValue = Mathf.Clamp(currentValue + value, 0, maxValue);
            OnValueChanged?.Invoke(this);
        }
    }
}