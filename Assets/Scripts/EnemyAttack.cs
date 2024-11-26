using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damageAmount = 10f;       // Damage per attack
    public float attackCooldown = 2f;     // Time between attacks
    private float lastAttackTime;         // Tracks the last attack time

    private Health baseHealth;            // Reference to the Health script of the "Castle"

    private bool isInAttackRange = false; // Tracks if enemy is in range to attack

    // Optional: Reference to Animator for attack animations
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>(); // Attach an Animator component to the enemy
    }

    // Trigger when the enemy enters the "Castle" range
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Castle")) // Ensure the "Castle" has the correct tag
        {
            baseHealth = other.GetComponent<Health>();
            isInAttackRange = true;
        }
    }

    // Trigger when the enemy exits the "Castle" range
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Castle"))
        {
            baseHealth = null;
            isInAttackRange = false;
        }
    }

    void Update()
    {
        if (isInAttackRange && baseHealth != null && Time.time >= lastAttackTime + attackCooldown)
        {
            // Deal damage
            AttackBase();
        }
    }

    void AttackBase()
    {
        baseHealth.TakeDamage(damageAmount); // Deal damage to the "Castle"
        lastAttackTime = Time.time;         // Update last attack time

        // Optional: Play an attack animation
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        Debug.Log(this.name + " attacked the base!");
    }
}
