using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

/*Programmer: Scott Watman
 Additional Programmer(s): Oliver Loescher, Kavian Kermani
 Description: Spawns number of players depending on how many joined at main menu. Incorporated parts of Random Spawn script by Kavian
              Can also restart a finished game with same amound of players.*/

public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefab;
    [HideInInspector] public List<GameObject> players = new List<GameObject>();

    [HideInInspector] public List<Transform> playerSpawns = new List<Transform>();
    private int _SpawnPointIndex;
    [SerializeField] private MatchInputHandler[] _PlayerInputs = new MatchInputHandler[6];
    private List<MatchInputHandler> _ActiveInputs = new List<MatchInputHandler>();

    private int playersToSpawn = 0;

    public void MatchStartup()
    {
        //If no players are entered, automatically set to 2.
        if (connectedPlayers.playersConnected <= 0)
        {
            connectedPlayers.playersConnected = 2;
            
            connectedPlayers.playerIndex.Clear();
            UsedDevices player = new UsedDevices();

            for (int i = 0; i < connectedPlayers.playersConnected; i++)
            {
                player.deviceUser = i;
                player.playerIndex = i;
                player.playerColorSet = new ColorSet();
                player.abilitySelected = Mathf.RoundToInt(Random.value * 2);

                connectedPlayers.playerIndex.Add(player);
            }
        }

        playersToSpawn = connectedPlayers.playersConnected;
    }

    // Return true if match is still going
    public int PlayerDied()
    {
        playersToSpawn--;
        return playersToSpawn;
    }

    public void InputSetup()
    {
        // Repeat this for every player that needs to be spawned
        for (int i = 0; i < playersToSpawn; i++)
        {
            // Check each connected players device index from Main Menu scene
            for (int j = 0; j < connectedPlayers.playerIndex.Count; j++)
            {
                // Set devices in Game Scene as same order as Main Menu scene
                if (connectedPlayers.playerIndex[j].playerIndex == i)
                {
                    // Check all devices for matching device
                    foreach (MatchInputHandler input in _PlayerInputs)
                    {
                        if (input.GetComponent<PlayerInput>().user.index == connectedPlayers.playerIndex[j].deviceUser)
                        {
                            _ActiveInputs.Add(input);
                            break;
                        }
                    }
                    break;
                }
            }
        }
    }

    //Waits three seconds then spawns players in one of the avaiable locations randomly (only one player per location).
    public void SpawnAllPlayers()
    {
        //Spawn number of connected players.
        Debug.Log("Spawning " + playersToSpawn + " players");

        if (playerSpawns.Count < playersToSpawn)
        {
            Debug.Log("Not enough spawnpoints available");
            return;
        }

        InputSetup();

        // Randomly spawn players at listed locations.
        SpawnPlayers();

        DisableUnusedDevices();
    }

    private void SpawnPlayers()
    {
        for (int i = 0; i < playersToSpawn; i++)
        {
            _SpawnPointIndex = Random.Range(0, (playersToSpawn <= 4 ? 4 : 8) - i);
            Transform _EightSpawnPos = playerSpawns[_SpawnPointIndex];
            playerSpawns.RemoveAt(_SpawnPointIndex);
            GameObject playerCharacter = Instantiate(playerPrefab, _EightSpawnPos.position, transform.rotation);
            //playerCharacter.transform.LookAt(Vector3.zero);
            //playerCharacter.transform.rotation = playerCharacter.transform.rotation * Quaternion.Euler(0, 90, 0);
            players.Add(playerCharacter);

            //Taking list of joined players and setting them to their correct device, with inputs enabled
            _ActiveInputs[i].playerStateController = playerCharacter.GetComponentInChildren<PlayerStateController>();

            // Set Color
            ColorSetter colorSetter = playerCharacter.GetComponent<ColorSetter>();
            colorSetter.SetColor(connectedPlayers.playerIndex[i].playerColorSet);
            colorSetter.SetAbility(connectedPlayers.playerIndex[i].abilitySelected);
        }
    }

    private void DisableUnusedDevices()
    {
        //Disable InputHandlers that aren't connected to a player
        foreach (MatchInputHandler input in _PlayerInputs)
        {
            if (input.playerStateController == null)
                input.gameObject.SetActive(false);
        }
    }
}