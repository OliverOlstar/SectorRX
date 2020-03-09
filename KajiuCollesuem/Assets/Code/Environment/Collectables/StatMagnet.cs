using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatMagnet : MonoBehaviour
{
    private Rigidbody _rb;

    [Header("Spawning")]
    [SerializeField] private float _spawnUpForce = 5.0f;
    [SerializeField] private float _spawnForce = 10.0f;

    [Header("Magnet")]
    [SerializeField] private float _magnetTriggerStartDelay = 0.1f;
    [SerializeField] private float _magnetInitialVelocity = 10;
    [SerializeField] private float _magnetInitialUpVelocity = 10;

    private List<Collider> collidersInMagnet = new List<Collider>();

    void Start()
    {
        _rb = GetComponentInParent<Rigidbody>();

        // Spawn Force
        _rb.AddForce(new Vector3((Random.value - 0.5f) * _spawnForce, _spawnUpForce, (Random.value - 0.5f) * _spawnForce), ForceMode.Impulse);

        // Magnet doesn't enable for a little bit
        StartCoroutine("EnableTriggerDelay");
    }

    IEnumerator EnableTriggerDelay()
    {
        Collider myTrigger = GetComponent<Collider>();
        myTrigger.enabled = false;

        yield return new WaitForSeconds(_magnetTriggerStartDelay);

        myTrigger.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        IAttributes otherAttributes = other.GetComponentInParent<IAttributes>();

        // Jump at player
        if (other.CompareTag("Player") && otherAttributes.IsDead() == false)
        {
            StartCoroutine(magnetRoutine(other, otherAttributes));
            collidersInMagnet.Add(other);
        }
    }
    IEnumerator magnetRoutine(Collider pOther, IAttributes pOtherAttributes)
    {
        do
        {
            // Get direction
            Vector3 jumpDir = pOther.transform.position - transform.position;
            float jumpUpMult = Mathf.Clamp(jumpDir.y * _magnetInitialUpVelocity, -3, 9);
            jumpDir = new Vector3(jumpDir.x, 0, jumpDir.z).normalized;

            // Set force
            _rb.velocity = jumpDir * _magnetInitialVelocity + Vector3.up * jumpUpMult;

            yield return new WaitForSeconds(1.6f);
        }
        // Check if still colliding
        while (collidersInMagnet.Contains(pOther) && pOtherAttributes.IsDead() == false);

        // If dead, remove
        if (collidersInMagnet.Contains(pOther))
            collidersInMagnet.Remove(pOther);
    }

    private void OnTriggerExit(Collider other)
    {
        if (collidersInMagnet.Contains(other))
            collidersInMagnet.Remove(other);
    }
}
