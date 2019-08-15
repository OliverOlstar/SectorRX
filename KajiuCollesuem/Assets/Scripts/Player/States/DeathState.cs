using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : BaseState
{
    PlayerStateController stateController;

    public DeathState(PlayerStateController controller) : base(controller.gameObject)
    {
        stateController = controller;
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override Type Tick()
    {
        Debug.Log("Death State");

        return null;
    }
}
