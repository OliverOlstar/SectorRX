using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerScript : MonoBehaviour
{
    public int health;
    public int maxHealth = 100;

    public int damage = 30;

    public float attackTimer;
    public float attackTimerMax = 4f;

    public bool playerInRange = false;

    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        attackTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) Destroy(this);
        if (playerInRange)
        {
            attackTimer += 0.01f;
            if (attackTimer >= attackTimerMax) Fire();
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
    }

    public void Fire()
    {
        Player.GetComponent<PlayerAttributes>().takeDamage(damage);
        attackTimer = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") playerInRange = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") playerInRange = false;
    }
}
