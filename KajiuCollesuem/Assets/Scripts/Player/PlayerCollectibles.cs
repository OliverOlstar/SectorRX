using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollectibles : MonoBehaviour
{
    HUDManager playerHUD;

    //public GameObject cell;
    public GameObject core;
    public GameObject tutorialPrompt;
    public bool firstCore;

    private void Start()
    {
        playerHUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
        firstCore = false;
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
            
            //Show tutorial for upgrading after 
            if(firstCore == false)
            {
                tutorialPrompt.SetActive(true);
                firstCore = true;
            }
        }

        //if (collision.gameObject.tag == "Cell")
        //{
        //    playerHUD.cellUIOn = true;
        //    Destroy(collision.gameObject);
        //    playerHUD.cellUI.SetActive(true);
        //    playerHUD.cellCounter = playerHUD.cellCounter + 1;
        //    playerHUD.SetCellCount();
        //}
    }
}
