using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawner : MonoBehaviour
{
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    private Vector3 spawnScale;

    private GameObject myEnemy;

    void RespawnEnemy()
    {
        myEnemy.transform.position = spawnPosition;
        myEnemy.transform.rotation = spawnRotation;
        myEnemy.transform.localScale = spawnScale;
        myEnemy.SetActive(true);
    }

    public void Setup(GameObject pEnemy)
    {
        myEnemy = pEnemy;
        spawnPosition = myEnemy.transform.position;
        spawnRotation = myEnemy.transform.rotation;
        spawnScale = myEnemy.transform.localScale;
    }
}
