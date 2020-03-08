using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : BaseState
{
    PlayerStateController _stateController;

    public DodgeState(PlayerStateController controller) : base(controller.gameObject)
    {
        _stateController = controller;
    }

    public override void Enter()
    {
        //Debug.Log("DodgeState: Enter");
        _stateController._movementComponent.disableMovement = true;

        // Start Dodge
        _stateController._dodgeComponent.Dodge(_stateController.dodgeInput == 0, _stateController.LastMoveDirection.normalized);
    }

    public override void Exit()
    {
        //Debug.Log("DodgeState: Exit");
        _stateController._movementComponent.disableMovement = false;

        // Stop Dodge
        _stateController._modelController.DoneDodge();

        // Remove Input
        _stateController.dodgeInput = -1.0f;
    }

    public override Type Tick()
    {
        // Stunned Or Dead
        Type stunnedOrDead = _stateController.DeadCheck();
        if (stunnedOrDead != null)
            return stunnedOrDead;

        // Leave Dodge
        if (_stateController._dodgeComponent.doneDodge)
        {
            _stateController._dodgeComponent.doneDodge = false;
            return typeof(MovementState);
        }

        // TODO Maybe add way to enter stunned state from here

        return null;
    }
}
