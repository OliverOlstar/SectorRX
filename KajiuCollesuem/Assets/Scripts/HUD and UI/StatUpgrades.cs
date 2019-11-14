using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUpgrades : MonoBehaviour
{
    //This script manages all Stat upgrades

    // The upgrade nodes, made as interactable Buttons
    public Button statRnk1;
    public Button statRnk2;
    public Button statRnk3;

    // Access the HUDManager to get access to the current Power Core count
    HUDManager hud;

    void Start()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
    }

    // Purchase Rank One upgrade if player has 50 Cells.
    public void RankUpOne()
    {
        if (hud.cellCounter >= 50)
        {
            Debug.Log("Cost met");
            statRnk1.GetComponent<Image>().color = Color.green;
        }
        else
        {
            Debug.Log("Cost not met");
        }

        // Used Cells (50) are subtracted from current count when upgrade is purchased
        if (statRnk1.GetComponent<Image>().color == Color.green)
        {
            hud.cellCounter = hud.cellCounter - 50;
            hud.upCellCount.text = hud.cellCounter.ToString();
        }
    }

    // Purchase Rank Two upgrade if player has 150 Cells and Rank One purchased.
    public void RankUpTwo()
    {
        if (hud.cellCounter >= 150 && statRnk1.GetComponent<Image>().color == Color.green)
        {
            Debug.Log("Conditions met");
            statRnk2.GetComponent<Image>().color = Color.green;
        }
        else
        {
            Debug.Log("Conditions not met");
        }

        // Used Cells (150) are subtracted from current count when upgrade is purchased
        if (statRnk2.GetComponent<Image>().color == Color.green)
        {
            hud.cellCounter = hud.cellCounter - 150;
            hud.upCellCount.text = hud.cellCounter.ToString();
        }
    }

    // Purchase Rank Three upgrade if player has 300 Cells and Ranks One and Two purchased.
    public void RankUpThree()
    {
        if (hud.cellCounter >= 300 && statRnk2.GetComponent<Image>().color == Color.green)
        {
            Debug.Log("Conditions met");
            statRnk3.GetComponent<Image>().color = Color.green;
        }
        else
        {
            Debug.Log("Conditions not met");
        }

        // Used Cells (300) are subtracted from current count when upgrade is purchased
        if (statRnk3.GetComponent<Image>().color == Color.green)
        {
            hud.cellCounter = hud.cellCounter - 300;
            hud.upCellCount.text = hud.cellCounter.ToString();
        }
    }
}
