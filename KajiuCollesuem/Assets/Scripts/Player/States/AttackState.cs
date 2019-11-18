﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    PlayerStateController stateController;
    
    private bool done = false;
    private int combo = 0;
    private float AttackStateReturnDelayLength = 0.6f;

    public AttackState(PlayerStateController controller) : base(controller.gameObject)
    {
        stateController = controller;
    }

    public override void Enter()
    {
        //stateController._hitboxComponent.gameObject.SetActive(true); /* Handled by animation events */
        combo = 0;

        //Run attack
        Attack();
    }

    public override void Exit()
    {
        //stateController._hitboxComponent.gameObject.SetActive(false); /* Handled by animation events */
        ClearAttackInputs();
        stateController.AttackStateReturnDelay = Time.time + AttackStateReturnDelayLength;
    }

    public override Type Tick()
    {
        //State Switched with Animation Events
        switch (stateController._animHandler.attackState)
        {
            case 0:
                ClearAttackInputs();
                break;

            case 1:
                Attack();
                break;

            case 2:
                done = true;
                break;
        }

        //Done Attack
        if (done)
        {
            done = false;
            return typeof(MovementState);
        }

        //Stunned
        if (stateController.Stunned)
        {
            return typeof(StunnedState);
        }

        return null;
    }

    private void Attack()
    {
        if (stateController.quickAttackInput && combo < 3)
        {
            //Debug.Log("AttackState: Attack");
            combo++;
            ClearAttackInputs();
            stateController._animHandler.StartAttack(false, combo);
        }

        if (stateController.heavyAttackInput && combo < 3)
        {
            //Debug.Log("AttackState: Attack");
            combo++;
            ClearAttackInputs();
            stateController._animHandler.StartAttack(true, combo);
        }

        if (stateController.powerInput > 0)
        {
            // run code from the power component
            int whichPower = stateController._powerComponent.UsingPower(stateController.powerInput);

            if (whichPower == -1)
            {
                done = true;
                Debug.Log("No In Slot Power");
            }
            else if (whichPower == -2)
            {
                done = true;
                Debug.Log("Not Enough Power");
            }
            else
            {
                stateController._animHandler.StartPower(whichPower);
                combo = 3;
            }
        }
    }

    private void ClearAttackInputs()
    {
        stateController.quickAttackInput = false;
        stateController.heavyAttackInput = false;
        stateController.powerInput = 0;
    }
}
