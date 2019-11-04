using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IceUpgrade : MonoBehaviour
{
    // The upgrade nodes, made as interactable Buttons
    public Button iceRnk2;
    public Button iceRnk3;

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
            iceRnk2.GetComponent<Image>().color = Color.blue;
        }
        else
        {
            Debug.Log("Cost not met");
        }

        // Used Power Cores (2) are subtracted from current count when upgrade is purchased
        if (iceRnk2.GetComponent<Image>().color == Color.blue)
        {
            hud.coreCounter = hud.coreCounter - 2;
        }
    }

    // Purchase Rank Three upgrade if player has 5 Power Cores and Rank Two purchased.
    public void IceUpTwo()
    {
        if (hud.coreCounter >= 5 && iceRnk2.GetComponent<Image>().color == Color.blue)
        {
            Debug.Log("Conditions met");
            iceRnk3.GetComponent<Image>().color = Color.blue;
        }
        else
        {
            Debug.Log("Conditions not met");
        }

        // Used Power Cores (5) are subtracted from current count when upgrade is purchased
        if (iceRnk3.GetComponent<Image>().color == Color.blue)
        {
            hud.coreCounter = hud.coreCounter - 5;
        }
    }
}
