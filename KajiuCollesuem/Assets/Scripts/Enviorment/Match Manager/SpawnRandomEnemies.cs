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

    public List<Transform> enemySpawnPoints = new List<Transform>();

    public int enemySpawnCount = 12;
    
    private int _enemyIndex;
    private int _enemySpawnPointIndex;

    public void SpawnEnemies()
    {
        for (int i = 0; i < enemySpawnCount; i++)
        {
            enemySpawnCount = Random.Range(4, 7);
            _enemySpawnPointIndex = Random.Range(0, enemySpawnPoints.Count);
            Instantiate(enemyToSpawn, enemySpawnPoints[_enemySpawnPointIndex].position, Quaternion.identity);
            enemySpawnPoints.Remove(enemySpawnPoints[_enemySpawnPointIndex]);
        }
    }
}
