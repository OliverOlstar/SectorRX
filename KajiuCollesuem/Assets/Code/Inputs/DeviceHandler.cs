using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Programmer(s): Scott Watman, Oliver Loescher
    Description: Handles device inputs, and calls functions depending on what was pressed.*/

public class DeviceHandler : MonoBehaviour
{
    [HideInInspector] public connectedPlayers addPlayer;
    [HideInInspector] public UIManager manager;
    private Panels playerPanel;


    // If device gets disconnected
    public void OnDeviceLost()
    {
        if (playerPanel != null)
        {
            int playerSlot = playerPanel.PlayerLeft();
            addPlayer.OnDeviceLeaves(playerSlot);
            playerPanel = null;
        }
    }

    private void OnStart()
    {
        playerPanel.OnStart();
    }

    // Disconnects the player device from assigned slot if player has left and panel was assigned
    private void OnBackward()
    {
        if(playerPanel != null)
        {
            // If returns that entered to leave
            if (playerPanel.OnBackward())
            {
                // Disconnect
                int playerSlot = playerPanel.PlayerLeft();
                addPlayer.OnDeviceLeaves(playerSlot);
                playerPanel = null;
            }
        }
        else
        {
            addPlayer.ResetPlayers();
            manager.BackToMainMenu();
        }
    }

    //Connects the player device to an open slot if player a panel has not been assigned to
    public void OnForward()
    {
        if (playerPanel == null)
        {
            playerPanel = addPlayer.OnDeviceJoined();
            playerPanel.PlayerJoined(this);
        }
        else
        {
            playerPanel.OnForward();
        }
    }

    private void OnColorPicking()
    {
        if (playerPanel != null)
        {
            playerPanel.OnColorPicking();
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
            return playerPanel.GetPlayerIndex();
        }
    }

    public void DisableConnecting()
    {
        if (playerPanel != null)
        {
            playerPanel.PlayerLeft();
            playerPanel = null;
        }

        addPlayer = null;
    }
}
