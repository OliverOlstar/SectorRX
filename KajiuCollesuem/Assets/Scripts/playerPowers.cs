using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class playerPowers : MonoBehaviour
{
    public static event Action OnPower1Used;
    public static event Action OnPower2Used;
    public static event Action OnPower3Used;
    public static event Action OnPower4Used;
    public int _currentPower = 0;
    public int _collectedPowers = 0;

    [SerializeField]
    private void UsingPower()
    {
        switch (_currentPower)
        {
            case 1:
                if (OnPower1Used != null)
                    OnPower1Used();

                break;


            case 2:
                if (OnPower2Used != null)
                    OnPower2Used();

                break;


            case 3:
                if (OnPower3Used != null)
                    OnPower3Used();

                break;


            case 4:
                if (OnPower4Used != null)
                    OnPower4Used();

                break;
        }
    }
    
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            UsingPower();
        }
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
