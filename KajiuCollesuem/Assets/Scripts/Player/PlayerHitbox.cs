using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnTriggerEnter (Collider other)
    {
        EnemyAttributes enemyAttributes = other.GetComponent<EnemyAttributes>();

        if (enemyAttributes != null)
        {
            enemyAttributes.TakeDamage(damage);
        }
    }
}
