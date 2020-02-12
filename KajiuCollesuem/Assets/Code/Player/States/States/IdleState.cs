using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    PlayerStateController stateController;
    private float dodgeInputBuffer = 0.4f;
    private float dodgeInputTime = 0.0f;
    private float dodgeInput = -1.0f;

    public IdleState(PlayerStateController controller) : base(controller.gameObject)
    {
        stateController = controller;
    }

    public override void Enter()
    {
        //Debug.Log("IdleState: Enter");
        stateController._movementComponent.disableMovement = false;
    }

    public override void Exit()
    {
        //Debug.Log("IdleState: Exit");
        stateController._movementComponent.disableMovement = true;

        // If Dodge input happened less than a dodgeInputBuffer time ago add the input back in
        if (dodgeInputTime + dodgeInputBuffer >= Time.time)
        {
            stateController.dodgeInput = dodgeInput;
        }
    }

    public override Type Tick()
    {
        // TODO Add Taunt system

        // Stunned Or Dead
        Type stunnedOrDead = stateController.stunnedOrDeadCheck();
        if (stunnedOrDead != null)
            return stunnedOrDead;

        // Idle
        if (stateController.moveInput.magnitude > 0)
        {
            return typeof(MovementState);
        }

        // Dodge Input Buffer Getter
        if (stateController.dodgeInput != -1)
        {
            dodgeInput = stateController.dodgeInput;
            stateController.dodgeInput = -1.0f;

            dodgeInputTime = Time.time;
        }

        //Attack
        if (stateController.heavyAttackinput != -1.0f || stateController.lightAttackinput != -1.0f || stateController.ability1input != -1.0f || stateController.ability2input != -1.0f)
        {
            if (stateController.AttackStateReturnDelay <= Time.time)
            {
                return typeof(AttackState);
            }
            else
            {
                //If Inputed attack before they can return to the attack state, remove the input
                stateController.clearAttackInputs();
            }
        }

        return null;
    }
}
