using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunnedState : BaseState
{
    PlayerStateController stateController;

     private float stunnedLength = 0.8f;
     private float timer = 0;

    public StunnedState(PlayerStateController controller) : base(controller.gameObject)
    {
        stateController = controller;
    }

    public override void Enter()
    {
        timer = 0;
        stateController._animHandler.Stunned(true);
        stateController.Stunned = false;
    }

    public override void Exit()
    {
         
    }

    public override Type Tick()
    {
        timer += Time.deltaTime;

        if (timer >= stunnedLength)
        {
            return typeof(MovementState);
        }

        //Respawn
        if (stateController.Respawn)
        {
            stateController.Respawn = false;
            stateController._animHandler.Respawn();
            return typeof(MovementState);
        }

        return null;
    }
}
