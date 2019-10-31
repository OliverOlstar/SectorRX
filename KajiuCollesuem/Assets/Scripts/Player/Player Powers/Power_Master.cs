using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Master : MonoBehaviour, IPower
{
    private PlayerPowerHandler _PlayerPowersComp;

    /* Select SOPower before play time. When script is selected option to select shows in inspector. */
    [SerializeField] protected SOPowers vars;

    public int GetAnimIndex() => vars.animationIndex;
    public int GetPowerRequired() => vars.powerRequired;

    void Awake()
    {
        //Add myself to the power script
        _PlayerPowersComp = GetComponent<PlayerPowerHandler>();
        _PlayerPowersComp.AddedPower(this);
    }

    private void OnDestroy()
    {
        //Remove myself from the power script
        if (_PlayerPowersComp)
            _PlayerPowersComp.PowerRemoved(this);
    }

    public void UsingMe()
    {
        //Function that gets overridden
        Debug.Log("POWER USED!");
    }
}
