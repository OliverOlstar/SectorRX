using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Oliver
    Save:

    Powers Player Has
    Player Upgrades
    Cells Collected
    Current Checkpoint


    Load:

    Everything in save and
    Respawn all enemies

 */

public class SaveAndLoad : MonoBehaviour
{
    public void Save(int pSlot = 0)
    {


        PlayerPrefs.Save();
    }

    public void LoadSave(int pSlot = 0)
    {

    }

    public void RespawnEnemies()
    {

    }
}
