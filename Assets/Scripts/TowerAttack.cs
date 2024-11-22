using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    public float attackRange = 10f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1f; // Seconds between each shot
    private float fireCooldown = 0f;

    private Transform currentTarget;

    void Update()
    {
        // Reduce the cooldown timer
        if (fireCooldown > 0)
        {
            fireCooldown -= Time.deltaTime;
        }

        // Find the closest enemy
        Transform closestEnemy = GetClosestEnemy();

        // Update the current target
        if (closestEnemy != null && Vector3.Distance(transform.position, closestEnemy.position) <= attackRange)
        {
            currentTarget = closestEnemy;
        }
        else
        {
            currentTarget = null;
        }

        // If there's a valid target and the cooldown is finished, shoot
        if (currentTarget != null && fireCooldown <= 0)
        {
            Shoot();
            fireCooldown = fireRate; // Reset cooldown
        }
    }

    Transform GetClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue; // Skip if the enemy is null (destroyed)

            Transform enemyTransform = enemy.transform;
            float distanceSqr = (enemyTransform.position - currentPosition).sqrMagnitude;

            if (distanceSqr < closestDistanceSqr)
            {
                closestDistanceSqr = distanceSqr;
                bestTarget = enemyTransform;
            }
        }

        return bestTarget;
    }

    void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Projectile projectileScript = projectile.GetComponent<Projectile>();
            if (projectileScript != null && currentTarget != null)
            {
                projectileScript.SetTarget(currentTarget);
            }
        }
    }
}
