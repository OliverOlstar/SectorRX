using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Programmer(s): Scott Watman, Oliver Loescher
    Description: Handles device inputs, and calls functions depending on what was pressed.*/

public class DeviceHandler : MonoBehaviour
{
    [SerializeField] public connectedPlayers _AddPlayer;
    [SerializeField] private Panels playerPanel;
    [SerializeField] public AbilitySet ability;

    //Disconnects the player device from assigned slot if player has left and panel was assigned
    public void OnLeaving()
    {
        if(playerPanel != null)
        {
            int playerSlot = playerPanel.PlayerLeft();
            _AddPlayer.OnDeviceLeaves(playerSlot);
            playerPanel = null;
        }
    }

    //Connects the player device to an open slot if player a panel has not been assigned to
    public void OnJoining()
    {
        if (playerPanel == null)
        {
            playerPanel = _AddPlayer.OnDeviceJoined();

            if (playerPanel != null)
            {
                playerPanel.PlayerJoined();
            }
        }
        else
        {
            playerPanel.OnJoining();
        }
    }

    private void OnLeft()
    {
        if (playerPanel != null)
        {
            playerPanel.OnLeft();
        }
    }

    private void OnRight()
    {
        if(playerPanel != null)
        {
            playerPanel.OnRight();
        }
    }

    public int GetPlayerIndex()
    {
        if(playerPanel == null)
        {
            return -1;
        }
        else
        {
            return playerPanel.PlayerLeft();
        }
    }

    private void OnColorPicking()
    {
        //if (playerPanel)
        //    playerPanel;
    }
}
