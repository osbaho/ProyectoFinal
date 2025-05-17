using UnityEngine;
using Abilities.Habilities;
using Base;
using Components;
using Abilities;

namespace Holders
{
    /// <summary>
    /// Portador jugable: puede tener habilidades, vida y/o maná por asociación.
    /// </summary>
    public class PlayableStatHolder : BaseStatHolder
    {
        [SerializeField] private ManaComponent mana;
        [SerializeField] private GameObject explosionVFXPrefab;
        [SerializeField] private GameObject projectilePrefab;

        [System.NonSerialized]
        private readonly SystemAbility abilitySystem = new SystemAbility();

        public SystemAbility AbilitySystem => abilitySystem;
        public ManaComponent Mana
        {
            get
            {
                if (mana == null)
                    mana = GetComponentInChildren<ManaComponent>();
                return mana;
            }
        }

        public void Initialize(HealthComponent health = null, ManaComponent mana = null)
        {
            SetHealth(health);
            this.mana = mana;
        }

        protected void SetHealth(HealthComponent health)
        {
            typeof(BaseStatHolder)
                .GetField("health", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .SetValue(this, health);
        }

        public void AddAbility(AbilityBase ability)
        {
            if (ability != null)
                abilitySystem.AddAbility(ability);
        }

        public void RemoveAbility(AbilityBase ability)
        {
            if (ability != null)
                abilitySystem.RemoveAbility(ability);
        }

        public bool UseAbility(int index)
        {
            abilitySystem.SelectedAbilityIndex = index;
            var ability = abilitySystem.GetSelectedAbility();
            if (ability != null && ability.CanUse(this))
            {
                abilitySystem.UseSelectedAbility(this);
                return true;
            }
            return false;
        }

        public override void TakeDamage(int amount)
        {
            Health?.AffectValue(-amount);
        }

        public void InitializeAbilities(Sprite healIcon, Sprite areaIcon, Sprite projectileIcon)
        {
            var heal = new HealAbility("Curar", healIcon, 3, 15);
            var area = new AreaDamageAbility("Explosión", areaIcon, 10, 69, 20, 69, explosionVFXPrefab);
            var projectile = new ProjectileAbility("Hachazo", projectileIcon, 4, 5, projectilePrefab, 20, 20);

            AddAbility(heal);
            AddAbility(area);
            AddAbility(projectile);

            Debug.Log($"PlayableStatHolder: habilidades inicializadas, total: {abilitySystem.AllAbilities.Count}");
        }
    }
}
