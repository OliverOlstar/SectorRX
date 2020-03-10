using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMultHitbox : MonoBehaviour
{
    [SerializeField] private string tag = "Ability";

    [HideInInspector] public float attackMult = 1;
    
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _damageRate = 0.5f;
    [SerializeField] private float _knockForwardForce = 1.0f; 
    [SerializeField] private float _knockUpForce = 0.0f;

    [HideInInspector] public GameObject attacker;

    private PlayerAttributes _playerAttributes;
    private IAttributes _playerIAttributes;

    private List<Collider> collidersInTrigger = new List<Collider>();

    private void OnDisable()
    {
        collidersInTrigger = new List<Collider>();
        StopAllCoroutines();
    }

    private void Start()
    {
        _playerAttributes = GetComponentInParent<PlayerAttributes>();
        if (_playerAttributes != null)
            _playerIAttributes = _playerAttributes.GetComponent<IAttributes>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Check if collided with an Attributes Script
        IAttributes otherAttributes = other.GetComponent<IAttributes>();
        if (otherAttributes == null) 
            otherAttributes = other.GetComponentInParent<IAttributes>();

        if (otherAttributes != null && otherAttributes.IsDead() == false && otherAttributes != _playerIAttributes && !collidersInTrigger.Contains(other))
        {
            StartCoroutine(damageRoutine(otherAttributes, other.GetComponent<OnGroundComponent>(), other));
            collidersInTrigger.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (collidersInTrigger.Contains(other))
            collidersInTrigger.Remove(other);
    }

    IEnumerator damageRoutine(IAttributes pAttributes, OnGroundComponent pOtherOnGround, Collider pOther)
    {
        do
        {
            // Add Damage & Knockup
            pAttributes.TakeDamage(Mathf.FloorToInt(_damage * attackMult), _knockForwardForce * transform.up + Vector3.up * _knockUpForce, attacker, tag);

            // Reset Falling Force
            if (pOtherOnGround != null)
                pOtherOnGround.ResetFallingForce();

            yield return new WaitForSeconds(_damageRate);
        }
        // Check if still colliding
        while (collidersInTrigger.Contains(pOther) && pOther.gameObject.activeSelf && pAttributes.IsDead() == false);

        // If in Tar still but is dead
        if (collidersInTrigger.Contains(pOther))
            collidersInTrigger.Remove(pOther);
    }
}
