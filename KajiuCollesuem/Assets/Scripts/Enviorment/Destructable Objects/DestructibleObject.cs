using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// OLIVER

/*
    Script for objects that can be destroyed.

    Watches for collision and ask DestructablePool for an destroyed prefab.
*/

public class DestructibleObject : MonoBehaviour
{
    private DestructableObjectPool _pool;

    [Space]
    [SerializeField] private GameObject destroyedPrefab;
    private int playerLayer;
    private int enemyLayer;

    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        enemyLayer = LayerMask.NameToLayer("Enemy");

        _pool = FindObjectOfType<DestructableObjectPool>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == playerLayer || other.gameObject.layer == enemyLayer)
        {
            if (destroyedPrefab != null)
            {
                _pool.getObjectFromPool(destroyedPrefab, transform);
            }

            Destroy(this.gameObject);
        }
    }
}
