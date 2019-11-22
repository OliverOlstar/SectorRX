using System;
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

    Time played

    Load:

    Everything in save and
    Respawn all enemies
    Respawn Player

 */

public class SaveAndLoad : MonoBehaviour
{
    public static event Action<SaveAndLoad> RespawnEnemies;
    public static SaveAndLoad _Instance;

    private void Start()
    {
        if (_Instance != null) 
            Destroy(gameObject);
        else
            _Instance = this;
    }

    [SerializeField] private PlayerUpgrades playerUpgrades;
    [SerializeField] private PlayerRespawn playerRespawn;
    [SerializeField] private PlayerAttributes playerAttributes;

    public void ClearSave(int pSlot = 0)
    {
        Debug.Log("SaveAndLoad: <color=Orange>ClearSave</color>");
        PlayerPrefs.SetInt("Save Slot Taken", 0);
    }

    public void Save(int pSlot = 0)
    {
        Debug.Log("SaveAndLoad: <color=Orange>Save</color>");
        PlayerPrefs.SetInt("Save Slot Taken", 1);
        SaveLevels(pSlot);
        SaveSpawnTransform(pSlot);
        PlayerPrefs.Save();
    }

    public bool LoadSave(int pSlot = 0)
    {
        Debug.Log("SaveAndLoad: <color=Orange>LoadSave</color>");
        //If Slot has not save don't load
        if (PlayerPrefs.GetInt("Save Slot Taken") == 0) return false;

        LoadLevels();
        LoadSpawnTransform(pSlot);
        QuickLoadSave(pSlot);
        return true;
    }

    public void QuickLoadSave(int pSlot = 0)
    {
        Debug.Log("SaveAndLoad: <color=Orange>QuickLoadSave</color>");
        //Loads Save after dieing. Only resets enemies and player.
        playerRespawn.Respawn();
        playerAttributes.Respawn();
        RespawnEnemies(null);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            Save(0);
        }

        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            LoadSave(0);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            QuickLoadSave(0);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            playerUpgrades.LevelUp(0, 1);
            playerUpgrades.LevelUp(1, 1);
        }
    }

    //Player Position
    private void SaveSpawnTransform(int pSlot = 0)
    {
        Vector3 respawnPoint = playerRespawn.getRespawnPoint();
        PlayerPrefs.SetFloat(pSlot + "-SpawnPos-x", respawnPoint.x);
        PlayerPrefs.SetFloat(pSlot + "-SpawnPos-y", respawnPoint.y);
        PlayerPrefs.SetFloat(pSlot + "-SpawnPos-z", respawnPoint.z);

        float respawnRotationY = playerRespawn.getRespawnRotation();
        PlayerPrefs.SetFloat(pSlot + "-SpawnRot-y", respawnRotationY);
    }

    private void LoadSpawnTransform(int pSlot = 0)
    {
        Vector3 respawnPoint = new Vector3(
            PlayerPrefs.GetFloat(pSlot + "-SpawnPos-x"),
            PlayerPrefs.GetFloat(pSlot + "-SpawnPos-y"),
            PlayerPrefs.GetFloat(pSlot + "-SpawnPos-z"));

        float respawnRotation =
            PlayerPrefs.GetFloat(pSlot + "-SpawnRot-y");

        playerRespawn.setRespawnTransform(respawnPoint, respawnRotation);
    }

    //Levels
    private void SaveLevels(int pSlot = 0)
    {
        int[] levels = playerUpgrades.GetStatLevels();
        for (int i = 0; i < levels.Length; i++)
            PlayerPrefs.SetInt(pSlot + "-Stats-" + i, levels[i]);

        levels = playerUpgrades.GetPowerLevels();
        for (int i = 0; i < levels.Length; i++)
            PlayerPrefs.SetInt(pSlot + "-Powers-" + i, levels[i]);
    }

    private void LoadLevels(int pSlot = 0)
    {
        //Clear Current Levels
        playerUpgrades.Setup();

        //Levelup to match save stats
        for (int i = 0; i < playerUpgrades.GetStatLevels().Length; i++)
        {
            for (int z = 1; z < PlayerPrefs.GetInt(pSlot + "-Stats-" + i); z++)
            {
                playerUpgrades.LevelUp(i, z);
            }
        }

        for (int i = 0; i < playerUpgrades.GetPowerLevels().Length; i++)
        {
            for (int z = 1; z < PlayerPrefs.GetInt(pSlot + "-Powers-" + i); z++)
            {
                playerUpgrades.PowerUpgrade(i, z);
            }
        }
    }
}
