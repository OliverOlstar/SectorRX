using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    DirectedGraph enemyPatrol = new DirectedGraph();
    public GameObject patrolPath;
    GameObject currentPatrolDest;
    AI ai;

    // Start is called before the first frame update
    void Start()
    {
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
        ai = this.GetComponent<AI>();
        ai.isPatrolling = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (ai.isPatrolling)
        {
            if (!transform.position.Equals(currentPatrolDest.transform.position))
            {
                ai.GetAgent().SetDestination(currentPatrolDest.transform.position);
            }

            else
            {
                currentPatrolDest = enemyPatrol.FindNode(currentPatrolDest).GetOutgoing()[0].GetData();
            }
        }
    }
}
