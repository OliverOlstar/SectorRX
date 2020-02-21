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

    [SerializeField] private float _halfPlayerHeight = 0.2f;
    
    private float _leaveStateTime = 0.0f;
    [SerializeField] private float _stunnedStateMinTime = 0.2f;

    [SerializeField] private bool _enabled = false;
    [SerializeField] private float _halfPlayerHeight = 0.52f;

    public void Setup(Transform pTarget, Animator pAnim, NavMeshAgent pAgent, EnemySmoothRotation pRotation)
    {
        _target = pTarget;
        _anim = pAnim;
        _agent = pAgent;
        _rb = GetComponent<Rigidbody>();
    }

    public void Enter()
    {
        _leaveStateTime = Time.time + _stunnedStateMinTime;

        _anim.SetTrigger("Hurt");
        _agent.enabled = false;
        _rb.isKinematic = false;
        _onGroundCheckTime = Time.time + 0.2f;
        _enabled = true;
    }

    public void Exit()
    {
        _agent.enabled = true;
        _rb.isKinematic = true;
        _agent.enabled = true;
        _enabled = false;
    }

    public bool CanEnter(float pDistance)
    {
        return false;
    }

    public bool CanExit(float pDistance)
    {
        return (Time.time > _leaveStateTime && IsOnGround());
    }

    public void Tick()
    {
        _rb.AddForce(Vector3.down * Time.deltaTime * 50);
    }

    public void UpdateTarget(Transform pTarget) => _target = pTarget;

    private bool IsOnGround()
    {
        // Linecast get two points
        Vector3 lineStart = transform.position;
        Vector3 vectorToSearch = new Vector3(lineStart.x, lineStart.y - _halfPlayerHeight, lineStart.z);

        // Debug Line
        Color color = new Color(0.0f, 0.0f, 1.0f);
        Debug.DrawLine(lineStart, vectorToSearch, color);

        // Linecast
        RaycastHit hitInfo;
        if (Physics.Linecast(this.transform.position, vectorToSearch, out hitInfo))
        {
            // On Ground
            return true;
        }

        // Off Ground
        return false;
    }
}
