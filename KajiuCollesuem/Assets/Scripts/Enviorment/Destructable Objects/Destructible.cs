using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private DestructableObjectPool _pool;

    [Space]
    [SerializeField] private GameObject destroyedPrefab;
    [SerializeField] private LayerMask canTriggerDestroyLayers;

    private void OnCollisionEnter()
    {
        if (destroyedPrefab != null)
            Instantiate(destroyedPrefab, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == canTriggerDestroyLayers)
        {
            if (destroyedPrefab != null)
            {
                _pool.getObjectFromPool(destroyedPrefab, transform);
            }
            
            Destroy(this.gameObject);  
        }
    }
}
