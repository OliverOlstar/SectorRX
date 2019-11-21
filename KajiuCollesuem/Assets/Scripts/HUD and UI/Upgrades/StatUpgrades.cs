﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUpgrades : MonoBehaviour
{
    public Text descriptionStat;
    public Text descriptionPower;
    public Text insufficientCost;
    [SerializeField] private PlayerUpgrades pU;
    private SOStats[] stats;
    private SOPowers[] powers;

    // Access the HUDManager to get access to the current Power Core count
    HUDManager hud;

    void Start()
    {
        stats = pU.Stats;
        powers = pU.Powers;
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
        insufficientCost.gameObject.SetActive(false);
    }

    // Purchase Rank One upgrade if player has 50 Cells.
    //public void RankUpOne()
    //{
    //    if (hud.cellCounter >= 50)
    //    {
    //        Debug.Log("Cost met");
    //        statRnk1.GetComponent<Image>().color = Color.green;
    //    }
    //    else
    //    {
    //        Debug.Log("Cost not met");
    //    }

    //    // Used Cells (50) are subtracted from current count when upgrade is purchased
    //    if (statRnk1.GetComponent<Image>().color == Color.green)
    //    {
    //        hud.cellCounter = hud.cellCounter - 50;
    //        hud.upCellCount.text = hud.cellCounter.ToString();
    //    }
    //}

    //// Purchase Rank Two upgrade if player has 150 Cells and Rank One purchased.
    //public void RankUpTwo()
    //{
    //    if (hud.cellCounter >= 150 && statRnk1.GetComponent<Image>().color == Color.green)
    //    {
    //        Debug.Log("Conditions met");
    //        statRnk2.GetComponent<Image>().color = Color.green;
    //    }
    //    else
    //    {
    //        Debug.Log("Conditions not met");
    //    }

    //    // Used Cells (150) are subtracted from current count when upgrade is purchased
    //    if (statRnk2.GetComponent<Image>().color == Color.green)
    //    {
    //        hud.cellCounter = hud.cellCounter - 150;
    //        hud.upCellCount.text = hud.cellCounter.ToString();
    //    }
    //}

    //// Purchase Rank Three upgrade if player has 300 Cells and Ranks One and Two purchased.
    //public void RankUpThree()
    //{
    //    if (hud.cellCounter >= 300 && statRnk2.GetComponent<Image>().color == Color.green)
    //    {
    //        Debug.Log("Conditions met");
    //        statRnk3.GetComponent<Image>().color = Color.green;
    //    }
    //    else
    //    {
    //        Debug.Log("Conditions not met");
    //    }

    //    // Used Cells (300) are subtracted from current count when upgrade is purchased
    //    if (statRnk3.GetComponent<Image>().color == Color.green)
    //    {
    //        hud.cellCounter = hud.cellCounter - 300;
    //        hud.upCellCount.text = hud.cellCounter.ToString();
    //    }
    //}

    public void ClickButtonPower(int pIndex, int pLevel)
    {
        if(hud.coreCounter >= powers[pIndex].cost[pLevel])
        {
            if(pU.PowerUpgrade(pIndex, pLevel))
            {
                hud.coreCounter -= powers[pIndex].cost[pLevel];
            }
            else
            {
                insufficientCost.text = "Can't do level";
                insufficientCost.gameObject.SetActive(false);
                insufficientCost.gameObject.SetActive(true);
            }
        }
        else
        {
            insufficientCost.text = "Not enough cores";
            insufficientCost.gameObject.SetActive(false);
            insufficientCost.gameObject.SetActive(true);
        }
    }

    public void ClickButtonStat(int pIndex, int pLevel)
    {
        if (hud.cellCounter >= stats[pIndex].cost[pLevel])
        {
            if (pU.LevelUp(pIndex, pLevel))
            {
                hud.cellCounter -= stats[pIndex].cost[pLevel];
            }
            else
            {
                insufficientCost.text = "Can't do level";
                insufficientCost.gameObject.SetActive(false);
                insufficientCost.gameObject.SetActive(true);
            }
        }
        else
        {
            insufficientCost.text = "Not enough cells";
            insufficientCost.gameObject.SetActive(false);
            insufficientCost.gameObject.SetActive(true);
        }
    }

    public void HoverStatButton(int pIndex, int pLevel, float pYPos)
    {
        descriptionStat.text = stats[pIndex].statDescriptions[pLevel];
        descriptionStat.rectTransform.position = new Vector2(descriptionStat.rectTransform.position.x, pYPos);
    }
    public void HoverPowerButton(int pIndex, int pLevel, float pYPos)
    {
        descriptionPower.text = powers[pIndex].powerDescriptions[pLevel];
        descriptionPower.rectTransform.position = new Vector2(descriptionPower.rectTransform.position.x, pYPos);
    }

    public void HoverExit()
    {
        descriptionStat.text = "";
        descriptionPower.text = "";
    }
}
