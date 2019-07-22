using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    //THIS IS THE STATE MANAGER - Oliver, Danish

         /*
         This script is to change the player's active state 
         depending on the inputs received. 
         */


    [Header("Inputs")]
    // Movement variables
    public float horizontalInput = 0;
    public float verticalInput = 0;

    // Attack Varaibles
    public bool quickAttack = false;
    public bool heavyAtatck = false;
    public bool lockOn = false;

    // Spontaneous Movement Variables
    public bool jumpInput = false;
    public bool longDodgeInput = false;
    public bool shortDodgeInput = false;

    // Power Use Inputs
    public bool power1 = false;
    public bool power2 = false;
    public bool power3 = false;

    // Menu Inputs
    public bool pause = false;
    public bool map = false;

    [Header("State Components")]
    private PlayerMovement _movementComponent;
    private PlayerDodge _dodgeComponent;

    enum States { Normal, LockedOn, Dodging, Attacking, Stunned, Dead };
    public int state = (int) States.Normal;
    
    void Start()
    {
        _movementComponent = GetComponent<PlayerMovement>();
        if (!_movementComponent)
            gameObject.AddComponent<PlayerMovement>();


        _dodgeComponent = GetComponent<PlayerDodge>();
        if (!_dodgeComponent)
            gameObject.AddComponent<PlayerDodge>();
    }
    
    void Update()
    {
        switch(state)
        {
            //Normal
            case (int) States.Normal:

                //Temperary
                if (Input.GetButtonDown("Jump"))
                    jumpInput = true;

                if (Input.GetKeyDown(KeyCode.C))
                    shortDodgeInput = true;

                horizontalInput = Input.GetAxis("Horizontal");
                verticalInput = Input.GetAxis("Vertical");


                //Sending Inputs
                if (jumpInput)
                {
                    _movementComponent.jumpInput = true;
                    jumpInput = false;
                }
                
                _movementComponent.horizontalInput = horizontalInput;
                _movementComponent.verticalInput = verticalInput;


                //Swtich States
                if (shortDodgeInput || longDodgeInput)
                {
                    SwitchStates((int)States.Dodging);
                }

                break;

            //Dodge
            case (int)States.Dodging:

                break;

            //Locked On
            case (int) States.LockedOn:

                break;

            //Attacking
            case (int) States.Attacking:

                break;

            //Stunned
            case (int) States.Stunned:

                break;

            //Dead
            case (int) States.Dead:

                break;


        }
    }

    //SWITCH STATES
    private void SwitchStates(int pState)
    {
        //SWITCHING OFF OF ////////////////////////////////////////////////////
        switch (state)
        {
            //Normal
            case (int)States.Normal:

                _movementComponent.enabled = false;

                break;

            //Dodge
            case (int)States.Dodging:

                break;

            //Locked On
            case (int)States.LockedOn:

                break;

            //Attacking
            case (int)States.Attacking:

                break;

            //Stunned
            case (int)States.Stunned:

                break;

            //Dead
            case (int)States.Dead:

                break;


        }

        //SWITCHING ON TO ////////////////////////////////////////////////////
        switch (pState)
        {
            //Normal
            case (int)States.Normal:

                _movementComponent.enabled = true;

                break;

            //Dodge
            case (int)States.Dodging:

                _dodgeComponent.Dodge(shortDodgeInput);

                break;

            //Locked On
            case (int)States.LockedOn:

                break;

            //Attacking
            case (int)States.Attacking:

                break;

            //Stunned
            case (int)States.Stunned:

                break;

            //Dead
            case (int)States.Dead:

                break;


        }
        
        //Change State Variable
        state = pState;
    }
}
