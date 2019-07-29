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
    [HideInInspector] public float horizontalInput = 0;
    [HideInInspector] public float verticalInput = 0;

    // Movement Variables
    [HideInInspector] public bool jumpInput = false;
    [HideInInspector] public bool longDodgeInput = false;
    [HideInInspector] public bool shortDodgeInput = false;

    // Attack Varaibles
    [HideInInspector] public bool quickAttackInput = false;
    [HideInInspector] public bool heavyAtatckInput = false;

    //Camera
    [HideInInspector] public bool lockOnInput = false;

    // Power Use Inputs
    [HideInInspector] public int powerInput = 0;

    // Menu Inputs
    [HideInInspector] public bool pauseInput = false;
    [HideInInspector] public bool mapInput = false;

    [Header("State Components")]
    private PlayerMovement _movementComponent;
    private PlayerDodge _dodgeComponent;
    private PlayerLockOnScript _lockOnComponent;
    private playerPowers _powerComponent;

    enum States { Normal, LockedOn, Dodging, Attacking, Stunned, Dead };
    [SerializeField] private int state = (int) States.Normal;
    
    void Start()
    {
        _movementComponent = GetComponent<PlayerMovement>();
        _dodgeComponent = GetComponent<PlayerDodge>();
        _lockOnComponent = GetComponent<PlayerLockOnScript>();
        _powerComponent = GetComponent<playerPowers>();
    }
    
    void Update()
    {
        switch(state)
        {
            //Normal
            case (int) States.Normal:
                //Sending Inputs
                if (jumpInput)
                {
                    _movementComponent.jumpInput = true;
                    jumpInput = false;
                }
                
                _movementComponent.horizontalInput = horizontalInput;
                _movementComponent.verticalInput = verticalInput;

                if (powerInput > 0)
                {
                    _powerComponent.UsingPower(powerInput);
                    powerInput = 0;
                }

                //Lock On
                if (lockOnInput)
                {
                    _lockOnComponent.lockOnInput = true;
                    lockOnInput = false;
                }


                //Swtich States
                if (shortDodgeInput || longDodgeInput)
                {
                    SwitchStates((int)States.Dodging);
                    shortDodgeInput = false; longDodgeInput = false;
                }

                break;

            //Dodge
            case (int) States.Dodging:




                //Swtich States
                if (_dodgeComponent.doneDodge)
                {
                    _dodgeComponent.doneDodge = false;
                    SwitchStates((int)States.Normal);
                }

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
        if (state != pState)
        {
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

                    _movementComponent.enabled = false;

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

                _movementComponent.enabled = true;

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
