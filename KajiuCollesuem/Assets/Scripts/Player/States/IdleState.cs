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
            // TODO Insert code that deactivates root motion (if active) on the animator 
            return typeof(RunState);
        }

        if(stateController.shortDodgeInput || stateController.longDodgeInput)
        {
            // TODO Insert code that activates root motion on the animator 
            return typeof(DodgeState);
        }

        if(stateController.quickAttackInput || stateController.heavyAtatckInput)
        {
            // TODO Insert code that activates root motion on the animator
            return typeof(AttackState);
        }

        // if(health equal to 0)
            // return typeof(DeathState);

        // if(Stunned equals true){
            // return typeof(StunnedState);

        return null;
    }
}
