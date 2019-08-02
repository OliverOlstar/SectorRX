using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Template : Power_Master, IPower
{
    void Start()
    {
        requiredPower = 1;
        damage = 1;
    }

    new public void UsingMe()
    {
        Debug.Log("Template Power Used");
    }
}
