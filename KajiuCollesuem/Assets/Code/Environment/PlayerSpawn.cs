using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/*Programmer: Scott Watman
 Additional Programmer(s): Oliver Loescher, Kavian Kermani
 Description: Spawns number of players depending on how many joined at main menu. Incorporated parts of Random Spawn script by Kavian
              Can also restart a finished game with same amound of players.*/

public class PlayerSpawn : MonoBehaviour
{
    public Camera cinemaCam;
    public GameObject playerPrefab;

    public MusicManager musicManager;
    public List<Transform> fourPlayerSpawns = new List<Transform>();
    public List<Transform> ninePlayerSpawns = new List<Transform>();
    private int _SpawnPointIndex;

    public void MatchStartup()
    {
        StartCoroutine("CameraSwitch");

        //If no players are entered, automatically set to 1.
        if (connectedPlayers.playersToSpawn <= 0)
        {
            connectedPlayers.playersToSpawn = 2;
            connectedPlayers.playersConnected = 2;
        }

        //Sets number of connected players equal to how many need to be spawned. Helps with match restarts after a player wins.
        if (connectedPlayers.playersToSpawn <= 1)
        {
            connectedPlayers.playersConnected = 1;
        }
        else if (connectedPlayers.playersToSpawn == 2)
        {
            connectedPlayers.playersConnected = 2;
        }
        else if (connectedPlayers.playersToSpawn == 3)
        {
            connectedPlayers.playersConnected = 3;
        }
    }

    public void MatchEnd()
    {
        //Debug.Log(connectedPlayers.playersToSpawn);
        //Debug.Log(connectedPlayers.playersConnected);

        if (connectedPlayers.playersConnected <= 1)
        {
            StartCoroutine("VictoryReset");
        }
    }

    //Waits three seconds then spawns players in one of the avaiable locations randomly (only one player per location).
    IEnumerator CameraSwitch()
    {
        //Wait for three seconds then turn off camera (simulates cinematic camera).
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(3.0f);
        cinemaCam.gameObject.SetActive(false); 

        //Spawn number of connected players.
        Debug.Log("Spawning " + connectedPlayers.playersToSpawn + " players");
        for (int i = 0; i < connectedPlayers.playersToSpawn; i++)
        {
            //Checks if there are enough spawn points to use for all players.
            if (fourPlayerSpawns.Count <= 0 || ninePlayerSpawns.Count <= 0)
            {
                yield return false;
            }

            //If 4 or less players connected, access the list of 4 spawn points and randomly spawn players at those listed locations.
            if (connectedPlayers.playersConnected <= 4)
            {                
                _SpawnPointIndex = Random.Range(0, fourPlayerSpawns.Count);
                Transform _FourSpawnPos = fourPlayerSpawns[_SpawnPointIndex];
                fourPlayerSpawns.RemoveAt(_SpawnPointIndex);
                Instantiate(playerPrefab, _FourSpawnPos.position, _FourSpawnPos.rotation);
                //musicManager.battleMusic[0].Play();
            }

            //If 5 or more players connected, access the list of 8 spawn points and randomly spawn players at those listed locations.
            else if (connectedPlayers.playersConnected >= 5)
            {
                _SpawnPointIndex = Random.Range(0, ninePlayerSpawns.Count);
                Transform _EightSpawnPos = ninePlayerSpawns[_SpawnPointIndex];
                ninePlayerSpawns.RemoveAt(_SpawnPointIndex);
                Instantiate(playerPrefab, _EightSpawnPos.position, _EightSpawnPos.rotation);
                //musicManager.battleMusic[0].Play();
            }
        }
    }

    //For when other scripts need to reference.
    //public void SpawnPlayer()
    //{
    //    cinemaCam.gameObject.SetActive(false);

    //    Debug.Log("Spawning " + connectedPlayers.playersConnected + " players");
    //    for (int i = 0; i < connectedPlayers.playersConnected; i++)
    //    {
    //        if (fourPlayerSpawns.Count <= 0 || ninePlayerSpawns.Count <= 0)
    //        {
    //            return;
    //        }

    //        //If 4 or less players connected, access the list of 4 spawn points and randomly spawn players at those listed locations.
    //        if (connectedPlayers.playersConnected <= 4)
    //        {
    //            _SpawnPointIndex = Random.Range(0, fourPlayerSpawns.Count);
    //            Transform _FourSpawnPos = fourPlayerSpawns[_SpawnPointIndex];
    //            fourPlayerSpawns.RemoveAt(_SpawnPointIndex);
    //            Instantiate(playerPrefab, _FourSpawnPos.position, _FourSpawnPos.rotation);
    //        }

    //        //If 5 or more players connected, access the list of 8 spawn points and randomly spawn players at those listed locations.
    //        else if (connectedPlayers.playersConnected >= 5)
    //        {
    //            _SpawnPointIndex = Random.Range(0, ninePlayerSpawns.Count);
    //            Transform _EightSpawnPos = ninePlayerSpawns[_SpawnPointIndex];
    //            ninePlayerSpawns.RemoveAt(_SpawnPointIndex);
    //            Instantiate(playerPrefab, _EightSpawnPos.position, _EightSpawnPos.rotation);
    //        }
    //    }
    //}

    IEnumerator VictoryReset()
    {
        Debug.Log("I REMOVED THIS");

        yield return new WaitForSeconds(4.0f);
        
        if (connectedPlayers.playersToSpawn > 1)
        {
            musicManager.mainAudio.Stop();
            SceneManager.LoadSceneAsync(2);
        }
    }
}