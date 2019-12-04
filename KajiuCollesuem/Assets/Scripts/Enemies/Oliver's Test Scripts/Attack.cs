using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : MonoBehaviour, IState
{
    [SerializeField] private float attackCooldownMax = 3;
    [SerializeField] private float attackCooldownMin = 1;
    private float nextAttackTime = 0;

    [SerializeField] private bool _colliding = false;

    private bool _enabled = false;

    public void Setup(Transform pTarget, Animator pAnim, NavMeshAgent pAgent)
    {
    }

    public void Enter()
    {
        _enabled = true;
        StartCoroutine("attackRoutine");
    }

    public void Exit()
    {

    }

    public bool CanEnter(float pDistance)
    {
        //Let This script control when it leaves
        if (_enabled) 
            return true;

        //Enter if colliding and done cooldown
        if (_colliding && Time.time >= nextAttackTime)
            return true;

        return false;
    }

    public bool CanExit(float pDistance)
    {
        return CanEnter(pDistance);
    }

    public void Tick()
    {

    }

    IEnumerator attackRoutine()
    {
        nextAttackTime = Time.time + Random.Range(attackCooldownMin, attackCooldownMax);
        yield return new WaitForSeconds(1);
        _enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _colliding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _colliding = false;
        }
    }
}
