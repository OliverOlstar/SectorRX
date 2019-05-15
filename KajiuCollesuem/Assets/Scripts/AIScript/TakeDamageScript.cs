using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageScript : MonoBehaviour {

    public int enemyHealth = 10;

    void deductPoints(int damageAmount)
    {
        enemyHealth -= damageAmount;
    }

    void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
