using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Panels : MonoBehaviour
{
    [SerializeField] int playerNumber;
    [SerializeField] private connectedPlayers _AddPlayer;
    public Text playerPanels;
    private int stateValue = 0;

    public Sprite[] abilityIcons;
    public Image[] ability = new Image[2];
    private int presetNumber = 0;
    public bool setOne;
    public bool setTwo;
    public bool abilityLocked;

    private void Start()
    {
        ability[0].GetComponent<Image>().sprite = abilityIcons[3];
        ability[1].GetComponent<Image>().sprite = abilityIcons[2];
    }

    public void OnJoining()
    {
        switch (stateValue)
        {
            case 0:
                if(stateValue == 0)
                {
                    abilityLocked = true;
                    stateValue = 1;
                }
                break;
            
            case 1:
                if (_AddPlayer._Devices.Length >= 2 && abilityLocked)
                {
                    _AddPlayer.SetPlayerOrder();
                    SceneManager.LoadScene(1);
                }
                break;
        }
    }

    public void OnLeft()
    {
        if(stateValue == 0)
        {
            ChangeIcons(presetNumber - 1);
        }
    }

    public void OnRight()
    {
        if (stateValue == 0)
        {
            ChangeIcons(presetNumber + 1);
        }
    }

    public void ChangeIcons(int pDirection)
    {
        if(presetNumber == 0)
        {
            ability[0].GetComponent<Image>().sprite = abilityIcons[3];
            ability[1].GetComponent<Image>().sprite = abilityIcons[2];
        }

        if(presetNumber == 1)
        {
            ability[0].GetComponent<Image>().sprite = abilityIcons[0];
            ability[1].GetComponent<Image>().sprite = abilityIcons[1];
        }
    }

    public void PlayerJoined()
    {
        playerPanels.text = "Player " + playerNumber + " Joined";
        presetNumber = 0;
        ability[0].GetComponent<Image>().sprite = abilityIcons[3];
        ability[1].GetComponent<Image>().sprite = abilityIcons[2];
    }

    public int PlayerLeft()
    {
        playerPanels.text = "Press Space or 'A' to Join";
        return playerNumber - 1;
    }
}