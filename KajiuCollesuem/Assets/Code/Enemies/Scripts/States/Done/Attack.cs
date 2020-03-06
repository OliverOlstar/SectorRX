using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : MonoBehaviour, IState
{
    private Animator _anim;
    private NavMeshAgent _agent;
    private Transform _target;
    private Rigidbody _rb;

    [SerializeField] private float _cooldown = 1.0f;
    private float _nextEnterTime = 0.0f;

    [SerializeField] private float _maxAttackRange = 1, _minAttackRange;

    [SerializeField] private GameObject _hitbox;

    [SerializeField] private float _jumpbackForce = 50;
    [SerializeField] private float _jumpbackUpForce = 10;

    [SerializeField] private float _halfPlayerHeight = 0.52f;
    [SerializeField] private float _onGroundCheckTime = 0;

    [SerializeField] [Range(0, 1)] private float _allowedDotValue = 0.6f;

    private int _subState = 0;

    [SerializeField] private bool _enabled = false;

    public void Setup(Transform pTarget, Animator pAnim, NavMeshAgent pAgent, EnemySmoothRotation pRotation)
    {
        _anim = pAnim;
        _agent = pAgent;
        _target = pTarget;
        _rb = GetComponent<Rigidbody>();
        _hitbox.SetActive(false);
    }

    public void Enter()
    {
        //Debug.Log("Fireball: Enter");
        _enabled = true;
        //transform.LookAt(_target.position);
        _anim.SetBool("Attacking", true);
        _agent.enabled = false;
    }

    public void Exit()
    {
        //Debug.Log("Fireball: Exit");
        _enabled = false;
        _nextEnterTime = Time.time + _cooldown;
        _anim.SetBool("Attacking", false);
        _rb.isKinematic = true;
        _agent.enabled = true;
        _hitbox.SetActive(false);
    }

    public bool CanEnter(float pDistance)
    {
        //Can shoot if cooldown is up and player is in range
        if (Time.time >= _nextEnterTime && pDistance < _maxAttackRange && pDistance > _minAttackRange)
        {
            if (Vector3.Dot(transform.forward, (_target.position - transform.position).normalized) >= _allowedDotValue)
            {
                return true;
            }
        }

        return false;
    }

    public bool CanExit(float pDistance)
    {
        //Debug.Log("Fireball: CanExit - " + (_enabled == false));
        return (_enabled == false);
    }

    public void Tick()
    {
        switch (_subState)
        {
            //Play Attack Anim
            case 0:
                break;

            //Jump Back Start
            case 2:
                _anim.SetBool("Attacking", false);
                _rb.isKinematic = false;
                _rb.AddForce(-transform.forward * _jumpbackForce + Vector3.up * _jumpbackUpForce, ForceMode.Impulse);
                _subState = 2;
                break;

            //Jump State
            case 1:
                if (_onGroundCheckTime <= Time.time && IsOnGround())
                {
                    _enabled = false;
                    _subState = 3;
                }
                break;
        }
    }

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

    public void UpdateTarget(Transform pTarget) => _target = pTarget;

    //Animation Events //////////////
    public void AEEnableHitbox()
    {
        if (_enabled)
            _hitbox.SetActive(true);
    }

    public void AEDisableHitbox()
    {
        if (_enabled)
            _hitbox.SetActive(false);
    }

    public void AEDoneAttack()
    {
        if (_enabled)
        {
            _onGroundCheckTime = Time.time + 0.2f;
            _subState = 1;
        }
    }
}
