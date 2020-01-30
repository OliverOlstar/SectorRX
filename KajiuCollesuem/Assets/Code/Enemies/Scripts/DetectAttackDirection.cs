using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Programmer: Mugiesshan Anandarajah
 * Description: Allows enemy to detect the direction of the attack, and immediately pursue the target
 * */
public class DetectAttackDirection : MonoBehaviour
{
    private Decision _decision;

    // Start is called before the first frame update
    void Start()
    {
        _decision = GetComponent<Decision>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("SwordHitBox"))
        {
            Transform player = other.gameObject.transform;

            while (!player.gameObject.tag.Equals("Player"))
                player = player.parent;

            //decision.retribution = true;
            _decision.UpdateTarget(player);
            //decision.SetupStates();
        //    //transform.LookAt(player);
        }
    }
}
