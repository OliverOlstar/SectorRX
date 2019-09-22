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
        Vector3 vec = stateController._Camera.TransformDirection(stateController.movementDir);
        vec.y = 0;
        vec.Normalize();

        if (stateController._dodgeComponent.Dodge(stateController.shortDodgeInput, vec))
            stateController._animHandler.StartDodge(vec);

        stateController.shortDodgeInput = false;
        stateController.longDodgeInput = false;
    }

    public override void Exit()
    {
        stateController._animHandler.StopDodge();
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
