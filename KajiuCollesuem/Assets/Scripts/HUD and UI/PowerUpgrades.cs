using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpgrades : MonoBehaviour
{
    //This script manages all Power upgrades.

    // The upgrade nodes, made as interactable Buttons
    public Button powerRnk2;
    public Button powerRnk3;

    // Access the HUDManager to get access to the current Power Core count
    HUDManager hud;

    void Start()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
    }

    // Purchase Rank Two upgrade if player has 2 Power Cores.
    public void IceUpOne()
    {
        if (hud.coreCounter >= 2)
        {
            Debug.Log("Cost met");
            powerRnk2.GetComponent<Image>().color = Color.green;
        }
        else
        {
            Debug.Log("Cost not met");
        }

        // Used Power Cores (2) are subtracted from current count when upgrade is purchased
        if (powerRnk2.GetComponent<Image>().color == Color.green)
        {
            hud.coreCounter = hud.coreCounter - 2;
        }
    }

    // Purchase Rank Three upgrade if player has 5 Power Cores and Rank Two purchased.
    public void IceUpTwo()
    {
        if (hud.coreCounter >= 5 && powerRnk2.GetComponent<Image>().color == Color.green)
        {
            Debug.Log("Conditions met");
            powerRnk3.GetComponent<Image>().color = Color.green;
        }
        else
        {
            Debug.Log("Conditions not met");
        }

        // Used Power Cores (5) are subtracted from current count when upgrade is purchased
        if (powerRnk3.GetComponent<Image>().color == Color.green)
        {
            hud.coreCounter = hud.coreCounter - 5;
        }
    }

}
