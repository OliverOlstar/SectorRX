using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    PlayerStateController stateController;


    public IdleState(PlayerStateController controller) : base(controller.gameObject)
    {
        stateController = controller;
    }

    public override Type Tick()
    {
        Debug.Log("Idle State");

        if(stateController.verticalInput != 0 || stateController.horizontalInput != 0)
        {
            return typeof(RunState);
        }

        return null;
    }
}
