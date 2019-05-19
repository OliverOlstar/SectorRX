using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public int startingHealth;
    public int currentHealth;
    public Slider healthSlider;

    void Start()
    {
        currentHealth = startingHealth;
    }

    void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;
        healthSlider.value = currentHealth;
    }
}
