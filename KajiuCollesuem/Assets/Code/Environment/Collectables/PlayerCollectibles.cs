using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Programmer: Scott Watman
 Description: Allows each player to collect their own cache of cells for upgrading*/

public class PlayerCollectibles : MonoBehaviour
{
    private float cellAmount = 10.0f;
    [SerializeField] private SliderController _SlideControl;

    private void Start()
    {
        //playerHUD = transform.parent.GetComponentInChildren<HUDManager>();
    }

    public void CollectedCell()
    {
        cellAmount += 10;
        
        if(cellAmount >= 100)
        {
            cellAmount = 100;
        }

        _SlideControl.UpdateBars(3, cellAmount);
    }
}
