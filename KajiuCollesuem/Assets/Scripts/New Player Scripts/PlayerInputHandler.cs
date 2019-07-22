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
    bool lockon_Input;
    bool power1;
    bool power2;
    bool power3;

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
    }

    void GetInput()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        attack_Input = Input.GetMouseButtonDown(0);
    }



}
