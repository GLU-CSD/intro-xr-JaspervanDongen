using UnityEngine;

public class ExplosiveProjectile : MonoBehaviour
{
    private Transform targetCastle;
    private float damage;
    private GameObject explosionEffect;

    public void Initialize(Transform castle, float damageAmount, GameObject effect)
    {
        targetCastle = castle;
        damage = damageAmount;
        explosionEffect = effect;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision object is the target castle
        if (collision.transform == targetCastle)
        {
            // Play explosion effect at the impact point
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            // Apply damage to the castle
            Health castleHealth = targetCastle.GetComponent<Health>();
            if (castleHealth != null)
            {
                castleHealth.TakeDamage(damage);
            }
        }

        // Destroy the projectile regardless of what it hits
        Destroy(gameObject);
    }
}
