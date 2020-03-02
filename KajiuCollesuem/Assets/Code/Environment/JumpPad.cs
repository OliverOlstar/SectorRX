using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private Vector3 _force;
    
    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Player"))
        //{
            // Add Force
            Rigidbody otherRB = other.GetComponent<Rigidbody>();

            if (otherRB != null)
                otherRB.AddForce(_force, ForceMode.Impulse);

            // Reset Falling force
            OnGroundComponent otherOnGround = other.GetComponent<OnGroundComponent>();

            if (otherOnGround != null)
                otherOnGround.ResetFallingForce();
        //}
    }
}
