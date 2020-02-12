using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * Programmer: Mugiesshan Anandarajah
 * Description: Edited this script to accomplist this task:
 *      Task: Grunts rotation is smooth
 * */
public class AlwaysSeek : MonoBehaviour, IState
{
    private Animator _anim;
    private NavMeshAgent _agent;
    private EnemySmoothRotation _rotation;
    protected Transform _target;

    [SerializeField] private float _minRange = 2;

    [SerializeField] protected bool _enabled = false;

    public void Setup(Transform pTarget, Animator pAnim, NavMeshAgent pAgent, EnemySmoothRotation pRotation)
    {
        _anim = pAnim;
        _agent = pAgent;
        _target = pTarget;
        _rotation = pRotation;
    }

    public void Enter()
    {
        _enabled = true;
        _agent.isStopped = false;
        _rotation.enabled = true;
    }

    public void Exit()
    {
        _enabled = false;
        _rotation.enabled = false;
    }

    public virtual bool CanEnter(float pDistance)
    {
        return true;
    }

    public bool CanExit(float pDistance)
    {
        return true;
    }

    public void Tick()
    {
        // Move to player
        _agent.SetDestination(_target.position);

        //Set Anim Speed
        _anim.SetFloat("Speed", _agent.velocity.magnitude / _agent.speed);

        // Doesn't just infinitly run into player
        if (Vector3.Distance(transform.position, _target.position) <= _minRange)
            _agent.isStopped = true;
        else
            _agent.isStopped = false;
    }

    public void UpdateTarget(Transform pTarget) => _target = pTarget;
}