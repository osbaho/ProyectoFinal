using System;
using Base;
using Enums;

namespace Components
{
    public class ManaComponent : StatComponent
    {
        private ManaCondition _condition = ManaCondition.ByTime;
        public ManaCondition Condition
        {
            get => _condition;
            set => _condition = value;
        }        

        private float _timeSinceLastIncrement = 0f;

        public override void AffectValue(int value)
        {
            switch (Condition)
            {
                case ManaCondition.None:
                    // No se aplica ningún efecto
                    break;
                case ManaCondition.ByTime:
                    // Incrementa el valor cada 2 segundos
                    if (_timeSinceLastIncrement >= 2f)
                    {
                        base.AffectValue(value);
                        _timeSinceLastIncrement = 0f;
                    }
                    break;
                case ManaCondition.Instant:
                    base.AffectValue(value);
                    break;
            }
        }

        public void RecoverResource(int amount)
        {
            AffectValue(amount);
        }

        public void UseResource(int amount)
        {
            AffectValue(-amount);
        }

        public void SetCondition(ManaCondition condition)
        {
            Condition = condition;
        }
    }
}
