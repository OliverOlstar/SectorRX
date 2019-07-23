using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class playerPowers : MonoBehaviour
{
    public enum powers { MagmaErupter,  }

    public static event Action OnPower1Used;
    public static event Action OnPower2Used;
    public static event Action OnPower3Used;

    public bool power1Input;
    public bool power2Input;
    public bool power3Input;

    public int _currentPower = 1;
    public int _collectedPowers = 0;

    [SerializeField]
    private void UsingPower()
    {
        if (power1Input)
        {
            if (OnPower1Used != null)
                OnPower1Used();
        }
        else if (power2Input)
        {
            if (OnPower2Used != null)
                OnPower2Used();
        }
        else if (power3Input)
        {
            if (OnPower3Used != null)
                OnPower3Used();
        }
    }

    void Start()
    {

    }

    void Update()
    {
        UsingPower();
    }

    public void AddPower(string pString)
    {
        if (pString == "Fireball") {
            gameObject.AddComponent<Power_Fireball>();
        } else if (pString == "Lightning") {
            gameObject.AddComponent<Power_Lightning>();
        } else if (pString == "......") {
            //gameObject.AddComponent<......>();
        }
    }

    public int PowerAdded()
    {
        _collectedPowers++;
        Debug.Log("Power has been obtained; you have " + _collectedPowers + " total!");

        return _collectedPowers;
    }
}
