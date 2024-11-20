using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public Image healthbarFill;


    // Start is called before the first frame update
    void Start()
    {
       currentHealth = maxHealth;
    UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {}


public void TakeDamage(float amount)
{
    Debug.Log("TakeDamage called with amount: " + amount);
    currentHealth -= amount;
    currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
     Debug.Log("TakeDamage called, currentHealth: " + currentHealth);
    UpdateHealthBar();
}

public void RestoreHealth(float amount)
{
    Debug.Log("RestoreHealth called with amount: " + amount);
    currentHealth += amount;
    currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    Debug.Log("RestoreHealth called, currentHealth: " + currentHealth);
    UpdateHealthBar();
}



    void UpdateHealthBar()
{
    healthbarFill.fillAmount = currentHealth / maxHealth;
     Debug.Log("Health Bar Updated: " + healthbarFill.fillAmount);
}

}