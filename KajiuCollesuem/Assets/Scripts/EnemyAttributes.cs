using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttributes : MonoBehaviour
{
    public float enemyHealth = 100f;

    public void takeDamage(float deductHealth)
    {
        enemyHealth -= deductHealth;

        if (enemyHealth <= 0) {
            EnemyDead();
        }
    }

    void EnemyDead()
    {
        Destroy(gameObject);
    }
}
