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
        Vector3 vec = new Vector3(stateController.movementDir.x, 0, stateController.movementDir.z).normalized;

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
