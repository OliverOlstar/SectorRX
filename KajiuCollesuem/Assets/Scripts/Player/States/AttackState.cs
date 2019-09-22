using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    PlayerStateController stateController;

    private float attackLength;
    private bool done = false;
    private float timer = 0;

    public AttackState(PlayerStateController controller) : base(controller.gameObject)
    {
        stateController = controller;
    }

    public override void Enter()
    {
        //stateController._hitboxComponent.gameObject.SetActive(true); // Handled by animation events

        // Stop Coroutine from running
        if (stateController.quickAttackInput)
        {
            stateController._animHandler.LightAttack();
            attackLength = 1.35f;
            // run code from attack component
        }

        if (stateController.heavyAttackInput)
        {
            stateController._animHandler.HeavyAttack();
            attackLength = 1.7f;
            // run code from the attack component
        }

        if (stateController.powerInput > 0)
        {
            attackLength = 0.5f;
            // run code from the attack component
            done = stateController._powerComponent.UsingPower(stateController.powerInput);
        }
    }

    public override void Exit()
    {
        //stateController._hitboxComponent.gameObject.SetActive(false); // Handled by animation events
        stateController.quickAttackInput = false;
        stateController.heavyAttackInput = false;
        stateController.powerInput = 0;
    }

    public override Type Tick()
    {
        //Debug.Log("Attack State");
        timer += Time.deltaTime;
        
        if (done || timer >= attackLength)
        {
            done = false;
            timer = 0;
            return typeof(MovementState);
        }

        if (stateController.Stunned)
        {
            return typeof(StunnedState);
        }

        return null;
    }
}
