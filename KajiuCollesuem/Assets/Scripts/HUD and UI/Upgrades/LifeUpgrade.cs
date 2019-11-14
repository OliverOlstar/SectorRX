using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUpgrade : MonoBehaviour
{
    // The upgrade nodes, made as interactable Buttons
    public Button lifeRnk1;
    public Button lifeRnk2;
    public Button lifeRnk3;

    // Access the HUDManager to get access to the current Power Core count
    HUDManager hud;

    void Start()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
    }

    // Purchase Rank Two upgrade if player has 50 Cells.
    public void LifeUpOne()
    {
        if (hud.cellCounter >= 50)
        {
            Debug.Log("Cost met");
            lifeRnk1.GetComponent<Image>().color = Color.green;
        }
        else
        {
            Debug.Log("Cost not met");
        }

        // Used Cells (50) are subtracted from current count when upgrade is purchased
        if (lifeRnk1.GetComponent<Image>().color == Color.green)
        {
            hud.cellCounter = hud.cellCounter - 50;
            hud.upCellCount.text = hud.cellCounter.ToString();
        }
    }

    // Purchase Rank Two upgrade if player has 150 Cells and Rank One purchased.
    public void LifeUpTwo()
    {
        if (hud.cellCounter >= 150 && lifeRnk1.GetComponent<Image>().color == Color.green)
        {
            Debug.Log("Conditions met");
            lifeRnk2.GetComponent<Image>().color = Color.green;
        }
        else
        {
            Debug.Log("Conditions not met");
        }

        // Used Cells (150) are subtracted from current count when upgrade is purchased
        if (lifeRnk2.GetComponent<Image>().color == Color.green)
        {
            hud.cellCounter = hud.cellCounter - 150;
            hud.upCellCount.text = hud.cellCounter.ToString();
        }
    }

    // Purchase Rank Three upgrade if player has 300 Cells and Ranks One and Two purchased.
    public void LifeUpThree()
    {
        if (hud.cellCounter >= 300 && lifeRnk2.GetComponent<Image>().color == Color.green)
        {
            Debug.Log("Conditions met");
            lifeRnk3.GetComponent<Image>().color = Color.green;
        }
        else
        {
            Debug.Log("Conditions not met");
        }

        // Used Cells (300) are subtracted from current count when upgrade is purchased
        if (lifeRnk3.GetComponent<Image>().color == Color.green)
        {
            hud.cellCounter = hud.cellCounter - 300;
            hud.upCellCount.text = hud.cellCounter.ToString();
        }
    }
}
