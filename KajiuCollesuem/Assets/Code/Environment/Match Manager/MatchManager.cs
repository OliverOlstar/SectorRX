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
    public static MatchManager instance = null;
    public PauseMenu playerPauseScript;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        StartCoroutine("CinemaOff");
        spawnPlayerScript.MatchStartup();

        foreach (SpawnRandomEnemies cluster in spawnEnemyScript)
        {
            cluster.SpawnEnemies();
        }
    }

    public void Update()
    {
        spawnLavaScript.lavaTimer();
    }

    public bool ManagerEnd()
    {
        return spawnPlayerScript.MatchEnd();
    }

    IEnumerator CinemaOff()
    {
        //Wait for three seconds then turn off camera (simulates cinematic camera).
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(3.0f);
        cinemaCam.gameObject.SetActive(false);

        spawnPlayerScript.SpawnAllPlayers();
        splitscreenScript.SetSplitScreen(this);
        PauseStatus();
    }

    private void PauseStatus()
    {
        StatPause[] temp = new StatPause[spawnPlayerScript.players.Count];
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] = spawnPlayerScript.players[i].GetComponentInChildren<StatPause>();
        }
        playerPauseScript.SetPlayerHUDs(temp);
    }
}