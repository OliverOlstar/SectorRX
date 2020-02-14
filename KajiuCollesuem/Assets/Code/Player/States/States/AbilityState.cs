using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityState : BaseState
{
    PlayerStateController _stateController;

    private float _exitStateTime = 0;
    private float _abilityStateReturnDelayLength = 0.6f;
    private IAbility _curAbility;
    private SOAbilities _curSOAbility;

    public AbilityState(PlayerStateController controller) : base(controller.gameObject)
    {
        _stateController = controller;
    }

    public override void Enter()
    {
        Debug.Log("AbilityState: Enter");
        _stateController._movementComponent.disableMovement = true;

        CheckInputs();
    }

    public override void Exit()
    {
        Debug.Log("AbilityState: Exit");
        _stateController.AttackStateReturnDelay = Time.time + _abilityStateReturnDelayLength;
        _stateController._movementComponent.disableMovement = false;

        _curAbility.Exit();
        ClearInputs();
    }

    public override Type Tick()
    {
        // Stunned Or Dead
        Type stunnedOrDead = _stateController.stunnedOrDeadCheck();
        if (stunnedOrDead != null)
            return stunnedOrDead;

        // Leave state after attack
        if (Time.time > _exitStateTime)
        {
            return typeof(MovementState);
        }

        return null;
    }

    #region Inputs
    private void CheckInputs()
    {
        // ON PRESSED ABILITY 1 (Called Once)
        if (_stateController.ability1input == 1)
        {
            _curAbility = _stateController._AbilityScript1;
            _curSOAbility = _stateController._AbilitySO1;
            PressedAbility();
        }
        // ON PRESSED ABILITY 2 (Called Once)
        else if (_stateController.ability2input == 1)
        {
            _curAbility = _stateController._AbilityScript2;
            _curSOAbility = _stateController._AbilitySO2;
            PressedAbility();
        }

        // ON RELEASED ABILITY 1 || 2 (Called Once)
        else if (_stateController.ability1input == 2 || _stateController.ability2input == 2)
        {
            ReleasedAbility();
        }

        ClearInputs();
    }

    private void PressedAbility()
    {
        _exitStateTime = Time.time + _curSOAbility.abilityTime;
        _curAbility.Pressed();
    }

    private void ReleasedAbility()
    {
        _curAbility.Released();
    }
    #endregion

    #region Set & Clear
    private void SetAbilityValues(SOAbilities pAbilityVars)
    {
        _exitStateTime = Time.time + pAbilityVars.abilityTime;
    }

    private void ClearInputs()
    {
        _stateController.lightAttackinput = -1.0f;
        _stateController.heavyAttackinput = -1.0f;
        _stateController.ability1input = -1.0f;
        _stateController.ability2input = -1.0f;
    }
    #endregion
}