using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    PlayerStateController stateController;
    
    private bool done = false;
    private int combo = 0;

    public AttackState(PlayerStateController controller) : base(controller.gameObject)
    {
        stateController = controller;
    }

    public override void Enter()
    {
        //stateController._hitboxComponent.gameObject.SetActive(true); /* Handled by animation events */
        Debug.Log("AttackState: Enter");
        combo = 0;
        Attack();
    }

    public override void Exit()
    {
        //stateController._hitboxComponent.gameObject.SetActive(false); /* Handled by animation events */
        stateController.quickAttackInput = false;
        stateController.heavyAttackInput = false;
        stateController.powerInput = 0;
        Debug.Log("AttackState: Exit");
    }

    public override Type Tick()
    {
        //Debug.Log("Attack State");
        switch (stateController._animHandler.attackState)
        {
            case 0:
                ClearAttackInputs();
                break;

            case 1:
                Attack();
                break;

            case 2:
                done = true;
                break;
        }
        //}

        if (done)
        {
            done = false;
            return typeof(MovementState);
        }

        if (stateController.Stunned)
        {
            return typeof(StunnedState);
        }

        return null;
    }

    private void Attack()
    {
        if (stateController.quickAttackInput && combo < 3)
        {
            Debug.Log("AttackState: Attack");
            combo++;
            ClearAttackInputs();
            stateController._animHandler.StartAttack(false, combo);
        }

        if (stateController.heavyAttackInput && combo < 3)
        {
            Debug.Log("AttackState: Attack");
            combo++;
            ClearAttackInputs();
            stateController._animHandler.StartAttack(true, combo);
        }

        if (stateController.powerInput > 0)
        {
            // run code from the power component
            done = stateController._powerComponent.UsingPower(stateController.powerInput);
        }
    }

    private void ClearAttackInputs()
    {
        stateController.quickAttackInput = false;
        stateController.heavyAttackInput = false;
        stateController.powerInput = 0;
    }
}
