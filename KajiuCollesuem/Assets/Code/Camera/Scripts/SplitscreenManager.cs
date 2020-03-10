using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Programmer: Scott Watman
 Additional Programmers: Oliver Loescher
 Description: Checks how many players there are and splits the screen accordingly*/

public class SplitscreenManager : MonoBehaviour
{
    [HideInInspector] public List<Camera> playerCams = new List<Camera>();
    [SerializeField] private SOCamera[] cameraPresets = new SOCamera[4];
    private int verticalSplit = 1;
    private int horizontalSplit = 1;

    public void SetSplitScreen(MatchManager pManager)
    {
        foreach(GameObject player in pManager.spawnPlayerScript.players)
        {
            Camera getCam = player.GetComponentInChildren<Camera>();
            playerCams.Add(getCam);
        }

        //Track amount of player cameras in game do determine horizontal and vertical split
        if(playerCams.Count >= 5)
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
            playerCams[0].GetComponentInParent<PlayerCamera>().SetPlayerCameraPresets(cameraPresets[0], cameraPresets[1], cameraPresets[2]);
            playerCams[0].rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
            
            playerCams[1].GetComponentInParent<PlayerCamera>().SetPlayerCameraPresets(cameraPresets[0], cameraPresets[1], cameraPresets[2]);
            playerCams[1].rect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
            
            return;
        }
        else
        {
            // Don't change anything if singleplayer
            return;
        }

        float hor = 1.0f / horizontalSplit;
        float ver = 1.0f / verticalSplit;

        //Set vertical and horizontal values for all connected cameras
        for (int y = 0; y < verticalSplit; y++) 
        {
            for (int x = 0; x < horizontalSplit; x++)
            {
                int index = y * horizontalSplit + x;

                if (index >= connectedPlayers.playersConnected)
                    break;

                playerCams[index].rect = new Rect(hor * x, 0.5f - (ver * y), hor, ver);
            }
        }
    }
}
