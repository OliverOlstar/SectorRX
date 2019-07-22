using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    public int state;
    /*
    0 - Normal
    1 - Locked On
    2 - Attacking
    3 - Stunned
    4 - Dead
    */
    
    void Start()
    {
        
    }
    
    void Update()
    {
        switch(state)
        {
            //Normal
            case 0:

                break;

            //Locked On
            case 1:

                break;

            //Attacking
            case 2:

                break;

            //Stunned
            case 3:

                break;

            //Dead
            case 4:

                break;


        }
    }
}
