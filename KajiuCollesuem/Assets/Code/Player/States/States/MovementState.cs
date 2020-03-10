using System;
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
    }

    public override void Exit()
    {
        //Debug.Log("MoveState: Exit");
        //stateController._modelController.ResetJump();
    }

    public override Type Tick()
    {
        // TODO Add Taunt system

        // Stunned Or Dead
        Type returnedState = stateController.DeadCheck();
        if (returnedState != null)
            return returnedState;

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

        // Attack or Ability
        returnedState = stateController.attackOrAbilityCheck();
        if (returnedState != null)
            return returnedState;

        return null;
    }
}
