using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private Vector3 _force;
    [SerializeField] private AudioSource _PadSource;

    private void OnTriggerEnter(Collider other)
    {
        // Add Force
        Rigidbody otherRB = other.GetComponent<Rigidbody>();

        if (otherRB != null)
        {
            otherRB.AddForce(_force, ForceMode.Impulse);
            _PadSource.Play();
        }

        // Reset Falling force
        OnGroundComponent otherOnGround = other.GetComponent<OnGroundComponent>();

        if (otherOnGround != null)
            otherOnGround.ResetFallingForce();
    }
}
