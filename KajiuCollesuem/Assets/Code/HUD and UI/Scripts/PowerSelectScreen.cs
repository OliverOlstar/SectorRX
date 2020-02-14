using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerSelectScreen : MonoBehaviour
{
    //Variables
    private PlayerAbilitySelector playerPowerHandler;
    public Text powerDescription;

    //Power Array
    [SerializeField] private List<SOAbilities> powers;

    //Power Randomize
    public void RandomPower()
    {

    }

    //Power Selection
    public void SelectPower(int i)
    {
        //playerPowerHandler.AddPower(powers[i]);
        powers.Remove(powers[i]);
    }

    //Power Select Description Management
    public void ChangeDescription()
    {
        powerDescription.text = powers[0].dislayDescription;
    }
}
