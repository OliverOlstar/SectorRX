using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Master : MonoBehaviour, IPower
{
    private playerPowers _PlayerPowersComp;

    // Start is called before the first frame update
    void Start()
    {
        _PlayerPowersComp = GetComponent<playerPowers>();
        _PlayerPowersComp.PowerAdded(this);
    }

    private void OnDestroy()
    {
        _PlayerPowersComp.PowerRemoved(this);
    }

    public void UsingMe()
    {
        Debug.Log("POWER USED!");
    }
}
