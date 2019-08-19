using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_Template : Power_Master, IPower
{
    //Select SOPower before play time. When script is selected option to select shows in inspector.
    
    new public void UsingMe()
    {
        Debug.Log("Template Power Used");
    }
}
