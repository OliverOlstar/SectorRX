using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Lightning : Power_Master, IPower
{
    void Start()
    {
        requiredPower = 6;
        damage = 2;
    }

    new public void UsingMe()
    {
        Debug.Log("LIGHTNING!!!");
    }
}
