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

    [Header("Movement")]
    // Movement Inputs
    private float vertical;
    private float horizontal;
    [SerializeField] private KeyCode KCverticalUp = KeyCode.W;
    [SerializeField] private KeyCode KCverticalDown = KeyCode.S;
    [SerializeField] private KeyCode KChorizontalLeft = KeyCode.A;
    [SerializeField] private KeyCode KChorizontalRight = KeyCode.D;

    [Header("Attack")]
    private bool attack_Input;
    private float attack_Timer;
    [SerializeField] private KeyCode KCattack_Input = KeyCode.Mouse0;

    [Header("Camera")]
    private bool lockon_Input;
    [SerializeField] private KeyCode KClockon_Input = KeyCode.Mouse1;

    [Header("Dodge")]
    private bool dodge_Input;
    private bool dodge_release_Input;
    private bool dodge_Input_WaitingForRelease;
    private float dodge_Timer;
    [SerializeField] private KeyCode KCdodge_Input = KeyCode.Space;

    [Header("Jump")]
    private bool jump_Input;
    [SerializeField] private KeyCode KCjump_Input = KeyCode.LeftShift;
    
    [Header("Powers")]
    private bool power1_Input;
    private bool power2_Input;
    private bool power3_Input;
    [SerializeField] private KeyCode KCpower1_Input = KeyCode.Q;
    [SerializeField] private KeyCode KCpower2_Input = KeyCode.E;
    [SerializeField] private KeyCode KCpower3_Input = KeyCode.F;

    [Header("Menu")]
    private bool pause_Input;
    private bool map_Input;
    [SerializeField] private KeyCode KCpause_Input = KeyCode.Escape;
    [SerializeField] private KeyCode KCmap_Input = KeyCode.M;

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

        attack_Input = Input.GetKeyDown(KCattack_Input);
        
        dodge_release_Input = Input.GetKeyUp(KCdodge_Input);

        //Only Listening to dodge if player has released the key between dodges
        if (dodge_Input_WaitingForRelease == false)
        {
            dodge_Input = Input.GetKey(KCdodge_Input);
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

        lockon_Input = Input.GetKeyDown(KClockon_Input);
        jump_Input = Input.GetKeyDown(KCjump_Input);

        power1_Input = Input.GetKeyDown(KCpower1_Input);
        power2_Input = Input.GetKeyDown(KCpower2_Input);
        power3_Input = Input.GetKeyDown(KCpower3_Input);

        if (attack_Input)
        {
            attack_Timer += delta;
        }

        if (dodge_Input)
        {
            dodge_Timer += delta;
        }

        lockon_Input = Input.GetKeyDown(KClockon_Input);
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
            _stateController.lockOnInput = true;
            lockon_Input = false;
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
