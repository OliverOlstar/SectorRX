/*
Programmer: Kavian Kermani
Additional Programmers: Scott Watman
Description: Match manager: Spawns players and enemies, spawns and manages lava, ends match on end conditions.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour
{
    public Camera cinemaCam;
    public PlayerSpawn spawnPlayerScript;
    public SplitscreenManager splitscreenScript;
    public SpawnRandomEnemies[] spawnEnemyScript;
    public SpawnEditLava spawnLavaScript;

    public void Start()
    {
        foreach (SpawnRandomEnemies cluster in spawnEnemyScript)
        {
            cluster.SpawnEnemies();
        }

        StartCoroutine("CinemaOff");
        spawnPlayerScript.MatchStartup();
    }

    public void Update()
    {
        spawnLavaScript.lavaTimer();
        spawnPlayerScript.MatchEnd();
    }

    IEnumerator CinemaOff()
    {
        //Wait for three seconds then turn off camera (simulates cinematic camera).
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(3.0f);
        cinemaCam.gameObject.SetActive(false);

        spawnPlayerScript.SpawnAllPlayers();
    }
}
    