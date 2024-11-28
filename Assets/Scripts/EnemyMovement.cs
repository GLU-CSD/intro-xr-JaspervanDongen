using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform castleTransform;

    public float attackRange = 15f;         // Range to start attacking
    public GameObject explosivePrefab;     // Prefab for the explosive projectile
    public Transform projectileOrigin;     // Position from which the projectile is fired
    public float attackCooldown = 2f;      // Time between attacks
    public float damageAmount = 10f;       // Damage dealt per attack
    public float projectileSpeed = 10f;    // Speed of the projectile
    public float explosionRadius = 5f;     // Radius of the explosion
    public float explosionForce = 500f;    // Force of the explosion
    public GameObject explosionEffect;     // Effect to play on impact

    private float lastAttackTime;
    private bool isAttacking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        GameObject castle = GameObject.FindGameObjectWithTag("Castle");
        if (castle != null)
        {
            castleTransform = castle.transform;
        }
    }

    void Update()
    {
        if (castleTransform == null)
        {
            FindCastle();
            return;
        }

        float distanceToCastle = Vector3.Distance(transform.position, castleTransform.position);

        if (distanceToCastle <= attackRange)
        {
            StartAttacking();
        }
        else
        {
            StopAttacking();
            agent.SetDestination(castleTransform.position);
        }
    }

    void FindCastle()
    {
        GameObject castle = GameObject.FindGameObjectWithTag("Castle");
        if (castle != null)
        {
            castleTransform = castle.transform;
        }
    }

    void StartAttacking()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            agent.isStopped = true;
        }

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            ShootExplosiveProjectile();
        }
    }

    void StopAttacking()
    {
        if (isAttacking)
        {
            isAttacking = false;
            agent.isStopped = false;
        }
    }

    void ShootExplosiveProjectile()
    {
        if (explosivePrefab != null && projectileOrigin != null && castleTransform != null)
        {
            // Instantiate the projectile
            GameObject explosive = Instantiate(explosivePrefab, projectileOrigin.position, Quaternion.identity);

            // Calculate the direction and set the projectile's velocity
            Vector3 direction = (castleTransform.position - projectileOrigin.position).normalized;
            Rigidbody rb = explosive.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = direction * projectileSpeed;
            }

            // Align the projectile to face the direction it's moving
            explosive.transform.rotation = Quaternion.LookRotation(direction);

            // Ignore collisions with the enemy's collider
            Collider enemyCollider = GetComponent<Collider>();
            Collider projectileCollider = explosive.GetComponent<Collider>();
            if (enemyCollider != null && projectileCollider != null)
            {
                Physics.IgnoreCollision(projectileCollider, enemyCollider);
            }

            // Set the projectile's target and explosion properties
            ExplosiveProjectile explosiveScript = explosive.GetComponent<ExplosiveProjectile>();
            if (explosiveScript != null)
            {
                explosiveScript.Initialize(castleTransform, damageAmount, explosionEffect);
            }

            // Record the last attack time
            lastAttackTime = Time.time;
        }
    }



}
