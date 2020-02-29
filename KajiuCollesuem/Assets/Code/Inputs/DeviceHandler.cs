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
    public void OnBackward()
    {
        if(playerPanel != null)
        {
            int playerSlot = playerPanel.PlayerLeft();
            _AddPlayer.OnDeviceLeaves(playerSlot);
            playerPanel = null;
        }
    }

    public void DisableConnecting()
    {
        if (playerPanel != null)
        {
            playerPanel.PlayerLeft();
            playerPanel = null;
        }

        _AddPlayer = null;
    }

    //Connects the player device to an open slot if player a panel has not been assigned to
    public void OnForward()
    {
        if (playerPanel == null)
        {
            playerPanel = _AddPlayer.OnDeviceJoined();
            playerPanel.PlayerJoined();
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
            StartCoroutine(LeaveWaitTime());
            return playerPanel.PlayerLeft();
        }
    }

    IEnumerator LeaveWaitTime()
    {
        yield return new WaitForSeconds(1.0f);
    }

    private void OnColorPicking()
    {
        //if (playerPanel)
        //    playerPanel;
    }
}
