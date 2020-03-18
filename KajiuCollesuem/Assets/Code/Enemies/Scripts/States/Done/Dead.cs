using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dead : MonoBehaviour, IState
{
    private Animator _anim;
    private NavMeshAgent _agent;
    private Transform _target;

    [SerializeField] private GameObject[] _itemPrefabs;
    [SerializeField] private int _cellSpawnCount = 5;

    [SerializeField] private bool _enabled = false;

    [SerializeField] private ParticleSystem _DeadParticle;

    public void Setup(Transform pTarget, Animator pAnim, NavMeshAgent pAgent, EnemySmoothRotation pRotation)
    {
        _anim = pAnim;
        _agent = pAgent;
        _target = pTarget;
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
    }

    public void Exit()
    {
        // It never exits
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
        int[] spawnIndex = new int[3];
        spawnIndex[0] = Random.Range(0, _itemPrefabs.Length);
        spawnIndex[1] = Random.Range(0, _itemPrefabs.Length);
        spawnIndex[2] = Random.Range(0, _itemPrefabs.Length);

        // Coins disperse
        for (int i = 0; i < _cellSpawnCount; ++i)
        {
            int randomIndex = spawnIndex[Random.Range(0, spawnIndex.Length)];
            GameObject tmp = Instantiate(_itemPrefabs[randomIndex]);
            tmp.transform.position = transform.position + Vector3.up * 0.65f;
        }

        _DeadParticle.Play();
        _DeadParticle.transform.parent = null;
        Destroy(_DeadParticle, 10.0f);

        Destroy(this.gameObject);
    }

    public void UpdateTarget(Transform pTarget) => _target = pTarget;
}
