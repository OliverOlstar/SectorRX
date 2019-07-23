using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    // THIS IS THE INPUT HANDLER - Danish

    /*
    This script is to get keyboard and gamepad input 
    and then send through to the State Manager
    */

    [Header("Dodge Input Settings")]
    [SerializeField] private float dodge_holdMax = 0.4f;
    [SerializeField] private float dodge_timeToLong = 0.3f;
    
    [Header("Inputs")]
    // Movement Inputs
    private float vertical;
    private float horizontal;

    // Attack Inputs and Timer
    private bool attack_Input;
    private float attack_Timer;
    private bool lockon_Input;

    // Dodge Input and Timer
    private bool dodge_Input;
    private bool dodge_release_Input;
    private bool dodge_Input_WaitingForRelease;
    private float dodge_Timer;
    private bool jump_Input;


    // Power Use Inputs
    private bool power1_Input;
    private bool power2_Input;
    private bool power3_Input;

    // Menu Inputs
    private bool pause;
    private bool map;


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

        attack_Input = Input.GetMouseButton(0);
        
        dodge_release_Input = Input.GetKeyUp(KeyCode.Z);

        //Only Listening to dodge if player has released the key between dodges
        if (dodge_Input_WaitingForRelease == false)
        {
            dodge_Input = Input.GetKey(KeyCode.Z);
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

        lockon_Input = Input.GetKeyDown(KeyCode.X);
        jump_Input = Input.GetButtonDown("Jump");

        power1_Input = Input.GetKeyDown(KeyCode.Alpha1);
        power2_Input = Input.GetKeyDown(KeyCode.Alpha2);
        power3_Input = Input.GetKeyDown(KeyCode.Alpha3);

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

        //if (!dodge_Input)
        //{
        //    dodge_Timer = 0;
        //}
    }
}
