using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Programmer: Scott Watman
 Description: Changes Splitscreen between Horizontal and Vertical*/

public class SplitscreenManager : MonoBehaviour
{
    public Camera[] playerCam;

    public bool isHorizontalSplit;

    public void SetSplitScreen()
    {
        //Player 1 Camera settings when Horizontal.
        playerCam[0].rect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
        playerCam[0].fieldOfView = 55;

        //Player 2 Camera settings when Horizontal.
        playerCam[1].rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
        playerCam[1].fieldOfView = 55;
    }
}
