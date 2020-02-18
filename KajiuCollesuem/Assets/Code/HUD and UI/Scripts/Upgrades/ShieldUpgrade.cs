using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldUpgrade : MonoBehaviour
{
    // The upgrade nodes, made as interactable Buttons
    public Button shldRnk1;
    public Button shldRnk2;
    public Button shldRnk3;

    // Access the HUDManager to get access to the current Power Core count
    HUDManager hud;

    void Start()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
    }

    // Purchase Rank Two upgrade if player has 50 Cells.
    //public void ShieldUpOne()
    //{
    //    if (hud.cellCounter >= 50)
    //    {
    //        Debug.Log("Cost met");
    //        shldRnk1.GetComponent<Image>().color = Color.yellow;
    //    }
    //    else
    //    {
    //        Debug.Log("Cost not met");
    //    }

    //    // Used Cells (50) are subtracted from current count when upgrade is purchased
    //    if (shldRnk1.GetComponent<Image>().color == Color.yellow)
    //    {
    //        hud.cellCounter = hud.cellCounter - 50;
    //        hud.upCellCount.text = hud.cellCounter.ToString();
    //    }
    //}

    //// Purchase Rank Two upgrade if player has 150 Cells and Rank One purchased.
    //public void ShieldUpTwo()
    //{
    //    if (hud.cellCounter >= 150 && shldRnk1.GetComponent<Image>().color == Color.yellow)
    //    {
    //        Debug.Log("Conditions met");
    //        shldRnk2.GetComponent<Image>().color = Color.yellow;
    //    }
    //    else
    //    {
    //        Debug.Log("Conditions not met");
    //    }

    //    // Used Cells (150) are subtracted from current count when upgrade is purchased
    //    if (shldRnk2.GetComponent<Image>().color == Color.yellow)
    //    {
    //        hud.cellCounter = hud.cellCounter - 150;
    //        hud.upCellCount.text = hud.cellCounter.ToString();
    //    }
    //}

    //// Purchase Rank Three upgrade if player has 300 Cells and Ranks One and Two purchased.
    //public void ShieldUpThree()
    //{
    //    if (hud.cellCounter >= 300 && shldRnk2.GetComponent<Image>().color == Color.yellow)
    //    {
    //        Debug.Log("Conditions met");
    //        shldRnk3.GetComponent<Image>().color = Color.yellow;
    //    }
    //    else
    //    {
    //        Debug.Log("Conditions not met");
    //    }

    //    // Used Cells (300) are subtracted from current count when upgrade is purchased
    //    if (shldRnk3.GetComponent<Image>().color == Color.yellow)
    //    {
    //        hud.cellCounter = hud.cellCounter - 300;
    //        hud.upCellCount.text = hud.cellCounter.ToString();
    //    }
    //}
}
