using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Inputs")]
    float vertical;
    float horizontal;

    bool attack_Input;
    float attack_Timer;
    bool dodge_Input;
    float dodge_Timer;

    bool jump_Input;




    PlayerStateController stateController;

    float delta;

    private void Awake()
    {
        stateController = GetComponent<PlayerStateController>();
    }



}
