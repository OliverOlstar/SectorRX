using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySet : MonoBehaviour
{
    public Sprite[] abilityIcons;
    public Image[] ability = new Image[2];
    public bool setOne;
    public bool setTwo;

    private void Start()
    {
        ability[0].GetComponent<Image>().sprite = abilityIcons[3];
        ability[1].GetComponent<Image>().sprite = abilityIcons[2];
    }

    public void AbilitySetTwo()
    {
        if(setOne)
        {
            ability[0].GetComponent<Image>().sprite = abilityIcons[0];
            ability[1].GetComponent<Image>().sprite = abilityIcons[1];
            setOne = false;
            setTwo = true;
        }
    }

    public void AbilitySetOne()
    {
        if (setOne)
        {
            ability[0].GetComponent<Image>().sprite = abilityIcons[3];
            ability[1].GetComponent<Image>().sprite = abilityIcons[2];
            setOne = true;
            setTwo = false;
        }
    }
}