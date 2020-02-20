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
        Debug.Log("DeathState: Enter");
        //stateController._respawnComponent.Dead();
        //stateController._modelController.Dead();
        //_stateController._ragdollManager.SwitchToRagdoll();
        _stateController._modelController.PlayDead();
        _stateController._movementComponent.disableMovement = true;
        _stateController.GetComponent<Collider>().material.bounciness = 1.0f;
        _stateController.GetComponentInChildren<Collider>().material.bounciness = 1.0f;
    }

    public override void Exit()
    {
        Debug.Log("DeathState: Exit");
        //stateController._modelController.Respawn();
    }

    public override Type Tick()
    {
        return null;
    }
}
