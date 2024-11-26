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
        if (playerTransform == null)
        {
            FindCastle();
            return;
        }

        agent.SetDestination(playerTransform.position);
    }

    void FindCastle()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Castle");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

}
