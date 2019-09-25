using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfieMovement : MonoBehaviour
{
    public Transform player;
    static Animator anim;

    [SerializeField]
    private float _fireballDuration;
    public float fireballCooldown = 1.0f;
    public float newFireballTime = 0.0f;

    public Rigidbody fireballPrefab;
    public Transform FBSpawnpoint;

    //Mugie's vars
    NavMeshAgent agent;
    DirectedGraph enemyPatrol = new DirectedGraph();
    public GameObject patrolPath;
    GameObject currentPatrolDest;
    int attackDecision = 0;
    public int enemySpeed;

    private enum WolfieState
    {
        Idle, SeePlayerInRange, CloseToPlayer
    }

    private WolfieState state = WolfieState.Idle;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        for (int i = 0; i < patrolPath.transform.childCount; ++i)
        {
            enemyPatrol.AddNode(patrolPath.transform.GetChild(i).gameObject);
        }

        enemyPatrol.AddEdge(patrolPath.transform.GetChild(3).gameObject, patrolPath.transform.GetChild(0).gameObject);

        for (int i = 0; i < 3; ++i)
        {
            enemyPatrol.AddEdge(patrolPath.transform.GetChild(i).gameObject, patrolPath.transform.GetChild(i + 1).gameObject);
        }
        currentPatrolDest = enemyPatrol.GetNodes()[0].GetData();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - this.transform.position;
        direction.y = 0;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
            Quaternion.LookRotation(direction), 0.1f);

        switch (state)
        {
            case WolfieState.Idle:
                Patrol();
                break;
            case WolfieState.SeePlayerInRange:
                ShootFireball();
                break;
            case WolfieState.CloseToPlayer:
                AttackPlayer(direction);
                break;
        }

        /*anim.SetBool("FiringRange", false);
        Vector3 direction = player.position - this.transform.position;
        direction.y = 0;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
            Quaternion.LookRotation(direction), 0.1f);

        if (direction.magnitude < 10.0f)
        {
            this.transform.Translate(0, 0, 0.1f);
            anim.SetBool("FiringRange", true);

            if (Vector3.Distance(player.position, this.transform.position) < 2)
            {
                anim.SetBool("TargetCloseRange", true);
            }
            // anim.SetBool("isPunching", false);
        }
    }


        if (Vector3.Distance(player.position, this.transform.position) < 15.0f)
        {
            

            if (Time.time > newFireballTime)
            {
                if (direction.magnitude < 10.0f)
                {
                    this.transform.Translate(0, 0, 0.1f);
                    anim.SetBool("FiringRange", true);

                    if (Vector3.Distance(player.position, this.transform.position) < 2)
                    {
                        anim.SetBool("TargetCloseRange", true);
                    }
                    // anim.SetBool("isPunching", false);
                }

                else
                {
                    // anim.SetBool("isPunching", true);
                    StartCoroutine(fireCast());
                }
            }

        }
        else
        {
            /*agent.isStopped = false;
            anim.SetBool("FiringRange", true);
            /*StopCoroutine(fireCast());

            Rigidbody FireballInstance;

            FireballInstance = Instantiate(fireballPrefab, FBSpawnpoint.position, FBSpawnpoint.rotation) as Rigidbody;
            FireballInstance.AddForce(FBSpawnpoint.forward * 1000);*/

            /*if (Vector3.Distance(transform.position, currentPatrolDest.transform.position) > 2)
            {
                agent.SetDestination(currentPatrolDest.transform.position);
                //Debug.Log(Vector3.Distance(transform.position, currentPatrolDest.transform.position));
            }

            else
            {
                currentPatrolDest = enemyPatrol.FindNode(currentPatrolDest).GetOutgoing()[0].GetData();
            }
        }*/

    }

    void Patrol()
    {
        agent.isStopped = false;
        anim.SetBool("FiringRange", true);
        /*StopCoroutine(fireCast());

        Rigidbody FireballInstance;

        FireballInstance = Instantiate(fireballPrefab, FBSpawnpoint.position, FBSpawnpoint.rotation) as Rigidbody;
        FireballInstance.AddForce(FBSpawnpoint.forward * 1000);*/

        if (Vector3.Distance(transform.position, currentPatrolDest.transform.position) > 2)
        {
            agent.SetDestination(currentPatrolDest.transform.position);
            //Debug.Log(Vector3.Distance(transform.position, currentPatrolDest.transform.position));
        }

        else
        {
            currentPatrolDest = enemyPatrol.FindNode(currentPatrolDest).GetOutgoing()[0].GetData();
        }

        //Swtich States
        if (Vector3.Distance(player.position, this.transform.position) < 15.0f)
            state = WolfieState.SeePlayerInRange;
    }

    void ShootFireball()
    {
        //From here
        agent.isStopped = true;
        anim.SetBool("FiringRange", false);

        if (Time.time > newFireballTime)
        {
            /*if (direction.magnitude < 10.0f)
            {
                this.transform.Translate(0, 0, 0.1f);
                anim.SetBool("FiringRange", true);

                if (Vector3.Distance(player.position, this.transform.position) < 2)
                {
                    anim.SetBool("TargetCloseRange", true);
                }
                // anim.SetBool("isPunching", false);
            }

            else
            {*/
                // anim.SetBool("isPunching", true);
                StartCoroutine(fireCast());
            //}
        }

        if (Vector3.Distance(player.position, this.transform.position) < 5.0f)
            state = WolfieState.CloseToPlayer;
    }

    void AttackPlayer(Vector3 pDirection)
    {
        if (pDirection.magnitude < 10.0f)
        {
            this.transform.Translate(0, 0, 0.1f);
            anim.SetBool("FiringRange", true);

            if (Vector3.Distance(player.position, this.transform.position) < 5
                && Vector3.Distance(player.position, this.transform.position) > 3)
            {
                attackDecision = Random.Range(0, 2);
                Debug.Log(attackDecision);

                if (attackDecision == 1)
                {
                    anim.SetBool("TargetLongRange", true);
                }
            }

            if (attackDecision == 0 && Vector3.Distance(player.position, this.transform.position) < 3
                    && Vector3.Distance(player.position, this.transform.position) > 1)
            {
                anim.SetBool("TargetCloseRange", true);
            }


            // anim.SetBool("isPunching", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Fireball")
        {
            Debug.Log("fireball hit");
            Destroy(gameObject, 0.0f);
        }
    }

    IEnumerator fireCast()
    {
        anim.SetTrigger("PlayerInRange");
        Rigidbody FireballInstance;

        FireballInstance = Instantiate(fireballPrefab, FBSpawnpoint.position, FBSpawnpoint.rotation) as Rigidbody;
        FireballInstance.AddForce(FBSpawnpoint.forward * 1000);

        yield return new WaitForSeconds(_fireballDuration);

        GetComponent<Rigidbody>().velocity = Vector3.zero;

        newFireballTime = Time.time + fireballCooldown;
    }
}

