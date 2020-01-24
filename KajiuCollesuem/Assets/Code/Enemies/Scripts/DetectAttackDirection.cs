using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Programmer: Mugiesshan Anandarajah
 * Description: Allows enemy to detect the direction of the attack, and immediately pursue the target
 * */
public class DetectAttackDirection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("SwordHitBox"))
        {
            Transform player = other.gameObject.transform;

            while (!player.gameObject.tag.Equals("Player"))
                player = player.parent;

            Decision decision = GetComponent<Decision>();
            //decision.retribution = true;
            decision.target = player;
            decision.SetupStates();
            //transform.LookAt(player);
        }
    }
}
