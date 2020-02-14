using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceHandler : MonoBehaviour
{
    [SerializeField] private connectedPlayers _AddPlayer;
    [SerializeField] private Panels playerPanel;

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
    private void OnJoining()
    {
        if (playerPanel == null)
        {
            playerPanel = _AddPlayer.OnDeviceJoined();
            
            if (playerPanel != null)
            {
                playerPanel.PlayerJoined();
            }
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
