using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpgrade : MonoBehaviour
{
    // The upgrade nodes, made as interactable Buttons
    public Button powerRnk2;
    public Button powerRnk3;
    public Text rankTwoDescript;
    public Text rankThreeDescript;

    // Access the HUDManager to get access to the current Power Core count
    HUDManager hud;

    void Start()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
        rankTwoDescript.gameObject.SetActive(false);
        rankThreeDescript.gameObject.SetActive(false);
    }

    // Purchase Rank Two upgrade if player has 2 Power Cores.
    public void PowerUpOne()
    {
        if (hud.coreCounter >= 2)
        {
            Debug.Log("Cost met");
            powerRnk2.GetComponent<Image>().color = Color.white;
        }
        else
        {
            Debug.Log("Cost not met");
        }

        // Used Power Cores (2) are subtracted from current count when upgrade is purchased
        if (powerRnk2.GetComponent<Image>().color == Color.white)
        {
            hud.coreCounter = hud.coreCounter - 2;
            hud.upCoreCount.text = hud.coreCounter.ToString();
        }
    }

    // Purchase Rank Three upgrade if player has 5 Power Cores and Rank Two purchased.
    public void PowerUpTwo()
    {
        if (hud.coreCounter >= 5 && powerRnk2.GetComponent<Image>().color == Color.white)
        {
            Debug.Log("Conditions met");
            powerRnk3.GetComponent<Image>().color = Color.white;
        }
        else
        {
            Debug.Log("Conditions not met");
        }

        // Used Power Cores (5) are subtracted from current count when upgrade is purchased
        if (powerRnk3.GetComponent<Image>().color == Color.white)
        {
            hud.coreCounter = hud.coreCounter - 5;
            hud.upCoreCount.text = hud.coreCounter.ToString();
        }
    }
}
