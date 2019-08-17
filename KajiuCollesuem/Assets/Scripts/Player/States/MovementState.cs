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
        stateController._movementComponent.disableMovement = false;
    }

    public override void Exit()
    {
        stateController._movementComponent.disableMovement = true;
    }

    public override Type Tick()
    {
        Debug.Log("Run State");

        stateController._movementComponent.horizontalInput = stateController.horizontalInput;
        stateController._movementComponent.verticalInput = stateController.verticalInput;
        stateController._movementComponent.jumpInput = stateController.jumpInput;

        if (stateController.longDodgeInput || stateController.shortDodgeInput)
        {
            return typeof(DodgeState);
        }

        return null;
    }
}
