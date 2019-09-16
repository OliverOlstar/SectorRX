using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    // Danish's variables
    public float moveSpeed = 5.0f;
    public float searchZone = 10.0f;

    //Mugie's variables
    public float maxSpeed = 10; //AI run speed

    protected NavMeshAgent agent;
    float height = 0.155f;

    Vector3 startingPosition;

    // Dylan's Variables
    protected GameObject player; // The player character
    Vector3 playerLastKnownPosition; // Store last known position of player - used for search mode

    public bool isPatrolling; // if false enemy will stand guard
    public bool playerInSight = false; // True when player is in field of view. False otherwise
    bool searchingForPlayer = false; // True after player leaves field of vision. False 

    public float searchTimeMax = 60f; // How long enemy will search for player
    float searchTime; // Current time spend searching for player

    private enum EnemyState
    {
        Idle, Attack, Movement, Death, MechanicalPhase, OrganicPhase
    }
    private EnemyState state = EnemyState.Idle;

    // Start is called before the first frame update
    void Start()
    {
        InheritStart();  
    }

    protected void InheritStart()
    {
        GetComponent<EnemyAttributes>().enemyHealthUI.SetActive(false);

        List<GameObject> patrolPoints = new List<GameObject>();

        agent = gameObject.GetComponent<NavMeshAgent>(); // connect to the NavMeshAgent on the enemy
        agent.speed = moveSpeed;

        player = GameObject.FindGameObjectWithTag("Player");

        startingPosition = transform.position;

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
        InheritUpdate();
    }

    protected void InheritUpdate()
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
                    OnPatrol();
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
                Debug.Log(searchTime);
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
    
    void OnPatrol()
    {

        
    }

    void OnGuard()
    {
        
        //Vector3 mover = new Vector3(0, 0, moveSpeed * Time.deltaTime);

        //Calculates distance between enemy and player, and runs towards the player
        if(Vector3.Distance(transform.position, player.transform.position) <= searchZone)
        {
            if (agent.isActiveAndEnabled)
            {
                agent.destination = player.transform.position;
                agent.speed = maxSpeed;
            }
            GetComponent<EnemyAttributes>().enemyHealthUI.SetActive(true);
        }
        else
        {
            GetComponent<EnemyAttributes>().StartCoroutine("HealthVanish");

            if (agent.isActiveAndEnabled)
            {
                agent.destination = startingPosition;
                agent.speed = moveSpeed;
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        // If player enters enemy field of view, attack
        if (c.tag == "Player") playerInSight = true;

        // If player enters enemy field view, stop patrolling
        //isPatrolling = false;
    }

    void OnTriggerExit(Collider c)
    {
        // If player leaves enemy field of view, stop attacking and start searching
        if (c.tag == "Player") playerInSight = false;
        searchTime = searchTimeMax;

        // Store players position as they leave - used for searching.
        playerLastKnownPosition = player.transform.position;

        //If player leaves enemy field view, start patrolling
        //isPatrolling = true;
    }

    public NavMeshAgent GetAgent()
    {
        return agent;
    }
}
