using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Programmer: Scott Watman
 Additional Programmer(s): Oliver Loescher
 Description: Spawns number of players depending on how many joined at main menu.*/

public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefab;

    void Awake()
    {
        Debug.Log("Spawning " + connectedPlayers.playersConnected + " players");

        for (int i = 0; i < connectedPlayers.playersConnected; i++)
        {
            Instantiate(playerPrefab, new Vector3(100, 0, 0), playerPrefab.transform.rotation);
        }
    }
}