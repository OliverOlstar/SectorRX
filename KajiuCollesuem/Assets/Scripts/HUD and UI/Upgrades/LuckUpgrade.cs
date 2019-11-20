using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LuckUpgrade : MonoBehaviour
{
    // The upgrade nodes, made as interactable Buttons
    public Button luckRnk1;
    public Button luckRnk2;
    public Button luckRnk3;

    // Access the HUDManager to get access to the current Power Core count
    HUDManager hud;

    void Start()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
    }

    // Purchase Rank One upgrade if player has 50 Cells.
    public void LuckUpOne()
    {
        if (hud.cellCounter >= 50)
        {
            Debug.Log("Cost met");
            luckRnk1.GetComponent<Image>().color = Color.white;
        }
        else
        {
            Debug.Log("Cost not met");
        }

        // Used Cells (50) are subtracted from current count when upgrade is purchased
        if (luckRnk1.GetComponent<Image>().color == Color.white)
        {
            hud.cellCounter = hud.cellCounter - 50;
            hud.upCellCount.text = hud.cellCounter.ToString();
        }
    }

    // Purchase Rank Two upgrade if player has 150 Cells and Rank One purchased.
    public void LuckUpTwo()
    {
        if (hud.cellCounter >= 150 && luckRnk1.GetComponent<Image>().color == Color.white)
        {
            Debug.Log("Conditions met");
            luckRnk2.GetComponent<Image>().color = Color.white;
        }
        else
        {
            Debug.Log("Conditions not met");
        }

        // Used Cells (150) are subtracted from current count when upgrade is purchased
        if (luckRnk2.GetComponent<Image>().color == Color.white)
        {
            hud.cellCounter = hud.cellCounter - 150;
            hud.upCellCount.text = hud.cellCounter.ToString();
        }
    }

    // Purchase Rank Three upgrade if player has 300 Cells and Ranks One and Two purchased.
    public void LuckUpThree()
    {
        if (hud.cellCounter >= 300 && luckRnk2.GetComponent<Image>().color == Color.white)
        {
            Debug.Log("Conditions met");
            luckRnk3.GetComponent<Image>().color = Color.white;
        }
        else
        {
            Debug.Log("Conditions not met");
        }

        // Used Cells (300) are subtracted from current count when upgrade is purchased
        if (luckRnk3.GetComponent<Image>().color == Color.white)
        {
            hud.cellCounter = hud.cellCounter - 300;
            hud.upCellCount.text = hud.cellCounter.ToString();
        }
    }
}
