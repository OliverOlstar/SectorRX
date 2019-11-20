using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUpgrade : MonoBehaviour
{
    // The upgrade nodes, made as interactable Buttons
    public Button enrgyRnk1;
    public Button enrgyRnk2;
    public Button enrgyRnk3;

    // Access the HUDManager to get access to the current Power Core count
    HUDManager hud;

    void Start()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
    }

    // Purchase Rank Two upgrade if player has 50 Cells.
    public void EnergyUpOne()
    {
        if (hud.cellCounter >= 50)
        {
            Debug.Log("Cost met");
            enrgyRnk1.GetComponent<Image>().color = Color.blue;
        }
        else
        {
            Debug.Log("Cost not met");
        }

        // Used Cells (50) are subtracted from current count when upgrade is purchased
        if (enrgyRnk1.GetComponent<Image>().color == Color.blue)
        {
            hud.cellCounter = hud.cellCounter - 50;
        }
    }

    // Purchase Rank Two upgrade if player has 150 Cells and Rank One purchased.
    public void EnergyUpTwo()
    {
        if (hud.cellCounter >= 150 && enrgyRnk1.GetComponent<Image>().color == Color.blue)
        {
            Debug.Log("Conditions met");
            enrgyRnk2.GetComponent<Image>().color = Color.blue;
        }
        else
        {
            Debug.Log("Conditions not met");
        }

        // Used Cells (150) are subtracted from current count when upgrade is purchased
        if (enrgyRnk2.GetComponent<Image>().color == Color.blue)
        {
            hud.cellCounter = hud.cellCounter - 150;
        }
    }

    // Purchase Rank Three upgrade if player has 300 Cells and Ranks One and Two purchased.
    public void EnergyUpThree()
    {
        if (hud.cellCounter >= 300 && enrgyRnk2.GetComponent<Image>().color == Color.blue)
        {
            Debug.Log("Conditions met");
            enrgyRnk3.GetComponent<Image>().color = Color.blue;
        }
        else
        {
            Debug.Log("Conditions not met");
        }

        // Used Cells (300) are subtracted from current count when upgrade is purchased
        if (enrgyRnk3.GetComponent<Image>().color == Color.blue)
        {
            hud.cellCounter = hud.cellCounter - 300;
        }
    }
}
