using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollectibles : MonoBehaviour
{
    HUDManager playerHUD;
    public GameObject cell;
    public GameObject core;

    private void Start()
    {
        playerHUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Core")
        {
            playerHUD.coreUIOn = true;
            Destroy(collision.gameObject);
            playerHUD.coreUI.SetActive(true);
            playerHUD.coreCounter = playerHUD.coreCounter + 1;
            playerHUD.SetCoreCount();
        }

        if (collision.gameObject.tag == "Cell")
        {
            playerHUD.cellUIOn = true;
            Destroy(collision.gameObject);
            playerHUD.cellUI.SetActive(true);
            playerHUD.cellCounter = playerHUD.cellCounter + 1;
            playerHUD.SetCellCount();
        }
    }
}
