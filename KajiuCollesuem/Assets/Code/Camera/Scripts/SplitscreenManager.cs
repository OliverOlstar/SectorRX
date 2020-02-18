using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Programmer: Scott Watman
 Description: Changes Splitscreen between Horizontal and Vertical*/

public class SplitscreenManager : MonoBehaviour
{
    public List<Camera> playerCams = new List<Camera>();

    public bool isHorizontalSplit;

    public void SetSplitScreen(MatchManager pManager)
    {
        foreach(GameObject player in pManager.spawnPlayerScript.players)
        {
            Camera getCam = player.GetComponentInChildren<Camera>();
            playerCams.Add(getCam);
        }

        //Player 1 Camera settings when Horizontal.
        playerCams[0].rect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
        playerCams[0].fieldOfView = 55;

        //Player 2 Camera settings when Horizontal.
        playerCams[1].rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
        playerCams[1].fieldOfView = 55;
    }
}
