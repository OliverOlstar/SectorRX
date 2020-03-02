using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] private int _damage = 40;
    [SerializeField] private int _knockForce = 40;
    [SerializeField] private int _knockupForce = 40;

    private void OnTriggerEnter(Collider other)
    {
        IAttributes otherAttributes = other.gameObject.GetComponent<PlayerAttributes>();

        //If collided with the player model, player takes damage
        if (otherAttributes != null)
        {
            otherAttributes.TakeDamage(_damage, transform.forward * _knockForce + Vector3.up * _knockupForce, this.gameObject);
        }
    }
}
