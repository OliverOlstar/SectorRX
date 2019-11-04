using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagmaUpgrade : MonoBehaviour
{
    // The upgrade nodes, made as interactable Buttons
    public Button magmaRnk2;
    public Button magmaRnk3;

    // Access the HUDManager to get access to the current Power Core count
    HUDManager hud;

    void Start()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
    }

    // Purchase Rank Two upgrade if player has 2 Power Cores.
    public void MagmaUpOne()
    {
        if (hud.coreCounter >= 2)
        {
            Debug.Log("Cost met");
            magmaRnk2.GetComponent<Image>().color = Color.red;
        }
        else
        {
            Debug.Log("Cost not met");
        }

        // Used Power Cores (2) are subtracted from current count when upgrade is purchased
        if (magmaRnk2.GetComponent<Image>().color == Color.red)
        {
            hud.coreCounter = hud.coreCounter - 2;
        }
    }

    // Purchase Rank Three upgrade if player has 5 Power Cores and Rank Two purchased.
    public void MagmaUpTwo()
    {
        if (hud.coreCounter >= 5 && magmaRnk2.GetComponent<Image>().color == Color.red)
        {
            Debug.Log("Conditions met");
            magmaRnk3.GetComponent<Image>().color = Color.red;
        }
        else
        {
            Debug.Log("Conditions not met");
        }

        // Used Power Cores (5) are subtracted from current count when upgrade is purchased
        if (magmaRnk3.GetComponent<Image>().color == Color.red)
        {
            hud.coreCounter = hud.coreCounter - 5;
        }
    }
}
