/*
Programmer: Kavian Kermani
Additional Programmers: Other people who worked on the script
Description: Match manager: Spawns players and enemies, spawns and manages lava, ends match on end conditions.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    public PlayerSpawn spawnPlayerScript;
    public SpawnRandomEnemies[] spawnEnemyScript;
    public SpawnEditLava spawnLavaScript;

    public void Start()
    {
        foreach (SpawnRandomEnemies cluster in spawnEnemyScript)
        {
            cluster.SpawnEnemies();
        }

        spawnPlayerScript.SpawnPlayer();
    }

    public void Update()
    {
        spawnLavaScript.lavaTimer();
    }
}
    