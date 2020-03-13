using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Programmer: Mugiesshan Anandarajah
 * Description: Edited this script to allow enemy to immediately pursue player after stun animation is done playing
 * */
public class Stunned : MonoBehaviour, IState
{
    private Transform _target;
    private Animator _anim;
    private NavMeshAgent _agent;
    private Rigidbody _rb;
    
    private float _leaveStateTime = 0.0f;
    [SerializeField] private float _stunnedStateMinTime = 0.2f;

    [SerializeField] private bool _enabled = false;

    public void Setup(Transform pTarget, Animator pAnim, NavMeshAgent pAgent, EnemySmoothRotation pRotation)
    {
        _target = pTarget;
        _anim = pAnim;
        _agent = pAgent;
        //_rb = GetComponent<Rigidbody>();
    }

    public void Enter()
    {
        _leaveStateTime = Time.time + _stunnedStateMinTime;

        _anim.SetTrigger("Hurt");
        _anim.SetFloat("Speed", 0);
        _agent.isStopped = true;
        //_rb.isKinematic = false;
        _enabled = true;
    }

    public void Exit()
    {
        //_rb.isKinematic = true;
        _agent.isStopped = false;
        _enabled = false;
    }

    public bool CanEnter(float pDistance)
    {
        return false;
    }

    public bool CanExit(float pDistance)
    {
        return (Time.time > _leaveStateTime);
    }

    public void Tick()
    {

    }

    public void UpdateTarget(Transform pTarget) => _target = pTarget;
}
