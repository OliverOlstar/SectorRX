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

    private int _Index = 0;
    private bool _RequestedToExit = false;

    public AbilityState(PlayerStateController controller) : base(controller.gameObject)
    {
        _stateController = controller;
    }

    public override void Enter()
    {
        Debug.Log("AbilityState: Enter");
        _RequestedToExit = false;

        CheckForPress();
    }

    public override void Exit()
    {
        Debug.Log("AbilityState: Exit");
        _stateController.AttackStateReturnDelay = Time.time + _abilityStateReturnDelayLength;

        _curAbility.Exit();
        ClearInputs();
    }

    public override Type Tick()
    {
        _curAbility.Tick();

        // Stunned Or Dead
        Type stunnedOrDead = _stateController.stunnedOrDeadCheck();
        if (stunnedOrDead != null)
            return stunnedOrDead;

        // Leave state after attack
        if (_RequestedToExit || Time.time > _exitStateTime)
        {
            return typeof(MovementState);
        }

        CheckForRelease();

        return null;
    }

    #region Inputs
    private void CheckForPress()
    {
        // ON PRESSED ABILITY 1 (Called Once)
        if (_stateController.ability1input == 1)
        {
            _Index = 1;
            _curAbility = _stateController._AbilityScript1;
            _curSOAbility = _stateController._modelController.abilities[0];
            PressedAbility();
        }
        // ON PRESSED ABILITY 2 (Called Once)
        else if (_stateController.ability2input == 1)
        {
            _Index = 2;
            _curAbility = _stateController._AbilityScript2;
            _curSOAbility = _stateController._modelController.abilities[1];
            PressedAbility();
        }
    }

    private void CheckForRelease()
    {
        // ON RELEASED ABILITY 1 || 2 (Called Once)
        if ((_stateController.ability1input == 0 && _Index == 1) 
         || (_stateController.ability2input == 0 && _Index == 2))
        {
            ReleasedAbility();
            _Index = 0;
        }
    }

    private void PressedAbility()
    {
        _exitStateTime = Time.time + _curSOAbility.abilityTime;
        _curAbility.Pressed(this);
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

    public void RequestExitState()
    {
        _RequestedToExit = true;
    }
}