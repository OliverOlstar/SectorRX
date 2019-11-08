using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /*
 Saves:

 Powers Unlocked
 Player Upgrades
 Current Player Spawnpoint
 Which Cells have already been collected


 Loads:

 Everything in save and
 Respawns all enemies
    */

public class SavingLoading : MonoBehaviour
{
    public void Save(int pSlot = 0)
    {


        PlayerPrefs.Save();
    }

    public void LoadSave(int pSlot = 0)
    {

    }
}
