using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpgrade : MonoBehaviour
{
    // The upgrade nodes, made as interactable Buttons
    public Text description;
    [SerializeField] private PlayerUpgrades pU;
    private SOStats[] stats;

    // Access the HUDManager to get access to the current Power Core count
    HUDManager hud;

    void Start()
    {
        stats = pU.Stats;

        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
        
    }

    // Purchase Rank Two upgrade if player has 2 Power Cores.
    //public void PowerUpOne()
    //{
    //    if (hud.coreCounter >= 2)
    //    {
    //        Debug.Log("Cost met");
    //        powerRnk2.GetComponent<Image>().color = Color.white;
    //    }
    //    else
    //    {
    //        Debug.Log("Cost not met");
    //    }

    //    // Used Power Cores (2) are subtracted from current count when upgrade is purchased
    //    if (powerRnk2.GetComponent<Image>().color == Color.white)
    //    {
    //        hud.coreCounter = hud.coreCounter - 2;
    //        hud.upCoreCount.text = hud.coreCounter.ToString();
    //    }
    //}

    //// Purchase Rank Three upgrade if player has 5 Power Cores and Rank Two purchased.
    //public void PowerUpTwo()
    //{
    //    if (hud.coreCounter >= 5 && powerRnk2.GetComponent<Image>().color == Color.white)
    //    {
    //        Debug.Log("Conditions met");
    //        powerRnk3.GetComponent<Image>().color = Color.white;
    //    }
    //    else
    //    {
    //        Debug.Log("Conditions not met");
    //    }

    //    // Used Power Cores (5) are subtracted from current count when upgrade is purchased
    //    if (powerRnk3.GetComponent<Image>().color == Color.white)
    //    {
    //        hud.coreCounter = hud.coreCounter - 5;
    //        hud.upCoreCount.text = hud.coreCounter.ToString();
    //    }
    //}
    
    public void HoverButton(int pIndex, int pLevel, float pYPos)
    {
        description.text = stats[pIndex].statDescriptions[pLevel];
        description.rectTransform.position = new Vector2(description.rectTransform.position.x, pYPos);
    }

    public void HoverExit()
    {
        description.text = "";
    }
}
