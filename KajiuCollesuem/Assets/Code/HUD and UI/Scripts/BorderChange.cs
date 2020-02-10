using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BorderChange : MonoBehaviour
{
    public Image[] borders;

    public void SetBoarderColor(Color pColor)
    { 
        foreach(Image board in borders)
        {
            board.color = pColor;
        }
    }
}