using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stare : MonoBehaviour, IState
{
    private Animator _anim;
    private NavMeshAgent _agent;
    private EnemySmoothRotation _rotation;
    private Transform _target;

    [SerializeField] private float _stareRange = 11;

    [SerializeField] private bool _enabled = false;

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
        _agent.isStopped = true;
        _anim.SetFloat("Speed", 0);
        _rotation.enabled = true;
    }

    public void Exit()
    {
        _enabled = false;
        _rotation.enabled = false;
    }

    public bool CanEnter(float pDistance)
    {
        //Can shoot if cooldown is up and player is in range
        if (pDistance <= _stareRange)
            return true;

        return false;
    }

    public bool CanExit(float pDistance)
    {
        return true;
    }

    public void Tick()
    {
        
    }

    public void UpdateTarget(Transform pTarget) => _target = pTarget;
}
