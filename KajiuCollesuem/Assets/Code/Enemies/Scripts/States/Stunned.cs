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

    public void Setup(Transform pTarget, Animator pAnim, NavMeshAgent pAgent)
    {
        _target = pTarget;
        _anim = pAnim;
        _agent = pAgent;
    }

    public void UpdateTarget(Transform pTarget) => _target = pTarget;

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

    //Animation Events //////////////
    public void AEDoneStunned()
    {
        //Debug.Log("Stunned: AEDoneStunned");
        GetComponent<AlwaysSeek>().retribution = true;
        GetComponent<Decision>().ForceStateSwitch(GetComponent<Seek>());
        _enabled = false;
    }

    public void Pause()
    {
        throw new System.NotImplementedException();
    }

    public void Resume()
    {
        throw new System.NotImplementedException();
    }
}
