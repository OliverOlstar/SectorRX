/*
Programmer: Kavian Kermani
Additional Programmers: Other people who worked on the script
Description: Spawns a player in four preset locations randomly.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomPlayers : MonoBehaviour
{
    public GameObject[] playersToSpawn;
    public Transform[] playerSpawnPoints;

    public int playerSpawnCount = 1;

    public List<GameObject> spawnedPlayers;

    private int _playerIndex;
    private int _playerSpawnPointIndex;

    public void SpawnPlayers()
    {
        for (int i = 0; i < playerSpawnCount; i++)
        {
            _playerIndex = Random.Range(0, playersToSpawn.Length);
            _playerSpawnPointIndex = Random.Range(0, playerSpawnPoints.Length);
            GameObject gameObject = Instantiate(playersToSpawn[_playerIndex], playerSpawnPoints[_playerSpawnPointIndex].position, Quaternion.identity);
            spawnedPlayers.Add(gameObject);
        }
    }
}
