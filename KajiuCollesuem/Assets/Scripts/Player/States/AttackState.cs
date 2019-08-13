using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    PlayerStateController stateController;

    float actionDelay = 0.3f;
    float timer = 0;

    public AttackState(PlayerStateController controller) : base(controller.gameObject)
    {
        stateController = controller;
    }

    public override Type Tick()
    {
        Debug.Log("Attack State");
        // Stop Coroutine from running
        if (stateController.quickAttackInput)
        {
            if(timer == 0)
            {
                // Start coroutine to start a timer
                // run code from attack component
            }
        }

        if (stateController.heavyAtatckInput)
        {
            if(timer == 0)
            {
                // Start Coroutine to start a timer
                // run code from the attack component
            }
        }


        return null;
    }
}
