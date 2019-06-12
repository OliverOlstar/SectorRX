using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Master : MonoBehaviour
{
    private playerPowers _PlayerPowersComp;
    private int _SlotImIn;

    // Start is called before the first frame update
    void Start()
    {
        _PlayerPowersComp = GetComponent<playerPowers>();

        _SlotImIn = _PlayerPowersComp.PowerAdded();

        //Adding Subject
        switch (_SlotImIn)
        {
            case 1:
                playerPowers.OnPower1Used += UsingMe;
                break;

            case 2:
                playerPowers.OnPower2Used += UsingMe;
                break;

            case 3:
                playerPowers.OnPower3Used += UsingMe;
                break;

            case 4:
                playerPowers.OnPower4Used += UsingMe;
                break;
        }
    }

    private void OnDestroy()
    {
        //Remove Subject
        switch (_SlotImIn)
        {
            case 1:
                playerPowers.OnPower1Used -= UsingMe;
                break;

            case 2:
                playerPowers.OnPower2Used -= UsingMe;
                break;

            case 3:
                playerPowers.OnPower3Used -= UsingMe;
                break;

            case 4:
                playerPowers.OnPower4Used -= UsingMe;
                break;
        }
    }

    public virtual void UsingMe()
    {
        Debug.Log("POWER USED!");
    }
}
