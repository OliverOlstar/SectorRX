using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Strafe : MonoBehaviour, IState
{
    private Transform _target;
    private Animator _anim;
    private NavMeshAgent _agent;
    //float decisionTimer = 0;
    float timer = 0, coolDownTime = 5;
    int strafeDecision = 0;
    Vector3 direction;
    [SerializeField] private int strafeMax = 5, strafeMin = 3;

    [SerializeField] private float _nextEnterTime = 0.0f;
    [SerializeField] private float _fStrafeSpeed;

    [SerializeField] private bool _enabled = false, _pause = false;

    public void Setup(Transform pTarget, Animator pAnim, NavMeshAgent pAgent, EnemySmoothRotation pRotation)
    {
        _target = pTarget;
        _anim = pAnim;
        _agent = pAgent;
    }

    public bool CanEnter(float pDistance)
    {
       if (Time.time >= _nextEnterTime && pDistance < strafeMax && pDistance > strafeMin)
            return true;

        return false;
    }

    public bool CanExit(float pDistance)
    {
        return pDistance > strafeMax || pDistance < strafeMin || Time.time < _nextEnterTime;
    }

    public void Enter()
    {
        _enabled = true;
        direction = GetStrafeDirection();
        timer = 0;

        if (gameObject.name.Contains("Alpha"))
            transform.Translate(Vector3.forward);
        _agent.Stop();
    }

    public void Exit()
    {
        _enabled = false;
        StopCoroutine("StrafeMovement");
    }

    public void Tick()
    {
        if (_enabled)
        {
            if (Vector3.Angle(transform.forward, _target.position - transform.position) > 1)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation(_target.position - transform.position),
                Time.deltaTime * 5);
            }

            else
                transform.LookAt(_target);

            if (timer > 5)
            {
                _nextEnterTime = Time.time + 5;

                /*if (distance > strafeMin - 2)
                {
                    transform.Translate(Vector3.forward);
                    //GetComponent<Rigidbody>().MovePosition(target.position);
                }

                else
                {
                    timer = 0;
                }*/
            }

            else
            {
                timer += Time.deltaTime;
                transform.Translate(direction);
            }
        }
    }

    public void UpdateTarget(Transform pTarget) => _target = pTarget;

    public Vector3 GetStrafeDirection()
    {
        strafeDecision = Random.Range(0, 2);
        return strafeDecision == 0 ? Vector3.left * _fStrafeSpeed : Vector3.right * _fStrafeSpeed;
    }

    public void Pause()
    {
        _pause = true;
    }

    public void Resume()
    {
        _pause = false;
    }

    /*IEnumerator StrafeMovement()
    {
        transform.LookAt(target);

        if (decisionTimer == 0)
        {
            strafeDecision = Random.Range(0, 2);
            Debug.Log(strafeDecision);
        }

        else
        {
            Vector3 direction = strafeDecision == 0 ? Vector3.left * .005f : Vector3.right * .005f;
            transform.Translate(direction);
        }
        decisionTimer += Time.deltaTime;
        yield return new WaitUntil(() => decisionTimer >= 5);
        decisionTimer = 0;
    }*/
}
