using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerHandler : MonoBehaviour
{
    private PlayerAttributes playerAttributes;

    //List of all powers to help with entering the desired power to be added
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

    //Attack state calls this function
    public int UsingPower(int pPowerInput)
    {
        //If power specific button exists
        if (pPowerInput > 0 && pPowerInput <= _collectedPowers.Count)
        {
            //If required power
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
        //Add power component based off of input
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
