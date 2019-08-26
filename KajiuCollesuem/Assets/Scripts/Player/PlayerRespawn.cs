﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private float deathLength = 3f;
    private static Vector3 currentRespawnPoint = new Vector3(0, 0, 0);

    private void Start()
    {
        if (currentRespawnPoint == Vector3.zero)
            currentRespawnPoint = transform.position;
        else
            transform.position = currentRespawnPoint;
    }

    public void Dead()
    {
        StartCoroutine("DeadRoutine");
    }

    private IEnumerator DeadRoutine()
    {
        yield return new WaitForSeconds(deathLength);

        //Temp Restart Scene (replace this with the proper scene manager and with a HUD element)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void setRespawinPoint(Vector3 pPoint)
    {
        currentRespawnPoint = pPoint;
    }
}