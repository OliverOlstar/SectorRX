using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerScript : MonoBehaviour {

    public float Speed;
    public float startWait;
    public Transform[] moveSpots;
    public Transform Player;

    private float waitTime;
    private int randomSpot;

    void Start()
    {
        waitTime = startWait;
        randomSpot = Random.Range(0, moveSpots.Length);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpot].position, Speed * Time.deltaTime);

        if (Player != null)
        {
            transform.LookAt(Player);
        }

        if (Vector3.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWait;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
