using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : BaseState
{
    PlayerStateController stateController;

    public DodgeState(PlayerStateController controller) : base(controller.gameObject)
    {
        stateController = controller;
    }

    public override void Enter()
    {
        stateController._dodgeComponent.Dodge(stateController.shortDodgeInput, stateController._movementComponent.moveDirection);
        stateController.shortDodgeInput = false;
        stateController.longDodgeInput = false;
    }

    public override void Exit()
    {

    }

    public override Type Tick()
    {
        //Debug.Log("Dodge State");

        if (stateController._dodgeComponent.doneDodge)
        {
            stateController._dodgeComponent.doneDodge = false;
            return typeof(MovementState);
        }

        return null;
    }
}
