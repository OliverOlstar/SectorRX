using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuscleUpgrade : MonoBehaviour
{
    // The upgrade nodes, made as interactable Buttons
    public Button musRnk1;
    public Button musRnk2;
    public Button musRnk3;

    // Access the HUDManager to get access to the current Power Core count
    HUDManager hud;

    void Start()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
    }

    // Purchase Rank Two upgrade if player has 50 Cells.
    public void MuscleUpOne()
    {
        if (hud.cellCounter >= 50)
        {
            Debug.Log("Cost met");
            musRnk1.GetComponent<Image>().color = Color.red;
        }
        else
        {
            Debug.Log("Cost not met");
        }

        // Used Cells (50) are subtracted from current count when upgrade is purchased
        if (musRnk1.GetComponent<Image>().color == Color.red)
        {
            hud.cellCounter = hud.cellCounter - 50;
            hud.upCellCount.text = hud.cellCounter.ToString();
        }
    }

    // Purchase Rank Two upgrade if player has 150 Cells and Rank One purchased.
    public void MuscleUpTwo()
    {
        if (hud.cellCounter >= 150 && musRnk1.GetComponent<Image>().color == Color.red)
        {
            Debug.Log("Conditions met");
            musRnk2.GetComponent<Image>().color = Color.red;
        }
        else
        {
            Debug.Log("Conditions not met");
        }

        // Used Cells (150) are subtracted from current count when upgrade is purchased
        if (musRnk2.GetComponent<Image>().color == Color.red)
        {
            hud.cellCounter = hud.cellCounter - 150;
            hud.upCellCount.text = hud.cellCounter.ToString();
        }
    }

    // Purchase Rank Three upgrade if player has 300 Cells and Ranks One and Two purchased.
    public void MuscleUpThree()
    {
        if (hud.cellCounter >= 300 && musRnk2.GetComponent<Image>().color == Color.red)
        {
            Debug.Log("Conditions met");
            musRnk3.GetComponent<Image>().color = Color.red;
        }
        else
        {
            Debug.Log("Conditions not met");
        }

        // Used Cells (300) are subtracted from current count when upgrade is purchased
        if (musRnk3.GetComponent<Image>().color == Color.red)
        {
            hud.cellCounter = hud.cellCounter - 300;
            hud.upCellCount.text = hud.cellCounter.ToString();
        }
    }
}
