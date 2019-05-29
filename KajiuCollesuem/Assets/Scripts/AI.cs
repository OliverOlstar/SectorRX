using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AI : MonoBehaviour
{
    // Danish's variables
    public float moveSpeed = 5.0f;
    public float searchZone = 10.0f;

    Rigidbody rb;

    // Dylan's Variables
    private GameObject player; // The player character
    Vector3 playerLastKnownPosition; // Store last known position of player - used for search mode

    public bool isPatrolling; // if false enemy will stand guard
    bool playerInSight = false; // True when player is in field of view. False otherwise
    bool searchingForPlayer = false; // True after player leaves field of vision. False 

    public float searchTimeMax = 60f; // How long enemy will search for player
    float searchTime; // Current time spend searching for player

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<EnemyAttributes>().enemyHealthUI.SetActive(false);

        rb = gameObject.GetComponent<Rigidbody>(); // connect to the Rigidbody on the enemy

        player = GameObject.FindGameObjectWithTag("Player");

        if (searchTimeMax <= 0)
        {
            Debug.Log("Search time not set on " + this + ". Defaulting to 60.");
            searchTimeMax = 60f;
        }
        if (!player) Debug.Log("player not set on " + this + ". Script relies on player gameobject to function.");         
    }

    // Update is called once per frame
    void Update()
    {
        // When player is not in sight
        if (!playerInSight)
        {
            // If enemy is not searching for player
            if (searchTime <= 0)
            {
                // Enemy is patrolling
                if (isPatrolling)
                {

                }
                // Enemy is guarding
                else
                {
                    OnGuard();

                }
            }

            // Enemy is searching for player
            else
            {
                searchTime -= 0.01f;
            }
        }


        // When player is in sight
        else
        {
            



        }


        // Enable / Disable search mode
        if (searchTime > 0) searchingForPlayer = true;
        else searchingForPlayer = false;
    }

    void OnGuard()
    {
        
        Vector3 mover = new Vector3(0, 0, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, player.transform.position) <= searchZone)
        {
            transform.LookAt(player.transform.position);
            transform.Translate(mover, Space.Self);
            GetComponent<EnemyAttributes>().enemyHealthUI.SetActive(true);
        }
        else
        {
            GetComponent<EnemyAttributes>().StartCoroutine("HealthVanish");
        }
    }

    void OnTriggerEnter(Collider c)
    {
        // If player enters enemy field of view, attack
        if (c.tag == "Player") playerInSight = true;
    }

    void OnTriggerExit(Collider c)
    {
        // If player leaves enemy field of view, stop attacking and start searching
        if (c.tag == "Player") playerInSight = false;
        searchTime = searchTimeMax;

        // Store players position as they leave - used for searching.
        playerLastKnownPosition = player.transform.position;
    }
}
