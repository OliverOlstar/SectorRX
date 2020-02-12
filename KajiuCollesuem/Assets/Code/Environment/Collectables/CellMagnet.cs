using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CellMagnet : MonoBehaviour
{
    private Rigidbody _rb;

    [Header("Spawning")]
    [SerializeField] private float _spawnUpForce = 5.0f;
    [SerializeField] private float _spawnForce = 10.0f;

    [Header("Magnet")]
    [SerializeField] private float _suckInitialVelocity = 10;
    [SerializeField] private float _suckInitialUpVelocity = 10;

    void Start()
    {
        _rb = GetComponentInParent<Rigidbody>();

        // Spawn Force
        _rb.AddForce(new Vector3((Random.value - 0.5f) * _spawnForce, _spawnUpForce, (Random.value - 0.5f) * _spawnForce), ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Jump at player
        if (other.CompareTag("Player"))
        {
            // Get direction
            Vector3 jumpDir = other.transform.position - transform.position;
            jumpDir = new Vector3(jumpDir.x, 0, jumpDir.z).normalized;

            // Set force
            _rb.velocity = jumpDir * _suckInitialVelocity + Vector3.up * _suckInitialUpVelocity;
        }
    }
}
