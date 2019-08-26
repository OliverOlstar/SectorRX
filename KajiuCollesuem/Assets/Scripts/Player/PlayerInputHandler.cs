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
    //[SerializeField] private KeyCode KCverticalUp = KeyCode.W;
    //[SerializeField] private KeyCode KCverticalDown = KeyCode.S;
    //[SerializeField] private KeyCode KChorizontalLeft = KeyCode.A;
    //[SerializeField] private KeyCode KChorizontalRight = KeyCode.D;

    [Header("Attack")]
    private bool attack_Input;
    private float attack_Timer;
    [SerializeField] private KeyCode KCattack_Input = KeyCode.Mouse0;
    [SerializeField] private KeyCode gp_KCattack_Input = KeyCode.JoystickButton2;

    [Header("Camera")]
    private bool lockon_Input;
    [SerializeField] private KeyCode KClockon_Input = KeyCode.Mouse1;
    [SerializeField] private KeyCode gp_KClockon_Input = KeyCode.JoystickButton3;

    [Header("Dodge")]
    private bool dodge_Input;
    private bool dodge_release_Input;
    private bool dodge_Input_WaitingForRelease;
    private float dodge_Timer;
    [SerializeField] private KeyCode KCdodge_Input = KeyCode.Space;
    [SerializeField] private KeyCode gp_KCdodge_Input = KeyCode.JoystickButton0;

    [Header("Jump")]
    private bool jump_Input;
    [SerializeField] private KeyCode KCjump_Input = KeyCode.LeftShift;
    [SerializeField] private KeyCode gp_KCjump_Input = KeyCode.JoystickButton1;
    
    [Header("Powers")]
    private int power_Input;
    [SerializeField] private KeyCode KCpower1_Input = KeyCode.Q;
    [SerializeField] private KeyCode KCpower2_Input = KeyCode.E;
    [SerializeField] private KeyCode KCpower3_Input = KeyCode.F;
    [SerializeField] private KeyCode gp_KCpower1_Input = KeyCode.JoystickButton4;
    [SerializeField] private KeyCode gp_KCpower2_Input = KeyCode.JoystickButton5;
    [SerializeField] private KeyCode gp_KCpower3_Input = KeyCode.JoystickButton8;

    [Header("Menu")]
    private bool pause_Input;
    private bool map_Input;
    [SerializeField] private KeyCode KCpause_Input = KeyCode.Escape;
    [SerializeField] private KeyCode KCmap_Input = KeyCode.M;
    [SerializeField] private KeyCode gp_KCpause_Input = KeyCode.JoystickButton6;
    [SerializeField] private KeyCode gp_KCmap_Input = KeyCode.JoystickButton7;

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
        //vertical = Input.GetKey(KCverticalUp) - Input.GetKey(KCverticalDown);
        vertical = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");

        attack_Input = Input.GetKeyDown(KCattack_Input);
        if (attack_Input == false)
            attack_Input = Input.GetKey(gp_KCattack_Input);

        dodge_release_Input = Input.GetKeyUp(KCdodge_Input);
        if (dodge_release_Input == false)
            dodge_release_Input = Input.GetKeyUp(gp_KCdodge_Input);

        //Only Listening to dodge if player has released the key between dodges
        if (dodge_Input_WaitingForRelease == false)
        {
            dodge_Input = Input.GetKey(KCdodge_Input);
            if (dodge_Input == false)
                dodge_Input = Input.GetKey(gp_KCdodge_Input);
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
        if (lockon_Input == false)
            lockon_Input = Input.GetKeyDown(gp_KClockon_Input);

        jump_Input = Input.GetKeyDown(KCjump_Input);
        if (jump_Input == false)
            jump_Input = Input.GetKeyDown(gp_KCjump_Input);

        if (Input.GetKeyDown(KCpower1_Input) || Input.GetKeyDown(gp_KCpower1_Input))
        {
            power_Input = 1;
        }
        else if (Input.GetKeyDown(KCpower2_Input) || Input.GetKeyDown(gp_KCpower2_Input))
        {
            power_Input = 2;
        }
        else if (Input.GetKeyDown(KCpower3_Input) || Input.GetKeyDown(gp_KCpower3_Input))
        {
            power_Input = 3;
        }

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
            _stateController.heavyAttackInput = true;
        }
        else if(attack_Input && attack_Timer <= 0.3f)
        {
            _stateController.quickAttackInput = true;
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
        if (power_Input > 0)
        {
            _stateController.powerInput = power_Input;
            power_Input = 0;
        }
    }
    
    void ResetInputAndTimers()
    {
        if (!attack_Input)
        {
            attack_Timer = 0;
        }
    }
}
