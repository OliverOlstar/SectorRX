using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollectibles : MonoBehaviour
{
    HUDManager playerHUD;

    //public GameObject cell;
    public GameObject core;
    public GameObject powerPrompt;
    public GameObject cellPrompt;
    public bool firstCore;
    public bool firstCell;

    private void Start()
    {
        playerHUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
        firstCore = false;
        firstCell = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        //If player collides with Power Core item
        if (collision.gameObject.tag == "Core")
        {
            playerHUD.coreUIOn = true;
            Destroy(collision.gameObject);
            playerHUD.coreUI.SetActive(true);
            playerHUD.coreCounter = playerHUD.coreCounter + 1;
            playerHUD.SetCoreCount();
            
            //Show tutorial after collecting first Core
            if(firstCore == false)
            {
                powerPrompt.SetActive(true);
                firstCore = true;
            }
        }

        //Show tutorial after obtaining first Cells
        if (playerHUD.cellCounter >= 150)
        {
            if (firstCell == false)
            {
                cellPrompt.SetActive(true);
                firstCell = true;
            }
        }
    }
}
