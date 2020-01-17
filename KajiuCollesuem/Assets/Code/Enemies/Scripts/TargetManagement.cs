using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetManagement : MonoBehaviour
{
    private GameObject[] _players;
    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float smallest_distance = 9999;
        int index = -1;

        for (int i = 0; i < _players.Length; ++i)
        {
            float distance = Vector3.Distance(transform.position, _players[i].transform.position);

            if (distance < smallest_distance)
            {
                smallest_distance = distance;
                index = i;
            }
        }
        transform.LookAt(_players[index].transform);
        _agent.SetDestination(_players[index].transform.position);
    }
}
