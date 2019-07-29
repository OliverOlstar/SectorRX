using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class playerPowers : MonoBehaviour
{
    public enum powers { MagmaErupter, Fireball }

    private List<IPower> _collectedPowers = new List<IPower>();

    public void UsingPower(int pPowerInput)
    {
        if (pPowerInput > 0 && pPowerInput <= _collectedPowers.Count)
            _collectedPowers[pPowerInput -1].UsingMe();
    }

    public void AddPower(int pWhichPower)
    {
        switch (pWhichPower)
        {
            case (int)powers.Fireball:
                gameObject.AddComponent<Power_Fireball>();
                break;
                
            case (int)powers.MagmaErupter:
                gameObject.AddComponent<Power_Lightning>();
                break;
        }
    }

    public void PowerAdded(IPower pPower)
    {
        _collectedPowers.Add(pPower);
    }

    public void PowerRemoved(IPower pPower)
    {
        if (_collectedPowers.Contains(pPower))
            _collectedPowers.Remove(pPower);
    }
}
