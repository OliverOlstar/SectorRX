using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    PlayerStateController stateController;

    private float actionDelay = 0.3f;
    private float timer = 0;
    private bool noPower = false;

    public AttackState(PlayerStateController controller) : base(controller.gameObject)
    {
        stateController = controller;
    }

    public override void Enter()
    {
        // Stop Coroutine from running
        if (stateController.quickAttackInput)
        {
            // Start coroutine to start a timer
            // run code from attack component
        }

        if (stateController.heavyAttackInput)
        {
            // Start Coroutine to start a timer
            // run code from the attack component
        }

        if (stateController.powerInput > 0)
        {
            // Start Coroutine to start a timer
            // run code from the attack component
            noPower = stateController._powerComponent.UsingPower(stateController.powerInput);
        }

        stateController.quickAttackInput = false;
        stateController.heavyAttackInput = false;
        stateController.powerInput = 0;

        // TODO turn on route motion
    }

    public override void Exit()
    {
        // TODO turn off route motion
    }

    public override Type Tick()
    {
        //Debug.Log("Attack State");

        timer += Time.deltaTime;

        if (timer >= 1 || noPower)
        {
            timer = 0;
            noPower = false;
            return typeof(MovementState);
        }

        return null;
    }
}
