using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectricUpgrade : MonoBehaviour
{
    // The upgrade nodes, made as interactable Buttons
    public Button elecRnk2;
    public Button elecRnk3;

    // Access the HUDManager to get access to the current Power Core count
    HUDManager hud;

    void Start()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
    }

    // Purchase Rank Two upgrade if player has 2 Power Cores.
    public void ElecUpOne()
    {
        if (hud.coreCounter >= 2)
        {
            Debug.Log("Cost met");
            elecRnk2.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            Debug.Log("Cost not met");
            hud.coreNotficationOne.gameObject.SetActive(true);
            hud.StartCoroutine("CoreNotifyOne");
        }

        // Used Power Cores (2) are subtracted from current count when upgrade is purchased
        if (elecRnk2.GetComponent<Image>().color == Color.yellow)
        {
            hud.coreCounter = hud.coreCounter - 2;
            hud.upCoreCount.text = hud.coreCounter.ToString();
        }
    }

    // Purchase Rank Three upgrade if player has 5 Power Cores and Rank Two purchased.
    public void ElecUpTwo()
    {
        if (hud.coreCounter >= 5 && elecRnk2.GetComponent<Image>().color == Color.yellow)
        {
            Debug.Log("Conditions met");
            elecRnk3.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            Debug.Log("Conditions not met");
            hud.coreNotficationTwo.gameObject.SetActive(true);
            hud.StartCoroutine("CoreNotifyTwo");
        }

        // Used Power Cores (5) are subtracted from current count when upgrade is purchased
        if (elecRnk3.GetComponent<Image>().color == Color.yellow)
        {
            hud.coreCounter = hud.coreCounter - 5;
            hud.upCoreCount.text = hud.coreCounter.ToString();
        }
    }
}
