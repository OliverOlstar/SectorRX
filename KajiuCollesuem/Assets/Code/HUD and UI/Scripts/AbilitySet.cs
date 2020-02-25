using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySet : MonoBehaviour
{
    public Sprite[] abilityIcons;
    public Sprite[] abilityIconsTwo;
    public Image[] ability = new Image[2];
    public Image[] abilityTwo = new Image[2];
    public bool setOne;
    public bool setTwo;

    private void Start()
    {
        
    }

    public void AbilitySetTwo()
    {
        
    }

    public void AbilitySetOne()
    {
        if (setTwo)
        {
            ability[0].GetComponent<Image>().sprite = abilityIcons[3];
            ability[1].GetComponent<Image>().sprite = abilityIcons[2];
            setOne = true;
            setTwo = false;
        }
    }
}