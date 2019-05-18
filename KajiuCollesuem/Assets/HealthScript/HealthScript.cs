using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public int startingHealth;
    public int currentHealth;
    public Slider healthSlider;
    private bool isDead;

    void Start()
    {
        currentHealth = startingHealth;
    }

    void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        healthSlider.value = currentHealth;
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
        }
    }
}
