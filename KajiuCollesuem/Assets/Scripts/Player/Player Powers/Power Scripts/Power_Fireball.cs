using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Fireball : Power_Master, IPower
{
    void Start()
    {
        requiredPower = 10;
        damage = 6;
    }

    new public void UsingMe()
    {
        Debug.Log("FIREBALL!!!");
    }
}
