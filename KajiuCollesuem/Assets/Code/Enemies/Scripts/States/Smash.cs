using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Smash : MonoBehaviour, IState
{
    private Animator _anim;
    private NavMeshAgent _agent;
    private Transform _target;

    [SerializeField] private float _fSmashRange = 11, _fSmashExitRange = 0, halfPlayerHeight;

    [SerializeField] private bool _enabled = false, _isTouchingGround = true;
    private int _iDecision = 1;

    public void Setup(Transform pTarget, Animator pAnim, NavMeshAgent pAgent)
    {
        _anim = pAnim;
        _agent = pAgent;
        _target = pTarget;
    }

    public void Enter()
    {
        _enabled = true;
        _agent.isStopped = true;
        _iDecision = 4;/*Random.Range(0, 100);*/
        _anim.SetFloat("Speed", 0);
    }

    public void Exit()
    {
        _enabled = false;
    }

    public bool CanEnter(float pDistance)
    {
        //If target is a gonner don't enter
        if (_target == null || _target.gameObject.activeSelf == false) return false;

        //Can shoot if cooldown is up and player is in range
        if ((pDistance <= _fSmashRange && pDistance >= _fSmashExitRange) || _iDecision % 4 == 0)
            return true;

        return false;
    }

    public bool CanExit(float pDistance)
    {
        return /*pDistance > _fSmashRange || pDistance < _fSmashExitRange ||*/ (_iDecision % 4 != 0 && _isTouchingGround);
    }

    public void Tick()
    {
        if (_iDecision % 4 == 0)
        {
            float distance = transform.position.z - _target.position.z;
            Debug.Log(distance);

            if (distance > 1)
            {
                transform.Translate(new Vector3(0, 1, 1));
            }

            else if (!_isTouchingGround)
            {
                transform.Translate(Vector3.down);
            }
        }

        _isTouchingGround = _isOnGround();
    }

    private bool _isOnGround()
    {
        float lengthToSearch = 0.8f;
        float colliderThreshold = 0.001f;

        Vector3 lineStart = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        Vector3 vectorToSearch = new Vector3(this.transform.position.x, lineStart.y - lengthToSearch, this.transform.position.z);

        Color color = new Color(0.0f, 0.0f, 1.0f);
        Debug.DrawLine(lineStart, vectorToSearch, color);

        RaycastHit hitInfo;
        if (Physics.Linecast(this.transform.position, vectorToSearch, out hitInfo))
        {
            if (hitInfo.distance < halfPlayerHeight)
            {
                return true;
            }
        }

        return false;
    }

    public void Pause()
    {
        throw new System.NotImplementedException();
    }

    public void Resume()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateTarget(Transform pTarget) => _target = pTarget;
}
