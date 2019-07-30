using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    // THIS IS THE INPUT HANDLER - Danish, Oliver

    /*
    This script is to get keyboard and gamepad input 
    and then send through to the State Manager
    */

    [Header("Dodge Input Settings")]
    [SerializeField] private float dodge_holdMax = 0.4f;
    [SerializeField] private float dodge_timeToLong = 0.3f;

    [Header("Movement")]
    // Movement Inputs
    private float vertical;
    private float horizontal;
    [SerializeField] private KeyCode KDverticalUp = KeyCode.W;
    [SerializeField] private KeyCode KDverticalDown = KeyCode.S;
    [SerializeField] private KeyCode KDhorizontalLeft = KeyCode.A;
    [SerializeField] private KeyCode KDhorizontalRight = KeyCode.D;

    [Header("Attack")]
    private bool attack_Input;
    private float attack_Timer;
    private bool lockon_Input;
    [SerializeField] private KeyCode KDattack_Input = KeyCode.Mouse0;
    [SerializeField] private KeyCode KDlockon_Input = KeyCode.Mouse1;

    [Header("Dodge")]
    private bool dodge_Input;
    private bool dodge_release_Input;
    private bool dodge_Input_WaitingForRelease;
    private float dodge_Timer;
    [SerializeField] private KeyCode KDdodge_Input = KeyCode.Space;

    [Header("Jump")]
    private bool jump_Input;
    [SerializeField] private KeyCode KDjump_Input = KeyCode.LeftShift;
    
    [Header("Powers")]
    private bool power1_Input;
    private bool power2_Input;
    private bool power3_Input;
    [SerializeField] private KeyCode KDpower1_Input = KeyCode.Q;
    [SerializeField] private KeyCode KDpower2_Input = KeyCode.E;
    [SerializeField] private KeyCode KDpower3_Input = KeyCode.F;

    [Header("Menu")]
    private bool pause_Input;
    private bool map_Input;
    [SerializeField] private KeyCode KDpause_Input = KeyCode.Escape;
    [SerializeField] private KeyCode KDmap_Input = KeyCode.M;
    

    private PlayerStateController _stateController;

    private float delta;

    private void Awake()
    {
        _stateController = GetComponent<PlayerStateController>();
    }
    
    private void Update()
    {
        delta = Time.deltaTime;

        GetInput();
        UpdateStates();
        ResetInputAndTimers();
    }

    void GetInput()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        attack_Input = Input.GetKeyDown(KDattack_Input);
        
        dodge_release_Input = Input.GetKeyUp(KDdodge_Input);

        //Only Listening to dodge if player has released the key between dodges
        if (dodge_Input_WaitingForRelease == false)
        {
            dodge_Input = Input.GetKey(KDdodge_Input);
        }
        else if (dodge_Input_WaitingForRelease & dodge_release_Input)
        {
            dodge_Input_WaitingForRelease = false;
            dodge_release_Input = false;
        }
        else
        {
            dodge_Input = false;
        }

        lockon_Input = Input.GetKeyDown(KDlockon_Input);
        jump_Input = Input.GetKeyDown(KDjump_Input);

        power1_Input = Input.GetKeyDown(KDpower1_Input);
        power2_Input = Input.GetKeyDown(KDpower2_Input);
        power3_Input = Input.GetKeyDown(KDpower3_Input);

        if (attack_Input)
        {
            attack_Timer += delta;
        }

        if (dodge_Input)
        {
            dodge_Timer += delta;
        }
    }

    void UpdateStates()
    {
        //Movement Input
        _stateController.horizontalInput = horizontal;
        _stateController.verticalInput = vertical;

        Vector3 v = vertical * transform.forward;
        Vector3 h = horizontal * transform.right;
        _stateController.movementDir = (v + h).normalized;

        float m = Mathf.Abs(vertical) + Mathf.Abs(horizontal);
        _stateController.moveAmount = Mathf.Clamp01(m);



        //Attacking Input
        if(attack_Input && attack_Timer > 0.3f)
        {
            _stateController.heavyAtatck = true;
        }
        else if(attack_Input && attack_Timer <= 0.3f)
        {
            _stateController.quickAttack = true;
        }

        //Dodging Input
        if (dodge_release_Input && dodge_Timer > dodge_timeToLong)
        {
            _stateController.longDodgeInput = true;
            dodge_Timer = 0;
        }
        else if (dodge_release_Input && dodge_Timer <= dodge_timeToLong)
        {
            _stateController.shortDodgeInput = true;
            dodge_Timer = 0;
        }
        //Dodge if held for max time
        else if (dodge_Timer > dodge_holdMax)
        {
            dodge_Input_WaitingForRelease = true;
            _stateController.longDodgeInput = true;
            dodge_Timer = 0;
        }

        //LockOn Input
        if (lockon_Input)
        {
            _stateController.lockOn = !_stateController.lockOn;
        }

        //Jump Input
        _stateController.jumpInput = jump_Input;

        //Powers Input
        _stateController.power1 = power1_Input;
        _stateController.power2 = power2_Input;
        _stateController.power3 = power3_Input;
    }
    
    void ResetInputAndTimers()
    {
        if (!attack_Input)
        {
            attack_Timer = 0;
        }
    }
}
