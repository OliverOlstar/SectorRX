using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Programmer: Scott Watman
 Description: Edits player's stats depending on which collectible they pick up*/

public class PlayerCollectibles : MonoBehaviour
{
    public enum Upgrades
    {
        Health,
        Shield,
        Power,
        Speed,
        Attack,
        Weight,
        Jump
    }

    public void CollectedItem(Upgrades pStat)
    {
        switch (pStat)
        {
            case Upgrades.Health:

                break;

            case Upgrades.Shield:

                break;

            case Upgrades.Power:

                break;

            case Upgrades.Speed:

                break;

            case Upgrades.Attack:
                Debug.Log("Raising Attack");
                break;

            case Upgrades.Weight:

                break;

            case Upgrades.Jump:

                break;
        }
    }
}
