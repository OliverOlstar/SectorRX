using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Lightning : Power_Master, IPower
{
    new public void UsingMe()
    {
        Debug.Log(vars.damage + "LIGHTNING!!!");
    }
}
