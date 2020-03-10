using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityState : BaseState
{
    PlayerStateController _stateController;

    private float _exitStateTime = 0;
    private float _abilityStateReturnDelayLength = 0.8f;

    public AbilityState(PlayerStateController controller) : base(controller.gameObject)
    {
        _stateController = controller;
    }

    public override void Enter()
    {
        Debug.Log("AbilityState: Enter");
        _stateController.usingAbility = true;
        _stateController._movementComponent.undisableJump = true;

        SOAbilities AbilitySO = _stateController._modelController.abilitySO;
        _exitStateTime = Time.time + AbilitySO.holdStartPosTime + AbilitySO.abilityAnimTime + AbilitySO.holdEndPosTime;

        _stateController._AbilityScript.Pressed();
    }

    public override void Exit()
    {
        Debug.Log("AbilityState: Exit");
        _stateController.AttackStateReturnDelay = Time.time + _abilityStateReturnDelayLength;

        _stateController._movementComponent.undisableJump = false;

        _stateController._AbilityScript.Exit();
        ClearInputs();
    }

    public override Type Tick()
    {
        // Stunned Or Dead
        Type stunnedOrDead = _stateController.DeadCheck();
        if (stunnedOrDead != null)
            return stunnedOrDead;

        // Leave state after attack
        if (_stateController.usingAbility == false || (_stateController.dodgeInput != -1.0f && _stateController.moveRawInput != Vector2.zero) || Time.time > _exitStateTime)
        {
            return typeof(MovementState);
        }

        _stateController._AbilityScript.Tick();

        return null;
    }

    private void ClearInputs()
    {
        _stateController.lightAttackinput = -1.0f;
        _stateController.heavyAttackinput = -1.0f;
        _stateController.abilityinput = -1.0f;
    }
}