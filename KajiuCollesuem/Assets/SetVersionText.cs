using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVersionText : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Text>().text = "Pre-Alpha 0.2.5";
    }
}
