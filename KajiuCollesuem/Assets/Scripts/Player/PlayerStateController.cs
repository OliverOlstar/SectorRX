using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    //THIS IS THE STATE MANAGER - Danish

         /*
         This script is to change the player's active state 
         depending on the inputs received. 
         */


    [Header("Inputs")]
    // Movement variables
    [HideInInspector] public float horizontalInput = 0;
    [HideInInspector] public float verticalInput = 0;
    [HideInInspector] public Vector3 movementDir;
    [HideInInspector] public float moveAmount = 0;

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
    private PlayerStateMachine stateMachine;
    public PlayerMovement _movementComponent { get; private set; } // Player's movement component, access this to move and jump
    // TODO LockOn Component // Player's lockon component changes player's movement aanimations
    [HideInInspector] public PlayerDodge _dodgeComponent; // Player's dodge component, access this to 

    private PlayerLockOnScript _lockOnComponent;
    private PlayerPowerHandler _powerComponent;

    [HideInInspector] public Rigidbody _rb;

    //enum States
    //{
    //    Normal, // Player's default state, able to move and can initiate attack
    //    LockedOn, // Payer is locked on to an enemy and can transition into any other state
    //    Dodging, // Player is currently in a dodge aninmation and cannot move or initiate an attack
    //    Attacking, // Player is currently in a attack animation, cannot move and cannot initiate another attack
    //    Stunned, // Player is stunned and cannot move
    //    Dead // Player doesn't receive anymore input
    //};
    
    //[SerializeField] private int state = (int) States.Normal;


    // Temporary Utility variables
    public bool OnGround = false;
    
    void Start()
    {
        _movementComponent = GetComponent<PlayerMovement>();
        _dodgeComponent = GetComponent<PlayerDodge>();
        _lockOnComponent = GetComponent<PlayerLockOnScript>();
        _powerComponent = GetComponent<PlayerPowerHandler>();

        stateMachine = GetComponent<PlayerStateMachine>();
        InitializeStateMachine();

        _rb = GetComponent<Rigidbody>();
    }

    void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            {typeof(IdleState), new IdleState(controller:this) },
            {typeof(RunState), new RunState(controller:this) },
            {typeof(DodgeState), new DodgeState(controller:this) },
            {typeof(StunnedState), new StunnedState(controller:this) },
            {typeof(AttackState), new AttackState(controller:this) },
            {typeof(DeathState), new DeathState(controller:this) }
        };

        stateMachine.SetStates(states);
    }


    
    //void Update()
    //{
    //    switch(state)
    //    {
    //        //Normal
    //        case (int) States.Normal:
    //            if (powerInput > 0)
    //            {
    //                _powerComponent.UsingPower(powerInput);
    //                powerInput = 0;
    //            }

    //            //Lock On
    //            if (lockOnInput)
    //            {
    //                _lockOnComponent.lockOnInput = true;
    //                lockOnInput = false;
    //            }


    //            //Swtich States
    //            if (shortDodgeInput || longDodgeInput)
    //            {
    //                //Sending Inputs
    //                if (jumpInput)
    //                {
    //                    _movementComponent.jumpInput = true;
    //                    jumpInput = false;
    //                }
                
    //                _movementComponent.horizontalInput = horizontalInput;
    //                _movementComponent.verticalInput = verticalInput;

    //                //Swtich States
    //                if (shortDodgeInput || longDodgeInput)
    //                {
    //                    SwitchStates((int)States.Dodging);
    //                    shortDodgeInput = false; longDodgeInput = false;
    //                }
    //            }
    //            break;

    //        //Dodge
    //        case (int) States.Dodging:




    //            //Swtich States
    //            if (_dodgeComponent.doneDodge)
    //            {
    //                _dodgeComponent.doneDodge = false;
    //                SwitchStates((int)States.Normal);
    //            }

    //            break;

    //        //Locked On
    //        case (int) States.LockedOn:

    //            break;

    //        //Attacking
    //        case (int) States.Attacking:

    //            break;

    //        //Stunned
    //        case (int) States.Stunned:

    //            break;

    //        //Dead
    //        case (int) States.Dead:

    //            break;


    //    }
    //}

    ////SWITCH STATES
    //private void SwitchStates(int pState)
    //{
    //    //SWITCHING OFF OF ////////////////////////////////////////////////////
    //    if (state != pState)
    //    {
    //        switch (state)
    //        {
    //            //Normal
    //            case (int)States.Normal:

    //                _movementComponent.enabled = false;

    //                break;

    //            //Dodge
    //            case (int)States.Dodging:

    //                break;

    //            //Locked On
    //            case (int)States.LockedOn:

    //                _movementComponent.enabled = false;

    //                break;

    //            //Attacking
    //            case (int)States.Attacking:

    //                break;

    //            //Stunned
    //            case (int)States.Stunned:

    //                break;

    //            //Dead
    //            case (int)States.Dead:

    //                break;
    //        }
    //    }


    //    //SWITCHING ON TO ////////////////////////////////////////////////////
    //    switch (pState)
    //    {
    //        //Normal
    //        case (int)States.Normal:

    //            _movementComponent.enabled = true;

    //            break;

    //        //Dodge
    //        case (int)States.Dodging:

    //            _dodgeComponent.Dodge(shortDodgeInput);

    //            break;

    //        //Locked On
    //        case (int)States.LockedOn:

    //            _movementComponent.enabled = true;

    //            break;

    //        //Attacking
    //        case (int)States.Attacking:

    //            break;

    //        //Stunned
    //        case (int)States.Stunned:

    //            break;

    //        //Dead
    //        case (int)States.Dead:

    //            break;
    //    }
        
    //    //Change State Variable
    //    state = pState;
    //}


    // Temporary Utility function to perform a raycast and determine if the player is moving on the ground
    bool CheckIfOnGround()
    {
        bool r = false;
        Vector3 origin = transform.position + (Vector3.up * 0.5f);
        Vector3 dir = -Vector3.up;
        float dist = 0.8f;
        RaycastHit hit;

        if(Physics.Raycast(origin, dir, out hit, dist, ~(1 << 11)))
        {
            r = true;
            Vector3 targetPos = hit.point;
            transform.position = targetPos;
        }


        return r;
    }
}
