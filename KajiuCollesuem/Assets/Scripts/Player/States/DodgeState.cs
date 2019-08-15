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

    }

    public override void Exit()
    {

    }

    public override Type Tick()
    {
        Debug.Log("Dodge State");

        if (stateController._dodgeComponent.doneDodge)
        {
            stateController._dodgeComponent.doneDodge = false;
            return typeof(RunState);
        }

        return null;
    }
}
