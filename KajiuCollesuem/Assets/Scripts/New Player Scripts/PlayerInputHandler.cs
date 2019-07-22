using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    // THIS IS THE INPUT HANDLER - Danish

        /*
        This script is to get keyboard and controller input 
        and then send through to the State Manager
        */


    [Header("Inputs")]
    // Movement Inputs
    float vertical;
    float horizontal;

    // Attack Inputs and Timer
    bool attack_Input;
    float attack_Timer;
    bool lockon_Input;

    // Dodge Input and Timer
    bool dodge_Input;
    float dodge_Timer;
    bool jump_Input;


    // Power Use Inputs
    bool power1_Input;
    bool power2_Input;
    bool power3_Input;

    // Menu Inputs
    bool pause;
    bool map;




    PlayerStateController stateController;

    float delta;

    private void Awake()
    {
        stateController = GetComponent<PlayerStateController>();
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
        dodge_Input = Input.GetKey(KeyCode.Z);

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
        stateController.horizontalInput = horizontal;
        stateController.verticalInput = vertical;


        if(attack_Input && attack_Timer > 0.3f)
        {
            stateController.heavyAtatck = true;
        }
        else if(attack_Input && attack_Timer <= 0.3f)
        {
            stateController.quickAttack = true;
        }


        if(dodge_Input && dodge_Timer > 0.3f)
        {
            stateController.longDodgeInput = true;
        }
        else if(dodge_Input && dodge_Timer <= 0.3f)
        {
            stateController.shortDodgeInput = true;
        }

        if (lockon_Input)
        {
            stateController.lockOn = !stateController.lockOn;
        }

        stateController.jumpInput = jump_Input;

        stateController.power1 = power1_Input;
        stateController.power2 = power2_Input;
        stateController.power3 = power3_Input;
    }



    void ResetInputAndTimers()
    {
        if (!attack_Input)
        {
            attack_Timer = 0;
        }

        if (!dodge_Input)
        {
            dodge_Timer = 0;
        }
    }



}
