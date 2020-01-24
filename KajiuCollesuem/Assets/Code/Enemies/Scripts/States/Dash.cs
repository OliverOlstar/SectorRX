using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
*  Programmer: Mugiesshan Anandarajah
*  Description: Added dash script for hellhound to dash before going into attack state
* */
public class Dash : MonoBehaviour, IState
{
    private Animator _anim;
    private NavMeshAgent _agent;
    private Transform _target;
    
    private float _nextEnterTime = 0.0f;

    [SerializeField] private float _fMaxDashRange = 1, _fMinDashRange, _fForce = 0;

    [SerializeField] private bool _enabled = false;
    private float _fDistance = 0;

    public void Setup(Transform pTarget, Animator pAnim, NavMeshAgent pAgent)
    {
        _anim = pAnim;
        _agent = pAgent;
        _target = pTarget;
    }

    public void Enter()
    {
        //Debug.Log("Fireball: Enter");
        _enabled = true;
        //_agent.isStopped = true;
        float acceleration = _fForce;
        float finalVelocity = transform.position.z + (acceleration /** Time.deltaTime*/);
        _fDistance = (finalVelocity - transform.position.z) /** Time.time*/;
        transform.LookAt(_target.position);
    }

    public void Exit()
    {
        //Debug.Log("Fireball: Exit");
        _enabled = false;
    }

    public bool CanEnter(float pDistance)
    {
        //If target is a gonner don't enter
        if (_target == null || _target.gameObject.activeSelf == false) return false;

        //Can shoot if cooldown is up and player is in range
        if (Time.time >= _nextEnterTime && pDistance < _fMaxDashRange && pDistance > _fMinDashRange)
            return true;

        return false;
    }

    public bool CanExit(float pDistance)
    {
        //Debug.Log("Fireball: CanExit - " + (_enabled == false));
        return pDistance > _fMaxDashRange || pDistance < _fMinDashRange;
    }

    public void Tick()
    {
        if (transform.position.x < _fDistance || transform.position.x > _fDistance
            && Vector3.Distance(transform.position, _target.position) > 1.5f)
        {
            float Z = Mathf.Abs(transform.position.z), time = Time.deltaTime;
            Debug.Log(time);

            if (time > 0.016f && time < 0.017)
            {
                //transform.position = new Vector3(transform.position.x, transform.position.y, Z - (Time.deltaTime * _fForce));
                transform.Translate(new Vector3(0, 0, Z + (time * _fForce)));
            }
        }
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
