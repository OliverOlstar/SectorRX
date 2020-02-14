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

    [SerializeField] private bool _enabled = false;

    public void Setup(Transform pTarget, Animator pAnim, NavMeshAgent pAgent, EnemySmoothRotation pRotation)
    {
        _target = pTarget;
        _anim = pAnim;
        _agent = pAgent;
    }

    public void Enter()
    {
        _enabled = true;
        _anim.SetTrigger("Hurt");
        _agent.isStopped = true;
    }

    public void Exit()
    {
        _enabled = false;
    }

    public bool CanEnter(float pDistance)
    {
        return false;
    }

    public bool CanExit(float pDistance)
    {
        return (_enabled == false);
    }

    public void Tick()
    {

    }

    public void UpdateTarget(Transform pTarget) => _target = pTarget;

    //Animation Events //////////////
    public void AEDoneStunned()
    {
        Debug.Log("Stunned: AEDoneStunned");
        _enabled = false;
    }
}
