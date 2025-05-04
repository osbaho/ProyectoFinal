using UnityEngine;

public class ProjectileAbility : AbilityBase
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 20f;

    public ProjectileAbility(string name, Sprite icon, GameObject projectilePrefab = null, float projectileSpeed = 20f)
    {
        Name = name;
        Icon = icon;
        this.projectilePrefab = projectilePrefab;
        this.projectileSpeed = projectileSpeed;
    }

    public override bool CanUse(PlayableStatHolder user)
    {
        // Lógica de validación de recursos, cooldown, etc.
        return base.CanUse(user);
    }

    protected override void OnAbilityEffect(PlayableStatHolder user)
    {
        // Instanciar un proyectil y lanzarlo hacia adelante desde el usuario
        if (projectilePrefab != null && user != null)
        {
            var spawnPos = user.transform.position + user.transform.forward * 1.5f;
            var projectile = GameObject.Instantiate(projectilePrefab, spawnPos, user.transform.rotation);
            var rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = user.transform.forward * projectileSpeed;
            }
        }
    }
}
