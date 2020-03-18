using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private Vector3 _force;
    [SerializeField] private AudioSource _PadSource;
    [SerializeField] private ParticleSystem _Particle;

    private void OnTriggerEnter(Collider other)
    {
        // Add Force
        Rigidbody otherRB = other.GetComponent<Rigidbody>();

        if (otherRB != null && !other.CompareTag("Fireball"))
        {
            otherRB.AddForce(_force, ForceMode.Impulse);
            _PadSource.Play();

            _Particle.transform.position = other.ClosestPointOnBounds(transform.position);
            _Particle.Play();
        }

        // Reset Falling force
        OnGroundComponent otherOnGround = other.GetComponent<OnGroundComponent>();

        if (otherOnGround != null)
            otherOnGround.ResetFallingForce();
    }
}
