using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    PlayerStateController stateController;

    float actionDelay = 0.3f;
    float timer = 0;

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
            stateController._powerComponent.UsingPower(stateController.powerInput);
        }

        stateController.quickAttackInput = false;
        stateController.heavyAttackInput = false;
    }

    public override void Exit()
    {

    }

    public override Type Tick()
    {
        Debug.Log("Attack State");

        timer += Time.deltaTime;

        if (timer >= 5)
        {
            return typeof(St)
        }

        return null;
    }
}
