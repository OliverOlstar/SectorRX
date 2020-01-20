using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Programmer: Scott Watman
 Additional Programmer(s): Oliver Loescher, Kavian Kermani
 Description: Spawns number of players depending on how many joined at main menu. Incorporated parts of Random Spawn script by Kavian*/

public class PlayerSpawn : MonoBehaviour
{
    public Camera cinemaCam;
    public GameObject playerPrefab;
    public List<Transform> fourPlayerSpawns = new List<Transform>();
    public List<Transform> eightPlayerSpawns = new List<Transform>();
    private int _SpawnPointIndex;

    void Awake()
    {
        StartCoroutine("CameraSwitch");
    }

    //Waits three seconds then spawns players in one of the avaiable locations randomly (only one player per location).
    IEnumerator CameraSwitch()
    {
        //Wait for three seconds then turn off camera (simulates cinematic camera).
        yield return new WaitForSeconds(3.0f);
        cinemaCam.gameObject.SetActive(false);
        
        //Spawn number of connected players.
        Debug.Log("Spawning " + connectedPlayers.playersConnected + " players");
        for (int i = 0; i < connectedPlayers.playersConnected; i++)
        {
            //Checks if there are enough spawn points to use for all players.
            if (fourPlayerSpawns.Count <= 0 || eightPlayerSpawns.Count <= 0)
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
            }

            //If 5 or more players connected, access the list of 8 spawn points and randomly spawn players at those listed locations.
            else if (connectedPlayers.playersConnected >= 5)
            {
                _SpawnPointIndex = Random.Range(0, eightPlayerSpawns.Count);
                Transform _EightSpawnPos = eightPlayerSpawns[_SpawnPointIndex];
                eightPlayerSpawns.RemoveAt(_SpawnPointIndex);
                Instantiate(playerPrefab, _EightSpawnPos.position, _EightSpawnPos.rotation);
            }
        }
    }

    //For when other scripts need to reference.
    public void SpawnPlayer()
    {
        cinemaCam.gameObject.SetActive(false);

        Debug.Log("Spawning " + connectedPlayers.playersConnected + " players");
        for (int i = 0; i < connectedPlayers.playersConnected; i++)
        {
            if (fourPlayerSpawns.Count <= 0 || eightPlayerSpawns.Count <= 0)
            {
                return;
            }

            //If 4 or less players connected, access the list of 4 spawn points and randomly spawn players at those listed locations.
            if (connectedPlayers.playersConnected <= 4)
            {
                _SpawnPointIndex = Random.Range(0, fourPlayerSpawns.Count);
                Transform _FourSpawnPos = fourPlayerSpawns[_SpawnPointIndex];
                fourPlayerSpawns.RemoveAt(_SpawnPointIndex);
                Instantiate(playerPrefab, _FourSpawnPos.position, _FourSpawnPos.rotation);
            }

            //If 5 or more players connected, access the list of 8 spawn points and randomly spawn players at those listed locations.
            else if (connectedPlayers.playersConnected >= 5)
            {
                _SpawnPointIndex = Random.Range(0, eightPlayerSpawns.Count);
                Transform _EightSpawnPos = eightPlayerSpawns[_SpawnPointIndex];
                eightPlayerSpawns.RemoveAt(_SpawnPointIndex);
                Instantiate(playerPrefab, _EightSpawnPos.position, _EightSpawnPos.rotation);
            }
        }
    }
}