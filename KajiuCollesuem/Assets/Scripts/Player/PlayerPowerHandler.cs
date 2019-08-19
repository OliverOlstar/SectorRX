using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerHandler : MonoBehaviour
{
    public enum powers
    {
        MagmaErupter,
        Fireball
    }

    private List<IPower> _collectedPowers = new List<IPower>();

    public bool UsingPower(int pPowerInput)
    {
        if (pPowerInput > 0 && pPowerInput <= _collectedPowers.Count)
        {
            _collectedPowers[pPowerInput - 1].UsingMe();
            return false;
        }

        //If no power
        return true;
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

    public void AddedPower(IPower pPower)
    {
        Debug.Log("Power Added");
        _collectedPowers.Add(pPower);
    }

    public void PowerRemoved(IPower pPower)
    {
        Debug.Log("Power Removed");
        if (_collectedPowers.Contains(pPower))
            _collectedPowers.Remove(pPower);
    }
}
