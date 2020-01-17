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
    public Text playerCount;

    public  void OnPlayerJoined()
    {
        playersConnected++;
        Debug.Log("OnPlayerJoined " + playersConnected);
        playerCount.text = "Number of Players: " + playersConnected.ToString();
    }
}