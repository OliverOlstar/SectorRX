using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerScript : AI
{ 
    public int damage = 30;

    private float attackTimer = 0;
    public float attackTimerMax;
    public float fieldOfVision;

    public bool playerInRange = false;

    public GameObject Player;
    public GameObject Bullet;

    protected override void RangerAI()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < fieldOfVision)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }

        if (playerInRange)
        {
            agent.isStopped = true;
            attackTimer = attackTimer + 0.01f;
            if (attackTimer >= attackTimerMax) Fire();
        }
        else
        {
            agent.isStopped = false;
        }
    }
    public void Fire()
    {
        Instantiate(Bullet);
        attackTimer = 0;
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") playerInRange = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") playerInRange = false;
    }*/
}
