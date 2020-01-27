using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Programmer: Scott Watman
 Additional Programmer(s): Oliver Loescher
 Description: Allows multiple players to connect and play the game*/

public class connectedPlayers : MonoBehaviour
{
    public static int playersConnected = 0;
    public static int playersToSpawn = 0;
    public Text playerCount;

    private void Awake()
    {
        playersConnected = 0;
        playersToSpawn = 0;
    }

    public  void OnPlayerJoined()
    {
        playersConnected++;
        playersToSpawn++;
        Debug.Log("OnPlayerJoined " + playersConnected);
        playerCount.text = "Number of Players: " + playersConnected.ToString();
    }

    public void OnPlayerLeaves()
    {
        playersConnected--;
        playersToSpawn--;
        Debug.Log("OnPlayerLeaves " + playersConnected);
        playerCount.text = "Number of Players: " + playersConnected.ToString();
    }
}