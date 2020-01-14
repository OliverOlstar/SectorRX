﻿using System;
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

        //Dodge Direction
        Vector2 direction = stateController.LastMoveDirection.normalized;

        //Start Dodge
        stateController._dodgeComponent.Dodge(stateController.dodgeInput == 0, direction);
        stateController._animHandler.StartDodge();

        //Remove Input
        stateController.dodgeInput = -1.0f;
    }

    public override void Exit()
    {
        Debug.Log("DodgeState: Exit");

        //Stop Dodge
        stateController._animHandler.StopDodge();

        //Remove Input
        stateController.dodgeInput = -1.0f;
    }

    public override Type Tick()
    {
        // Leave Dodge
        if (stateController._dodgeComponent.doneDodge)
        {
            stateController._dodgeComponent.doneDodge = false;
            return typeof(MovementState);
        }

        // Respawn
        if (stateController.Respawn)
        {
            stateController.Respawn = false;
            //stateController._animHandler.Respawn();
            stateController._dodgeComponent.StopDodge();
            return typeof(MovementState);
        }

        // TODO Maybe add way to enter stunned state from here

        return null;
    }
}
