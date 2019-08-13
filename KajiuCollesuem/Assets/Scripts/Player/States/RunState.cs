using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : BaseState
{
    PlayerStateController stateController;

    

    public RunState(PlayerStateController controller) : base(controller.gameObject)
    {
        stateController = controller;
    }

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        Debug.Log("Dodge");



    }

    public override Type Tick()
    {
        Debug.Log("Run State");

        if (stateController.verticalInput == 0 && stateController.horizontalInput == 0)
        {
            //return typeof(IdleState);
        }

        //stateController._rb.velocity = stateController.movementDir * (stateController._movementComponent.moveSpeed * stateController.moveAmount);
        stateController._movementComponent.horizontalInput = stateController.horizontalInput;
        stateController._movementComponent.verticalInput = stateController.verticalInput;
        stateController._movementComponent.jumpInput = stateController.jumpInput;

        if (stateController.longDodgeInput || stateController.shortDodgeInput)
        {
            stateController._dodgeComponent.Dodge(stateController.shortDodgeInput);
            stateController.shortDodgeInput = false;
            stateController.longDodgeInput = false;
            return typeof(DodgeState);
        }

        return null;
    }
}
