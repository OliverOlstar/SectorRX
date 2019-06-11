using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Lightning : Power_Master
{
    public override void UsingMe()
    {
        base.UsingMe();

        Debug.Log("LIGHTNING!!!");
    }
}
