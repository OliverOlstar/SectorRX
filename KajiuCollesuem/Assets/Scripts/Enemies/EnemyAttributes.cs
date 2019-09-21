using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttributes : MonoBehaviour, IAttributes
{
    public int startHealth = 100;
    public int currentHealth;
    
    private Slider healthSlider;
    public GameObject enemyHealthBar;

    private Camera _camera;
    private Animator _anim;

    private bool isDead;

    // Use this for initialization
    void Start()
    {
        _anim = GetComponent<Animator>();
        currentHealth = startHealth;

        healthSlider = enemyHealthBar.GetComponentInChildren<Slider>();

        //if (enemyHealthBar)
        //    enemyHealthBar.SetActive(false);

        _camera = Camera.main;
    }

    public void TakeDamage(int pAmount)
    {
        currentHealth -= pAmount;

        //if (healthSlider)
            healthSlider.value = (float)currentHealth/startHealth;

        if (currentHealth <= 0 && !isDead)
            Death();
    }

    void Death()
    {
        isDead = true;
        Destroy(gameObject);
    }
}
