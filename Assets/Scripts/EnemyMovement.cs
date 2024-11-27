using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform castleTransform;

    public float attackRange = 10f;         // Range to start attacking
    public GameObject beamPrefab;          // Prefab for the beam attack
    public Transform beamOrigin;           // Position from which the beam is fired
    public float attackCooldown = 2f;      // Time between attacks
    public float damageAmount = 10f;       // Damage dealt per attack
    public float beamSpeed = 20f;          // Speed of the beam
    public float beamLifetime = 1f;        // Time before the beam is destroyed

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
            ShootBeam();
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

    void ShootBeam()
    {
        if (beamPrefab != null && beamOrigin != null && castleTransform != null)
        {
            // Calculate direction and distance
            Vector3 direction = (castleTransform.position - beamOrigin.position).normalized;
            float distance = Vector3.Distance(beamOrigin.position, castleTransform.position);

            // Instantiate the beam
            GameObject beam = Instantiate(beamPrefab, beamOrigin.position, Quaternion.identity);

            // Align the beam's rotation to face the castle
            beam.transform.forward = direction;

            // Adjust the beam's scale along the z-axis
            beam.transform.localScale = new Vector3(beam.transform.localScale.x,
                                                    beam.transform.localScale.y,
                                                    distance);

            // Position the beam at the midpoint between the origin and the castle
            beam.transform.position = beamOrigin.position + direction * (distance / 2f);

            // Deal damage to the castle
            Health castleHealth = castleTransform.GetComponent<Health>();
            if (castleHealth != null)
            {
                castleHealth.TakeDamage(damageAmount);
            }

            // Destroy the beam after a certain time
            Destroy(beam, beamLifetime);

            lastAttackTime = Time.time;
        }
    }

}
