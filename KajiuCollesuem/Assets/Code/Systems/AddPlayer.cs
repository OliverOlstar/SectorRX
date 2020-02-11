using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class AddPlayer : MonoBehaviour
{
    public List<Text> playerPanels = new List<Text>();
    public List<PlayerInput> allPlayers = new List<PlayerInput>();

    public void PlayerJoins()
    {
        playerPanels[connectedPlayers.playersConnected - 1].text = "Player " + connectedPlayers.playersConnected + " Joined";
    }

    public void PlayerLeaves()
    {
        playerPanels[connectedPlayers.playersConnected].text = "Press Any Button to JOIN";
    }

    public void SwitchDefaultScheme()
    {
        if(Keyboard.current.anyKey.wasPressedThisFrame)
        {
            if(allPlayers[connectedPlayers.playersConnected - 1].currentControlScheme == "Gamepad")
            {
                allPlayers[connectedPlayers.playersConnected - 1].SwitchCurrentControlScheme("Keyboard&Mouse");
            }
        }

        //if (Gamepad.current.buttonSouth.wasPressedThisFrame)
        //{
        //    if (allPlayers[connectedPlayers.playersConnected - 1].currentControlScheme == "Keyboard&Mouse")
        //    {
        //        allPlayers[connectedPlayers.playersConnected - 1].SwitchCurrentControlScheme("Gamepad");
        //    }
        //}
    }
}