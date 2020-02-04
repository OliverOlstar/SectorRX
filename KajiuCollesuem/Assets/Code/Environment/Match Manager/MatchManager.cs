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
    public PlayerSpawn spawnPlayerScript;
    public SplitscreenManager splitscreenScript;
    public SpawnRandomEnemies[] spawnEnemyScript;
    public SpawnEditLava spawnLavaScript;

    public void Start()
    {
        foreach (SpawnRandomEnemies cluster in spawnEnemyScript)
        {
            //cluster.SpawnEnemies();
        }

        spawnPlayerScript.MatchStartup();
    }

    public void Update()
    {
        spawnLavaScript.lavaTimer();
        spawnPlayerScript.MatchEnd();
    }
}
    