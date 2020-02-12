using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JumpBack : MonoBehaviour, IState
{
    private Animator _anim;
    private NavMeshAgent _agent;
    private Transform _target;

    [SerializeField] private float _cooldown = 1.0f, y, z;
    private float _nextEnterTime = 0.0f, _jumpTime = 0;
    public float jumpSpeed = 0, speed = 0, halfPlayerHeight;

    [SerializeField] private float _jumpBackRange = 1;
    private Rigidbody _rb;

    [SerializeField] private bool _enabled = false, _isTouchingGround = false;

    public void Setup(Transform pTarget, Animator pAnim, NavMeshAgent pAgent, EnemySmoothRotation pRotation)
    {
        _anim = pAnim;
        _agent = pAgent;
        _target = pTarget;
        _rb = GetComponent<Rigidbody>();
    }

    public void Enter()
    {
        //Debug.Log("Jump back: Enter");
        _enabled = true;
        //_agent.isStopped = true;
        //originalPosition = transform.position;
        transform.LookAt(_target.position);
        y = transform.position.y;
        z = transform.position.z;
        //transform.Translate(Vector3.back / 2);
        _agent.enabled = false;
        //rb.AddForce(-transform.forward * 20 + Vector3.up * 10);
    }

    public void Exit()
    {
        //Debug.Log("Jump back: Exit");
        _agent.enabled = true;
        _enabled = false;
    }

    public bool CanEnter(float pDistance)
    {
        //If target is a gonner don't enter
        if (_target == null || _target.gameObject.activeSelf == false) return false;

        //Can shoot if cooldown is up and player is in range
        if (Time.time >= _nextEnterTime && pDistance < _jumpBackRange)
            return true;

        return false;
    }

    public bool CanExit(float pDistance)
    {
        //Debug.Log("Jump back: CanExit - " + (_enabled == false));
        return pDistance > _jumpBackRange + 0.3f;
    }

    public void Tick()
    {
       
    }

    public void UpdateTarget(Transform pTarget) => _target = pTarget;

    public void FixedUpdate()
    {
        if (_enabled)
        {
            //Alternate jump solution
            y += Time.time;
            z += Time.time;
            float time = Time.time * speed;

            /*if (transform.position.y > 1)
                rb.AddForce(new Vector3(0, -y * time, z * time));
            else
                rb.AddForce(new Vector3(0, y * time, z * time));*/

            //This clamps the jump within a specific radius to ensure jump back isn't too long
            if (time > 150)
                time = 150;

            Debug.Log(time);

            /*transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.forward, 
                Time.deltaTime * 5);*/

            //Calculate end jump position
            /*Vector3 move = Vector3.right;
            move += Vector3.back;
            move.y = 0;
            Vector3 targetPosition = rb.position + move;
            //_isTouchingGround = _isOnGround();

            float newYposition = targetPosition.y;
            _jumpTime += Time.fixedDeltaTime;
            newYposition = _originalPosition + (jumpSpeed * _jumpTime) + (0.5f * -9.8f * _jumpTime * _jumpTime);
            targetPosition.y = newYposition;
            Debug.Log(targetPosition);

            rb.MovePosition(targetPosition);
            //transform.Translate(targetPosition);
            _isTouchingGround = _isOnGround();*/
        }
    }

    public void AEJumpBack()
    {

    }
}
