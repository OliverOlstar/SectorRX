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
    //Add Colour variable
    //Add Ability Presets variable
}

public class connectedPlayers : MonoBehaviour
{
    public static int playersConnected = 0;
    public static int playersToSpawn = 0;
    public bool canJoin;
    private List<int> playerSlots = new List<int>(){0, 1, 2, 3, 4, 5, 6, 7, 8};
    public static List<UsedDevices> playerIndex = new List<UsedDevices>();

    [SerializeField] private DeviceHandler[] _Devices; 
    [SerializeField] private Text _PlayerCount;
    [SerializeField] private Panels[] playerPanels;

    private void Awake()
    {
        playersConnected = 0;
        playersToSpawn = 0;
    }

    public void EnableJoin()
    { 
        if(!canJoin)
            canJoin = true;
    }

    public void ResetPlayers()
    {
        if(canJoin) 
            canJoin = false;
        foreach(DeviceHandler d in _Devices)
        {
            d.OnLeaving();
        }
    }

    //Check user device index and set struct ints to user number
    public void SetPlayerOrder()
    {
        playerIndex.Clear();
        foreach (DeviceHandler d in _Devices)
        {
            UsedDevices user = new UsedDevices();
            user.deviceUser = d.GetComponent<PlayerInput>().user.index;
            user.playerIndex = d.GetPlayerIndex();
            playerIndex.Add(user);
        }
    }

    public Panels OnDeviceJoined()
    {
        //Checks if on join screen
        if (canJoin)
        {
            //Allows players to connect if on join screen
            playersConnected++;
            playersToSpawn++;
            Debug.Log("OnPlayerJoined " + playersConnected);
            _PlayerCount.text = "Number of Players: " + playersConnected.ToString();

            //Sets player panel to first open slot and then removes it from list
            int slot = playerSlots[0];
            playerSlots.Remove(slot);
            return playerPanels[slot];
        }
        else
        {
            return null;
        }
    }

    public void OnDeviceLeaves(int pSlot)
    {
        //Disconnects player
        playersConnected--;
        playersToSpawn--;
        Debug.Log("OnPlayerLeaves " + playersConnected);
        _PlayerCount.text = "Number of Players: " + playersConnected.ToString();

        //Adds new open player slot to list then properly sorts list
        playerSlots.Add(pSlot);
        playerSlots.Sort();
    }
}