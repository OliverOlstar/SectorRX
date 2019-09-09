using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Master : MonoBehaviour, IPower
{
    private PlayerPowerHandler _PlayerPowersComp;

    /* Select SOPower before play time. When script is selected option to select shows in inspector. */
    [SerializeField] protected SOPowers vars;
    
    void Awake()
    {
        _PlayerPowersComp = GetComponent<PlayerPowerHandler>();
        _PlayerPowersComp.AddedPower(this);
    }

    private void OnDestroy()
    {
        if (_PlayerPowersComp)
            _PlayerPowersComp.PowerRemoved(this);
    }

    public void UsingMe()
    {
        Debug.Log("POWER USED!");
    }
}
