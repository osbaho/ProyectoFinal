using System;
using System.Threading.Tasks;
using Base;
using Enums;
using UnityEngine;

namespace Components
{
    [Serializable]
    public class ManaComponent : StatComponent
    {
        // Reflection solo debe usarse para inicialización avanzada/documentada
        private ManaCondition _condition = ManaCondition.ByTime;
        public ManaCondition Condition { get; private set; }

        public void SetCondition(ManaCondition condition)
        {
            Condition = condition;
        }

        private uint flowSpeed = 1;

        public override void AffectValue(int value)
        {
            ExecuteAffectCondition(value, _condition);
        }

        public void AffectValue(int value, ManaCondition customCondition = ManaCondition.None)
        {
            if (customCondition == ManaCondition.None)
            {
                ExecuteAffectCondition(value, _condition);
                return;
            }
            ExecuteAffectCondition(value, customCondition);
        }

        private void ExecuteAffectCondition(int value, ManaCondition condition)
        {
            switch (condition)
            {
                case ManaCondition.None:
                    // No se aplica ningún efecto
                    break;
                case ManaCondition.ByTime:
                    ManaCycle(value);
                    break;
                case ManaCondition.Instant:
                    base.AffectValue(value); // Esto dispara el evento OnValueChanged
                    break;
            }
        }

        private async void ManaCycle(int value)
        {
            int flowAmount = Mathf.Abs(value);
            int sign = Math.Sign(value);

            while (flowAmount > 0)
            {
                await Task.Delay(1000); // Espera 1 segundo
                flowAmount -= (int)flowSpeed; // Disminuye el flujo de mana
                base.AffectValue((int)(flowSpeed * sign)); // Aumenta el mana en 1
            }
        }
    }
}
