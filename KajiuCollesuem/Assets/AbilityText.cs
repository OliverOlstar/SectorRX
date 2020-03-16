using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityText : MonoBehaviour
{
    private Text myText;

    void Awake()
    {
        myText = GetComponent<Text>();
    }

    public void SetText(int pValue, int pCost)
    {
        int count = Mathf.FloorToInt(pValue / pCost);

        if (count > 0)
            myText.text = count.ToString();
        else
            myText.text = "";
    }
}
