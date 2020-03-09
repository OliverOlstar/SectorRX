using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    [SerializeField] private int _damage = 40;
    [SerializeField] private int _knockForce = 40;
    [SerializeField] private int _knockupForce = 40;

    private List<IAttributes> alreadyHit = new List<IAttributes>();

    private void OnDisable()
    {
        alreadyHit = new List<IAttributes>();
    }

    private void OnTriggerEnter(Collider other)
    {
        IAttributes otherAttributes = other.GetComponent<PlayerAttributes>();
        if (otherAttributes == null)
            otherAttributes = other.GetComponentInParent<PlayerAttributes>();

        // Can't hit the same person twice
        if (alreadyHit.Contains(otherAttributes))
            return;

        //If collided with the player model, player takes damage
        if (otherAttributes != null)
        {
            otherAttributes.TakeDamage(_damage, transform.forward * _knockForce + Vector3.up * _knockupForce, this.gameObject);
            alreadyHit.Add(otherAttributes);
        }
    }
}
