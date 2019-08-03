using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    [SerializeField] private DestructableObjectPool _pool;

    [Space]
    [SerializeField] private GameObject destroyedPrefab;
    private int playerLayer;
    private int enemyLayer;

    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        enemyLayer = LayerMask.NameToLayer("Enemy");
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
