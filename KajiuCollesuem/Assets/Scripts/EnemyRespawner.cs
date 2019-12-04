using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRespawner : MonoBehaviour
{
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;

    void Start()
    {
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;

        //Add my function to event
        SaveAndLoad.RespawnEnemies += RespawnEnemy;
    }

    private void OnDestroy()
    {
        //Remove my function from event
        SaveAndLoad.RespawnEnemies -= RespawnEnemy;
    }

    void RespawnEnemy(SaveAndLoad pSaveAndLoad)
    {
        transform.position = spawnPosition;
        transform.rotation = spawnRotation;
        GetComponent<IAttributes>().Respawn();
        GetComponent<Decision>().Respawn();
        gameObject.SetActive(true);
    }
}
