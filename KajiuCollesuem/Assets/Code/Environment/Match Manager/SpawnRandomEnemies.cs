/*
Programmer: Kavian Kermani
Additional Programmers: Other people who worked on the script
Description: Spawns a random number of enemies, in preset locations randomly.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomEnemies : MonoBehaviour
{
    public GameObject enemyToSpawn;

    private List<Transform> _enemySpawnPoints = new List<Transform>();

    public int enemySpawnCount = 12;
    
    private int _enemyIndex;
    private int _enemySpawnPointIndex;

    private void Start()
    {
        foreach (Transform children in GetComponentInChildren<Transform>())
        {
            _enemySpawnPoints.Add(children);
        }
    }

    public void SpawnEnemies()
    {
        enemySpawnCount = Random.Range(4, 7);

        for (int i = 0; i < enemySpawnCount; i++)
        {
            _enemySpawnPointIndex = Random.Range(0, _enemySpawnPoints.Count);
            Instantiate(enemyToSpawn, _enemySpawnPoints[_enemySpawnPointIndex].position, Quaternion.identity);
            _enemySpawnPoints.Remove(_enemySpawnPoints[_enemySpawnPointIndex]);
        }
    }
}
