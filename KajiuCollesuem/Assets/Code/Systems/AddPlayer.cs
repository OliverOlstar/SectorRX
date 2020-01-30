using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class AddPlayer : MonoBehaviour
{
    public List<Text> playerPanels = new List<Text>();
    public List<PlayerInput> allPlayers = new List<PlayerInput>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Gamepad.all);
    }

    public void PlayerJoins()
    {
        if (connectedPlayers.playersConnected == 1)
        {
            playerPanels[0].text = "Player 1 Joined";
        }

        if (connectedPlayers.playersConnected == 2)
        {
            playerPanels[1].text = "Player 2 Joined";
        }

        if (connectedPlayers.playersConnected == 3)
        {
            playerPanels[2].text = "Player 3 Joined";
        }

        if (connectedPlayers.playersConnected == 4)
        {
            playerPanels[3].text = "Player 4 Joined";
        }
    }
}
