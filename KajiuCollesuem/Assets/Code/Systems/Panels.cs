using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panels : MonoBehaviour
{
    [SerializeField] int playerNumber;
    public Text playerPanels;

    public void PlayerJoined()
    {
        playerPanels.text = "Player " + playerNumber + " Joined";
    }

    public int PlayerLeft()
    {
        playerPanels.text = "Press Space or 'A' to Join";
        return playerNumber - 1;
    }
}