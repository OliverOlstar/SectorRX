using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dead : MonoBehaviour, IState
{
    private Animator _anim;
    private NavMeshAgent _agent;
    private Transform _target;
    //private Rigidbody _rb;

    [SerializeField] private GameObject[] _itemPrefabs;
    [SerializeField] private int _cellSpawnCount = 5;

    [SerializeField] private bool _enabled = false;

    public void Setup(Transform pTarget, Animator pAnim, NavMeshAgent pAgent, EnemySmoothRotation pRotation)
    {
        _anim = pAnim;
        _agent = pAgent;
        _target = pTarget;
        //_rb = GetComponent<Rigidbody>();
    }

    public void Enter()
    {
        _enabled = true;
        _anim.SetTrigger("Dead");
        _anim.SetBool("IsDead", true);
        _agent.enabled = false;
        
        foreach (Collider col in GetComponents<Collider>())
        {
            col.enabled = false;
        }

        //_rb.isKinematic = false;
        // TODO Remove collision during animation and after animation replace model with ragdoll version
    }

    public void Exit()
    {
        //It never exits
    }

    public bool CanEnter(float pDistance)
    {
        return false;
    }

    public bool CanExit(float pDistance)
    {
        return false;
    }

    public void Tick()
    {

    }

    public void AEDeadDone()
    {
        // Coins disperse
        for (int i = 0; i < _cellSpawnCount; ++i)
        {
            GameObject tmp = Instantiate(_itemPrefabs[Random.Range(0, _itemPrefabs.Length)]);
            tmp.transform.position = transform.position + Vector3.up * 0.35f;
        }
        Destroy(this.gameObject);
    }

    public void UpdateTarget(Transform pTarget) => _target = pTarget;
}
