using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrikeHitbox : MonoBehaviour
{
    private float _AttackMult = 1;
    
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _damageRate = 0.5f;
    [SerializeField] private float _knockForwardForce = 1.0f; 
    [SerializeField] private float _knockUpForce = 0.0f;

    private GameObject _Attacker;

    private IAttributes _playerIAttributes;

    private List<Collider> collidersInTrigger = new List<Collider>();

    private void OnDisable()
    {
        collidersInTrigger = new List<Collider>();
        StopAllCoroutines();
    }

    public void Init(IAttributes pPlayerAttributes, GameObject pAttacker, float pAttackMult, float pLifeTime)
    {
        _playerIAttributes = pPlayerAttributes;
        _Attacker = pAttacker;
        _AttackMult = pAttackMult;

        StartCoroutine(DestroyDelay(pLifeTime));
    }

    IEnumerator DestroyDelay(float pDelay)
    {
        yield return new WaitForSeconds(pDelay);
        Destroy(gameObject);
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
            pAttributes.TakeDamage(Mathf.FloorToInt(_damage * _AttackMult), _knockForwardForce * transform.up + Vector3.up * _knockUpForce, _Attacker);

            // Reset Falling Force
            if (pOtherOnGround != null)
                pOtherOnGround.ResetFallingForce();

            yield return new WaitForSeconds(_damageRate);
        }
        // Check if still colliding
        while (collidersInTrigger.Contains(pOther) && pAttributes.IsDead() == false);

        // If in still but is dead
        if (collidersInTrigger.Contains(pOther))
            collidersInTrigger.Remove(pOther);
    }
}
