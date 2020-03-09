using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaHitbox : MonoBehaviour
{
    [SerializeField] private float _KnockupForce = 10.0f;
    [SerializeField] private int _Damage = 10;

    private List<Collider> collidersInTar = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        IAttributes otherAttributes = other.GetComponent<IAttributes>();

        if (otherAttributes != null && otherAttributes.IsDead() == false)
        {
            StartCoroutine(damageRoutine(otherAttributes, other.GetComponent<OnGroundComponent>(), other));
            collidersInTar.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (collidersInTar.Contains(other))
            collidersInTar.Remove(other);
    }

    IEnumerator damageRoutine(IAttributes attributes, OnGroundComponent otherOnGround, Collider other)
    {
        do
        {
            // Add Damage & Knockup
            attributes.TakeDamage(_Damage, Vector3.up * _KnockupForce, null, true);

            // Reset Falling Force
            if (otherOnGround != null)
                otherOnGround.ResetFallingForce();

            yield return new WaitForSeconds(1.5f);
        }
        // Check if still colliding
        while (collidersInTar.Contains(other) && attributes.IsDead() == false);

        // If in Tar still but is dead
        if (collidersInTar.Contains(other))
            collidersInTar.Remove(other);
    }
}
