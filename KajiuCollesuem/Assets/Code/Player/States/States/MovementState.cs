﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : BaseState
{
    PlayerStateController stateController;

    public MovementState(PlayerStateController controller) : base(controller.gameObject)
    {
        stateController = controller;
    }

    public override void Enter()
    {
        //Debug.Log("MoveState: Enter");
        stateController._movementComponent.disableMovement = false;
    }

    public override void Exit()
    {
        //Debug.Log("MoveState: Exit");
        stateController._movementComponent.disableMovement = true;
        //stateController._modelController.ResetJump();
    }

    public override Type Tick()
    {
        // TODO Add Taunt system

        // Stunned Or Dead
        Type stunnedOrDead = stateController.stunnedOrDeadCheck();
        if (stunnedOrDead != null)
            return stunnedOrDead;

        // Idle
        if (stateController.moveInput.magnitude == 0)
        {
            return typeof(IdleState);
        }

        //Dodge
        if (stateController.dodgeInput != -1)
        {
            if (stateController.onGround)
                return typeof(DodgeState);
            else
                stateController.dodgeInput = -1;
        }

        // Attack
        if (stateController.heavyAttackinput != -1.0f || stateController.lightAttackinput != -1.0f)
        {
            return typeof(AttackState);
        }

        // Ability
        if (stateController.ability1input != -1.0f || stateController.ability2input != -1.0f)
        {
            return typeof(AbilityState);
        }

        return null;
    }
}
