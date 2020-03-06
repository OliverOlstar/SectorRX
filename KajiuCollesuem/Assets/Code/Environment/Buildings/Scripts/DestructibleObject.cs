using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// OLIVER

/*
    Script for objects that can be destroyed.

    Watches for collision and ask DestructablePool for an destroyed prefab.
*/

public class DestructibleObject : MonoBehaviour, IAttributes
{
    private ObjectShaker _Shaker;

    [SerializeField] private int _Health = 3;
    [SerializeField] private float _FallSpeed = 10;

    [Space]
    [SerializeField] private GameObject[] _itemPrefabs;
    [SerializeField] private int _collectablesSpawnCount = 5;
    [SerializeField] private float _collectableUpOffset = 0.3f;

    private void Start()
    {
        _Shaker = GetComponent<ObjectShaker>();
    }

    public bool TakeDamage(int pDamage, Vector3 pKnockback, GameObject pAttacker, bool pIgnoreWeight = false)
    {
        _Health--;
        _Shaker.AddShake(0.1f, 0.2f, 0.3f);

        if (_Health <= 0)
        {
            StartCoroutine("dropDelay");
            StartCoroutine("fallRoutine");
            _Health = 99999;
        }

        return false;
    }

    public bool IsDead()
    {
        return false;
    }

    private IEnumerator fallRoutine()
    {
        _Shaker.AddShake(0.1f, 0.3f, 5.0f);

        while (transform.position.y > -12)
        {
            transform.Translate(transform.up * _FallSpeed * Time.deltaTime);
            yield return null;
        }

        Destroy(this.gameObject);
    }

    private IEnumerator dropDelay()
    {
        yield return new WaitForSeconds(0.3f);
        DropCollectables();
    }

    private void DropCollectables()
    {
        // Coins disperse
        for (int i = 0; i < _collectablesSpawnCount; ++i)
        {
            GameObject tmp = Instantiate(_itemPrefabs[Random.Range(0, _itemPrefabs.Length)]);
            tmp.transform.position = transform.position + Vector3.up * _collectableUpOffset;
        }
    }
}
