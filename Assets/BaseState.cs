using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Health health;

    private void Update()
    {
        if (health == null)
        {
            Debug.LogWarning("BaseState: Health reference is missing!");
            return;
        }

        if (gameManager == null)
        {
            Debug.LogWarning("BaseState: GameManager reference is missing!");
            return;
        }

        if (health.currentHealth <= 0)
        {
            Debug.Log("Health reached zero. Triggering GameOver...");
            gameManager.GameOver();
            health.Death();
        }
    }
}
