using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Master : MonoBehaviour, IPower
{
    private PlayerPowerHandler _PlayerPowersComp;

    protected int requiredPower = 0;
    protected int damage = 0;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Start");
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
