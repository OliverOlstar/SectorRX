using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Seek : MonoBehaviour, IState
{
    private Animator _anim;
    private NavMeshAgent _agent;
    private Transform _target;

    [SerializeField] private float agroRange = 10;
    [SerializeField] private float agroLostRange = 10;

    private bool _enabled = false;

    public void Setup(Transform pTarget, Animator pAnim, NavMeshAgent pAgent)
    {
        _anim = pAnim;
        _agent = pAgent;
        _target = pTarget;
    }

    public void Enter()
    {
        _enabled = true;
        _agent.isStopped = false;
    }

    public void Exit()
    {
        _enabled = false;
    }

    public bool CanEnter(float pDistance)
    {
        //If target is a gonner don't enter
        if (_target == null || _target.gameObject.activeSelf == false) return false;

        //If in agro range
        if (pDistance <= (_enabled ? agroLostRange : agroRange))
            return true;
        
        return false;
    }

    public bool CanExit(float pDistance)
    {
        return true;
    }

    public void Tick()
    {
        //Vector3 direction = (target.position - transform.position).normalized;
        //direction.y = 0;

        //Quaternion targetRot = Quaternion.LookRotation(direction);
        //targetRot = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * rotation);
        //transform.rotation = targetRot;

        //Move to player
        _agent.SetDestination(_target.position);

        //Set Anim Speed
        _anim.SetFloat("Speed", _agent.velocity.magnitude / _agent.speed);
    }
}
