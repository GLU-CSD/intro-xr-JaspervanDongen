using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthbarFill;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float amount)
    {
        Debug.Log("TakeDamage called with amount: " + amount);
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        // Check if health is 0 or less and call Death function
        if (currentHealth <= 0)
        {
            //Death();
        }
    }

    public void RestoreHealth(float amount)
    {
        Debug.Log("RestoreHealth called with amount: " + amount);
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (healthbarFill != null)
        {
            healthbarFill.fillAmount = currentHealth / maxHealth;
            Debug.Log("Health Bar Updated: " + healthbarFill.fillAmount);
        }
    }

    // Death function to destroy the GameObject
    public void Death()
    {
        Debug.Log(gameObject.name + " has died!");
        Destroy(gameObject);
    }
}
