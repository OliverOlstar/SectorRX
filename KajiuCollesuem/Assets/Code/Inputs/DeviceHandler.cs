using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeviceHandler : MonoBehaviour
{
    [SerializeField] public connectedPlayers _AddPlayer;
    [SerializeField] private Panels playerPanel;
    private int stateValue = 0;

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
        switch (stateValue)
        {
            case 0:
                if (playerPanel == null)
                {
                    playerPanel = _AddPlayer.OnDeviceJoined();

                    if (playerPanel != null)
                    {
                        playerPanel.PlayerJoined();
                        stateValue = 1;
                    }
                }
                break;

            case 2:
                if (_AddPlayer.hasJoined && _AddPlayer._Devices.Length >= 2)
                {
                    _AddPlayer.SetPlayerOrder();
                    SceneManager.LoadScene(1);
                }
                break;
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
