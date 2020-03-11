using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryLizzy : MonoBehaviour
{
    private ColorSetter _ColorSetter;

    private void Awake()
    {
        _ColorSetter = GetComponent<ColorSetter>();
    }

    public void SetLizzy(UsedDevices pPlayer)
    {
        _ColorSetter.SetColor(pPlayer.playerColorSet);
        _ColorSetter.SetAbility(pPlayer.abilitySelected);
    }
}
