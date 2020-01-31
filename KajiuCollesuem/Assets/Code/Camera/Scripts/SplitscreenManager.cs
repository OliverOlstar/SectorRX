using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Programmer: Scott Watman
 Description: Changes Splitscreen between Horizontal and Vertical*/

public class SplitscreenManager : MonoBehaviour
{
    public Camera[] playerCam;

    public bool isHorizontalSplit;

    // Start is called before the first frame update
    void Start()
    {
        playerCam = GameObject.FindObjectsOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //If the 'O' Key is pressed, camera values change which changes splitscreen style.
        if(Input.GetKeyDown(KeyCode.O))
        {
            isHorizontalSplit = !isHorizontalSplit;
            
            SetSplitScreen();
        }
    }

    public void SetSplitScreen()
    {
        if(isHorizontalSplit)
        {
            //Player 1 Camera settings when Horizontal.
            playerCam[0].rect = new Rect(0.0f, 0.0f, 1.0f, 0.5f);
            playerCam[0].fieldOfView = 35;

            //Player 2 Camera settings when Horizontal.
            playerCam[1].rect = new Rect(0.0f, 0.5f, 1.0f, 0.5f);
            playerCam[1].fieldOfView = 35;
        }
        else
        {
            //Player 1 Camera settings when Horizontal.
            playerCam[0].rect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
            playerCam[0].fieldOfView = 55;

            //Player 2 Camera settings when Horizontal.
            playerCam[1].rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
            playerCam[1].fieldOfView = 55;
        }
    }
}
