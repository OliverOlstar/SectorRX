using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
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
        //If collided with the player model, player takes damage
        if (other.gameObject.GetComponent<PlayerAttributes>() != null)
        {
            other.gameObject.GetComponent<PlayerAttributes>().TakeDamage(40, true);
        }
    }
}
