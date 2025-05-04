using UnityEngine;
using Abilities.Habilities;
using Base;
using Components;
using Abilities;

/// <summary>
/// Portador jugable: puede tener habilidades, vida y/o maná por asociación.
/// </summary>

namespace Holders
{
    // El nombre de la clase debe coincidir con el nombre del archivo para evitar errores de compilación.
    // Asegúrate de que el archivo se llame PlayableStatHolder.cs y esté en la misma carpeta que este código.
    // Si cambias el nombre de la clase, también debes cambiar el nombre del archivo para que coincida con el nuevo

    public class PlayableStatHolder : BaseStatHolder
    {
        [Header("Stats")]
        public HealthComponent Health;
        public ManaComponent Mana;

        [Header("Abilities")]
        public SystemAbility AbilitySystem = new SystemAbility();

        public void Initialize(HealthComponent health = null, ManaComponent mana = null)
        {
            Health = health;
            Mana = mana;
        }

        public void AddAbility(AbilityBase ability)
        {
            AbilitySystem.AddAbility(ability);
        }

        public void RemoveAbility(AbilityBase ability)
        {
            AbilitySystem.RemoveAbility(ability);
        }

        public void UseAbility(int index)
        {
            AbilitySystem.SelectedAbilityIndex = index;
            AbilitySystem.UseSelectedAbility(this);
        }

        public void TakeDamage(int amount)
        {
            Health?.TakeDamage(amount);
        }

        public void InitializeAbilities(Sprite healIcon, Sprite areaIcon, Sprite projectileIcon, GameObject projectilePrefab)
        {
            var heal = new HealAbility("Curar", healIcon, resourceCost: 20);
            var area = new AreaDamageAbility("Explosión", areaIcon, radius: 5f, damage: 30, resourceCost: 30);
            var projectile = new ProjectileAbility("Disparo", projectileIcon, projectilePrefab, projectileSpeed: 20f, resourceCost: 10);

            AddAbility(heal);
            AddAbility(area);
            AddAbility(projectile);
        }
    }
}
