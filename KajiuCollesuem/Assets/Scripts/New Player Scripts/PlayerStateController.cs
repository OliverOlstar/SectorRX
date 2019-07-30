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
    [HideInInspector] public Vector3 movementDir;
    [HideInInspector] public float moveAmount = 0;

    // Movement Variables
    [HideInInspector] public bool jumpInput = false;
    [HideInInspector] public bool longDodgeInput = false;
    [HideInInspector] public bool shortDodgeInput = false;

    // Attack Varaibles
    [HideInInspector] public bool quickAttack = false;
    [HideInInspector] public bool heavyAtatck = false;
    [HideInInspector] public bool lockOn = false;

    // Power Use Inputs
    [HideInInspector] public bool power1 = false;
    [HideInInspector] public bool power2 = false;
    [HideInInspector] public bool power3 = false;

    // Menu Inputs
    [HideInInspector] public bool pause = false;
    [HideInInspector] public bool map = false;

    [Header("State Components")]
    private PlayerMovement _movementComponent; // Player's movement component, access this to move and jump
    // TODO LockOn Component // Player's lockon component changes player's movement aanimations
    private PlayerDodge _dodgeComponent; // Player's dodge component, access this to 

    enum States
    {
        Normal, // Player's default state, able to move and can initiate attack
        LockedOn, // Payer is locked on to an enemy and can transition into any other state
        Dodging, // Player is currently in a dodge aninmation and cannot move or initiate an attack
        Attacking, // Player is currently in a attack animation, cannot move and cannot initiate another attack
        Stunned, // Player is stunned and cannot move
        Dead // Player doesn't receive anymore input
    };

    [SerializeField] private int state = (int) States.Normal;


    // Temporary Utility variables
    public bool OnGround = false;
    
    void Start()
    {
        _movementComponent = GetComponent<PlayerMovement>();
        if (!_movementComponent)
            gameObject.AddComponent<PlayerMovement>();


        _dodgeComponent = GetComponent<PlayerDodge>();
        if (!_dodgeComponent)
            gameObject.AddComponent<PlayerDodge>();
    }


    public void FixedTick()
    {

    }

    public void Tick()
    {
        OnGround = CheckIfOnGround();
    }
    
    void Update()
    {
        switch(state)
        {
            //Normal
            case (int) States.Normal:

                if (OnGround)
                {
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
                        shortDodgeInput = false; longDodgeInput = false;
                    }
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
