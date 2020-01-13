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

    public SpawnRandomPlayers spawnPlayerScript;
    public SpawnRandomEnemies[] spawnEnemyScript;

    public void Awake()
    {
        foreach (SpawnRandomEnemies cluster in spawnEnemyScript)
        {
            cluster.SpawnEnemies();
        }
    }

    public void Start()
    {
        spawnPlayerScript.SpawnPlayers();
    }
}
    