using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolScript : MonoBehaviour
{
    public float speed;
    public Animator wolfAnim;

    public Transform patrolSpots;

    private float waitTime;
    public float startWaitTime;

    public Transform PlayerPosition;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = startWaitTime;
        PlayerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        patrolSpots.position = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(patrolSpots.position, this.transform.position) < 100f)
        {
            Vector3 direction = patrolSpots.position - this.transform.position;
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                Quaternion.LookRotation(direction), 0.1f);
        }

            if (Vector3.Distance(transform.position, patrolSpots.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                patrolSpots.position = new Vector3(Random.Range(minX, maxX), 0, Random.Range(minZ, maxZ));
                waitTime = startWaitTime;
            }

            else
            {
                waitTime -= Time.deltaTime;
            }

        }

        if (Vector3.Distance(wolfAnim.transform.position, PlayerPosition.position) > 30)
        {

            wolfAnim.transform.position = Vector3.MoveTowards(wolfAnim.transform.position, patrolSpots.position, speed * Time.deltaTime);

            wolfAnim.SetBool("isChasing", false);
            wolfAnim.SetBool("isIdle", false);
            wolfAnim.SetBool("isPatrol", true);
        }

        else
        {
            wolfAnim.SetBool("isPatrol", false);
        }
    }
}
