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
        Debug.Log("DodgeState: Enter");

        //Start Dodge
        stateController._dodgeComponent.Dodge(stateController.dodgeInput == 0, stateController.LastMoveDirection.normalized);

        //Remove Input
        stateController.clearAttackInputs();
    }

    public override void Exit()
    {
        Debug.Log("DodgeState: Exit");

        //Stop Dodge
        stateController._modelController.DoneDodge();

        //Remove Input
        stateController.dodgeInput = -1.0f;
    }

    public override Type Tick()
    {
        // Stunned Or Dead
        Type stunnedOrDead = stateController.stunnedOrDeadCheck();
        if (stunnedOrDead != null)
            return stunnedOrDead;

        // Leave Dodge
        if (stateController._dodgeComponent.doneDodge)
        {
            stateController._dodgeComponent.doneDodge = false;
            return typeof(MovementState);
        }

        // TODO Maybe add way to enter stunned state from here

        return null;
    }
}
