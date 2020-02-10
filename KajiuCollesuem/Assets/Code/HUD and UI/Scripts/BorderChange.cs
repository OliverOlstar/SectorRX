using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BorderChange : MonoBehaviour
{
    public Text[] playerLabel;

    public void SetBorderColor(Color p1Color, Color p2Color)
    {
        for(int i = 0; i < playerLabel.Length; i++)
        {
            playerLabel[1].gameObject.SetActive(false);
            playerLabel[2].gameObject.SetActive(false);
            playerLabel[3].gameObject.SetActive(false);
        }
    }
}