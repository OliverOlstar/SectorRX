using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnTriggerEnter (Collider other)
    {
        IAttributes otherAttributes = other.GetComponent<IAttributes>();

        if (otherAttributes != null)
        {
            otherAttributes.TakeDamage(damage, true);
        }
    }
}
