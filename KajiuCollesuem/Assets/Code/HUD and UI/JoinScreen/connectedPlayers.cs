using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

/*Programmer: Scott Watman
 Additional Programmer(s): Oliver Loescher
 Description: Allows multiple players to connect and play the game*/

public struct UsedDevices
{
    public int deviceUser;
    public int playerIndex;
    public ColorSet playerColorSet;
    public int abilitySelected;

    public VictoryStats victoryScene;
}

public struct VictoryStats
{
    public float[] Stats;
    public bool Alive;
}

public class connectedPlayers : MonoBehaviour
{
    public static int playersConnected = 0;
    private List<int> playerSlots = new List<int>() { 0, 1, 2, 3, 4, 5 };
    public static List<UsedDevices> playerIndex = new List<UsedDevices>();
    public GameObject devicePrefab;
    public GameObject startButton;

    private DeviceHandler[] _Devices = new DeviceHandler[6]; 
    [SerializeField] private Panels[] playerPanels;

    private void Awake()
    {
        playersConnected = 0;
        playerIndex.Clear();

        if(UIManager.menuProperties == true)
            EnableJoin();
    }

    // When you enter the join game screen
    public void EnableJoin()
    { 
        for (int i = 0; i < _Devices.Length; i++)
        {
            _Devices[i] = Instantiate(devicePrefab).GetComponent<DeviceHandler>();
            _Devices[i]._AddPlayer = this;
        }
    }

    // When you leave the join game screen
    public void ResetPlayers()
    {
        //Disconnects players
        for (int i = 0; i < _Devices.Length; i++)
        {
            _Devices[i].DisableConnecting();
            Destroy(_Devices[i].gameObject);
            _Devices[i] = null;
        }

        playersConnected = 0;
        playerSlots = new List<int>() { 0, 1, 2, 3, 4, 5 };
    }

    public void SetPlayerOrder()
    {
        playerIndex.Clear();
        int numberOfNotFound = 0;

        foreach (Panels panel in playerPanels)
        {
            if (panel != null && panel.myDevice != null)
            {
                UsedDevices user = new UsedDevices();
                user.deviceUser = panel.myDevice.GetComponent<PlayerInput>().user.index;
                user.playerIndex = panel.myDevice.GetPlayerIndex() - numberOfNotFound;
                user.playerColorSet = panel.myColorSet;
                user.abilitySelected = panel.abilityNumber;
                playerIndex.Add(user);
            }
            else
            {
                numberOfNotFound++;
            }
        }
    }

    public Panels OnDeviceJoined()
    {
        //Allows players to connect if on join screen
        playersConnected++;

        //Sets player panel to first open slot and then removes it from list
        int slot = playerSlots[0];
        playerSlots.Remove(slot);
        return playerPanels[slot];
    }

    public void OnDeviceLeaves(int pSlot)
    {
        //Disconnects player
        playersConnected--;

        //Adds new open player slot to list then properly sorts list
        playerSlots.Add(pSlot);
        playerSlots.Sort();
    }
}