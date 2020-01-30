using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Programmer: Kavian Kermani
Additional Programmers: Oliver Loescher
Description: Spawns a random number of enemies, in preset locations randomly.
*/

public class SpawnRandomEnemies : MonoBehaviour
{
    public GameObject enemyToSpawn;

    [SerializeField] private List<Transform> _enemySpawnPoints = new List<Transform>();

    [SerializeField] private int enemySpawnCount = 12;
    
    private int _enemySpawnPointIndex;

    private void Awake()
    {
        foreach (Transform children in GetComponentInChildren<Transform>())
        {
            _enemySpawnPoints.Add(children);
        }
    }

    public void SpawnEnemies()
    {
        if (enemySpawnCount > _enemySpawnPoints.Count - 1)
            enemySpawnCount = _enemySpawnPoints.Count - 1;

        for (int i = 0; i < enemySpawnCount; i++)
        {
            _enemySpawnPointIndex = Random.Range(0, _enemySpawnPoints.Count);
            Instantiate(enemyToSpawn, _enemySpawnPoints[_enemySpawnPointIndex].position, Quaternion.identity);
            _enemySpawnPoints.Remove(_enemySpawnPoints[_enemySpawnPointIndex]);
        }
    }
}
