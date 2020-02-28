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
        ability[0].GetComponent<Image>().sprite = abilityIcons[2];
        ability[1].GetComponent<Image>().sprite = abilityIcons[3];
    }

    private void Update()
    {
        Debug.Log(presetNumber);
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
                if (connectedPlayers.playersConnected >= 2 && abilityLocked)
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
            ChangeIcons(-1);
        }
    }

    public void OnRight()
    {
        if (stateValue == 0)
        {
            ChangeIcons(1);
        }
    }

    public void ChangeIcons(int pDirection)
    {
        presetNumber += pDirection;

        // Check if outside bounds
        if(presetNumber < 0)
        {
            presetNumber = abilityIcons.Length / 2 - 1;
        }
        else if(presetNumber >= abilityIcons.Length / 2)
        {
            presetNumber = 0;
        }

        ability[0].GetComponent<Image>().sprite = abilityIcons[presetNumber * 2];
        ability[1].GetComponent<Image>().sprite = abilityIcons[presetNumber * 2 + 1];
    }

    public void PlayerJoined()
    {
        playerPanels.text = "Player " + playerNumber + " Joined";
        presetNumber = 0;
        ability[0].GetComponent<Image>().sprite = abilityIcons[2];
        ability[1].GetComponent<Image>().sprite = abilityIcons[3];
    }

    public int PlayerLeft()
    {
        playerPanels.text = "Press Space or 'Start' to Join";
        return playerNumber - 1;
    }
}