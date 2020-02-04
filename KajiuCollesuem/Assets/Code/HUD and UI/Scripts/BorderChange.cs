using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BorderChange : MonoBehaviour
{
    public Image[] borders;

    public void SetBorderColor(Color pColor)
    {
        foreach(Image b in borders)
        {
            b.color = pColor;
        }
    }
}