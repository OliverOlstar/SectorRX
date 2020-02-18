using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Programmer: Scott Watman
 Description: Changes Splitscreen between Horizontal and Vertical*/

public class SplitscreenManager : MonoBehaviour
{
    public List<Camera> playerCams = new List<Camera>();
    private float verticalSplit = 1;
    private float horizontalSplit = 1;

    public void SetSplitScreen(MatchManager pManager)
    {
        foreach(GameObject player in pManager.spawnPlayerScript.players)
        {
            Camera getCam = player.GetComponentInChildren<Camera>();
            playerCams.Add(getCam);
        }

        //Track amount of player cameras in game do determine horizontal and vertical split
        if(playerCams.Count >= 7)
        {
            horizontalSplit = 3;
            verticalSplit = 3;
        }
        else if(playerCams.Count >= 5)
        {
            horizontalSplit = 3;
            verticalSplit = 2;
        }
        else if(playerCams.Count >= 3)
        {
            horizontalSplit = 2;
            verticalSplit = 2;
        }
        else if(playerCams.Count == 2)
        {
            playerCams[0].rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
            playerCams[1].rect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
            return;
        }

        float hor = 1.0f / horizontalSplit;
        float ver = 1.0f / verticalSplit;

        //Set vertical and horizontal values for all connected cameras
        for (int i = 0; i < playerCams.Count; i++)
        {
            playerCams[i].rect = new Rect(hor * (i % horizontalSplit), (ver * (verticalSplit - 1)) - ver * Mathf.Floor(i / verticalSplit), hor, ver);
            Debug.Log("Ver: " + Mathf.Floor(i / verticalSplit));
        }
    }
}
