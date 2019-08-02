using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnState : BaseState
{
    PlayerStateController stateController;

    public LockOnState(PlayerStateController controller) : base(controller.gameObject)
    {
        stateController = controller;
    }

    public override Type Tick()
    {
        Debug.Log("Lock On State");


        return null;
    }
}
