/*
Programmer: Kavian Kermani
Additional Programmers: Scott Watman
Description: Match manager: Spawns players and enemies, spawns and manages lava, ends match on end conditions.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MatchManager : MonoBehaviour
{
    public static MatchManager instance = null;

    public PlayerSpawn spawnPlayerScript;
    [SerializeField] private Camera cinemaCam;
    [SerializeField] private SplitscreenManager splitscreenScript;
    [SerializeField] private SpawnRandomEnemies[] spawnEnemyScript;
    [SerializeField] private SpawnEditLava spawnLavaScript;
    [SerializeField] private PauseMenu playerPauseScript;

    [SerializeField] private MusicManager musicManager;
    public RectTransform transitionScreen;

    private float _MatchStartTime;

    private void Awake()
    {
        instance = this;
        Cursor.visible = false;
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

    IEnumerator CinemaOff()
    {
        //Wait for three seconds then turn off camera (simulates cinematic camera).
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(5.0f);
        cinemaCam.gameObject.SetActive(false);

        spawnPlayerScript.SpawnAllPlayers();
        splitscreenScript.SetSplitScreen(this);
        PauseStatus();

        _MatchStartTime = Time.time;
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

    public bool ManagerEnd()
    {
        int playersAlive = spawnPlayerScript.PlayerDied();

        // Check if match is over
        if (playersAlive <= 1)
        {
            StartCoroutine("VictoryReset");
            return false;
        }

        return true;
    }

    IEnumerator VictoryReset()
    {
        yield return new WaitForSeconds(3.5f);

        musicManager.mainAudio.Stop();
        transitionScreen.DOAnchorPos(new Vector2(0, 0), 0.4f);

        for (int i = 0; i < connectedPlayers.playersConnected; i++)
        {
            UsedDevices player = connectedPlayers.playerIndex[i];

            // Save Stats
            PlayerCollectibles collectables = spawnPlayerScript.players[i].GetComponentInChildren<PlayerCollectibles>();
            player.victoryScene.Stats = collectables.GetStats();

            // Save if he lived or not
            PlayerAttributes attributes = collectables.GetComponent<PlayerAttributes>();
            player.victoryScene.Alive = !attributes.IsDead();
            player.victoryScene.TimeOfDeath = attributes.TimeOfDeath() - _MatchStartTime;

            connectedPlayers.playerIndex[i] = player;
        }

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync(2);
    }
}