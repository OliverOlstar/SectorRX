using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttributes : MonoBehaviour
{
    public int startHealth = 100;
    public int currentHealth;
    public Slider healthSlider;

    AI enemyhealthUI;
    public GameObject enemyHealthUI;
    private GameObject player;

    public RectTransform healthBar;
    private Camera cam;

    public int playerDamage = 5;

    Animator anim;

    bool isDead;
    bool damaged;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = startHealth;

        enemyHealthUI.SetActive(false);
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //set rotation of health bar
        healthBar.rotation = cam.transform.rotation;
    }

    public void TakeDamage(int amount)
    {
        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        EnemyDead();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            TakeDamage(playerDamage);
        }
    }

    void EnemyDead()
    {
        Destroy(gameObject);
    }

    IEnumerator HealthVanish()
    {
        yield return new WaitForSeconds(0.0f);
        enemyHealthUI.SetActive(false);
    }
}
