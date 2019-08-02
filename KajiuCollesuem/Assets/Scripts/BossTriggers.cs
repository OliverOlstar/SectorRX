using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggers : MonoBehaviour
{
    public bool playerInBossRoom;
    public bool bossKilled;

    public GameObject BossWall;
    public GameObject FinalBoss;

    // Start is called before the first frame update
    void Start()
    {
        bossKilled = false;
        playerInBossRoom = false;
        if (!BossWall) Debug.Log("BossWall not set on " + this + ". Player will not be locked in room.");
        else BossWall.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInBossRoom && !bossKilled) BossWall.SetActive(true);
        if (bossKilled) BossWall.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInBossRoom = true;
            FinalBoss.GetComponent<BossMovement>().bossAggro = true;
            FinalBoss.GetComponent<BossMovement>().StartCoroutine("SwitchDirection");
        }
    }

    public void RemoveBossWall()
    {
        bossKilled = true;
    }
}
