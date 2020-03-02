using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour, IState
{
    private Animator _anim;
    private NavMeshAgent _agent;
    private Decision _decision;

    [SerializeField] private bool _enabled = false;

    public void Setup(Transform pTarget, Animator pAnim, NavMeshAgent pAgent, EnemySmoothRotation pRotation)
    {
        _anim = pAnim;
        _agent = pAgent;
        _decision = GetComponent<Decision>();
    }

    public void Enter()
    {
        _enabled = true;

        //Loses target
        _decision.target = null;

        _anim.SetFloat("Speed", 0);
        if (_agent != null)
            _agent.isStopped = true;
        //Debug.Log("Guard: Enter");
    }

    public void Exit()
    {
        //Debug.Log("Guard: Exit");
        _enabled = false;
    }

    public bool CanEnter(float pDistance)
    {
        //Debug.Log("Guard: CanEnter");
        //False if no patrol points

        return true;
    }

    public bool CanExit(float pDistance)
    {
        return true;
    }

    // Update is called once per frame
    public void Tick()
    {

    }

    public bool IsEnabled() { return _enabled; }

    public void UpdateTarget(Transform pTarget) { }
}
