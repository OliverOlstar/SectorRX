using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    // Danish's variables
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float searchZone = 10.0f;
    Rigidbody rb;
    [SerializeField] Transform EnemyTransform;
    [SerializeField] Transform raycastPoint;
    RaycastHit hit;

    Vector3 rotator = new Vector3(0, 1);

    // Dylan's Variables
    public GameObject player; // The player character
    Vector3 playerLastKnownPosition; // Store last known position of player - used for search mode

    public bool isPatrolling; // if false enemy will stand guard
    bool playerInSight = false; // True when player is in field of view. False otherwise
    bool searchingForPlayer = false; // True after player leaves field of vision. False 

    public float searchTimeMax = 60f; // How long enemy will search for player
    float searchTime; // Current time spend searching for player

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>(); // connect to the Rigidbody on the enemy


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
        RaycastHit hit;
        Vector3 mover = new Vector3(0, 0, moveSpeed * Time.deltaTime);

        Debug.DrawRay(raycastPoint.position, raycastPoint.TransformDirection(Vector3.forward) * searchZone, Color.red);

        if (Physics.Raycast(raycastPoint.position, raycastPoint.TransformDirection(Vector3.forward), out hit, searchZone))
        {
            Debug.Log("HIT");
            //player.TransformDirection(Vector3.forward * moveSpeed);
            //rb.AddForce(mover * moveSpeed);
            EnemyTransform.Translate(mover, Space.Self);
        }
        else
        {
            EnemyTransform.Rotate(rotator);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // TODO change to something more applicable for the game
            collision.gameObject.SetActive(false);
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
