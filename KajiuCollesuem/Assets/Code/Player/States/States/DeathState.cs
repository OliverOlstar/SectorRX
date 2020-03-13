using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : BaseState
{
    PlayerStateController _stateController;

    public DeathState(PlayerStateController controller) : base(controller.gameObject)
    {
        _stateController = controller;
    }

    public override void Enter()
    {
        _stateController._modelController.PlayDead();
        _stateController._movementComponent.disableMovement = true;
        _stateController.GetComponent<Collider>().material.bounciness = 1.0f;
        _stateController.GetComponentInChildren<Collider>().material.bounciness = 1.0f;
    }

    public override void Exit()
    {

    }

    public override Type Tick()
    {
        return null;
    }
}
