using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform playerTransform;

    void Start()
    {
        // Vind de NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();

        // Zoek de XR Rig met de tag "Player"
        GameObject player = GameObject.FindGameObjectWithTag("Castle");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Update()
    {
        // Beweeg naar de positie van de speler als deze is gevonden
        if (playerTransform != null)
        {
            agent.SetDestination(playerTransform.position);
        }
    }
}
