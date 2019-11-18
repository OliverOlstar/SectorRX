using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerHandler : MonoBehaviour
{
    private PlayerAttributes playerAttributes;

    public enum powers
    {
        MagmaErupter,
        Fireball
    }

    private List<IPower> _collectedPowers = new List<IPower>();

    private void Start()
    {
        playerAttributes = GetComponent<PlayerAttributes>();
    }

    public int UsingPower(int pPowerInput)
    {
        if (pPowerInput > 0 && pPowerInput <= _collectedPowers.Count)
        {
            if (playerAttributes.getPower() >= _collectedPowers[pPowerInput - 1].GetPowerRequired())
            {
                playerAttributes.modifyPower(-_collectedPowers[pPowerInput - 1].GetPowerRequired());
                _collectedPowers[pPowerInput - 1].UsingMe();
                return _collectedPowers[pPowerInput - 1].GetAnimIndex();
            }
            else
            {
                return -2;
            }
        }

        //If no power
        return -1;
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
