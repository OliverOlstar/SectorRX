using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPound : MonoBehaviour
{
    MovementComponent _PMovement;

    // Start is called before the first frame update
    void Start()
    {
        _PMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
