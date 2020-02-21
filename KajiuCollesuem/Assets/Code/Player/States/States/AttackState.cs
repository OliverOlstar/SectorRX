﻿using System;
using UnityEngine;

public class AttackState : BaseState
{
    PlayerStateController _stateController;

    private int _numberOfClicks = 0;
    private float _exitStateTime = 0;
    private float _addForceTime = 0;
    private float _stopForceTime = 0;
    private float _addForceAmount = 0;

    private float _enableHitboxTime = 0;
    private float _disableHitboxTime = 0;
    private PlayerHitbox _hitbox = null;

    private float _attackStateReturnDelayLength = 0.3f;
    private float _maxCharge = 1.0f;

    private bool _onHolding = false;
    public float chargeTimer = 0f;

    public AttackState(PlayerStateController controller) : base(controller.gameObject)
    {
        _stateController = controller;
    }

    public override void Enter()
    {
        Debug.Log("AttackState: Enter");
        _exitStateTime = 0;
        _onHolding = false;
        CheckForAttack();
    }

    public override void Exit()
    {
        Debug.Log("AttackState: Exit");
        _stateController.AttackStateReturnDelay = Time.time + _attackStateReturnDelayLength;
        _numberOfClicks = 0;

        // If leaving state before disabling hitbox, disable hitbox
        if (_hitbox != null)
        {
            _hitbox.gameObject.SetActive(false);
            _hitbox = null;
        }

        _stateController._modelController.SetInputDirection(Vector3.zero);
        ClearInputs();
    }

    public override Type Tick()
    {
        // Stunned Or Dead
        Type stunnedOrDead = _stateController.stunnedOrDeadCheck();
        if (stunnedOrDead != null)
            return stunnedOrDead;

        // Check for another attack
        CheckForAttack();

        // Leave state after attack
        if (Time.time > _exitStateTime && _onHolding == false)
        {
            return typeof(MovementState);
        }

        if (Time.time < _addForceTime)
            _stateController._modelController.SetInputDirection(_stateController.moveInput);

        // Forward stepping force
        if (Time.time > _addForceTime && Time.time < _stopForceTime && _onHolding == false)
        {
            _stateController._Rb.AddForce(_stateController._modelController.transform.forward * _addForceAmount * Time.deltaTime);
        }

        // Hitbox enable & disable
        if (_hitbox != null)
        {
            if (Time.time >= _disableHitboxTime)
            {
                _hitbox.gameObject.SetActive(false);
                _hitbox = null;
            }
            else if (Time.time >= _enableHitboxTime)
            {
                _hitbox.gameObject.SetActive(true);
            }
        }

        return null;
    }

    public void CheckForAttack()
    {
        // Cannot go beyond combo limit
        if (_numberOfClicks <= 2)
        {
            // If holding don't listen for more attacks, listen for release else run holding code.
            if (_onHolding == true)
            {
                // ON RELEASE HEAVY (Called Once)
                if (_stateController.heavyAttackinput == 0)
                {
                    //Debug.Log("AttackState: CheckForAttack - HEAVY RELEASED");
                    ReleaseHeavyAttack();
                }
                // ON HOLDING HEAVY
                else
                {
                    chargeTimer += Time.deltaTime;

                    // If Reached max charge release heavy attack
                    if (chargeTimer >= _maxCharge)
                    {
                        //Debug.Log("AttackState: CheckForAttack - HEAVY RELEASED Maxed Charge");
                        
                        // Automatically did a release input so ignore the actual input
                        _stateController.IgnoreNextHeavyRelease = true;
                        ReleaseHeavyAttack();
                    }
                }
            }
            // Can only input for next attack if done previous attack
            else if (Time.time > _exitStateTime)
            {
                // ON RELEASED HEAVY BEFORE CHARGING STARTED (Called Once)
                if (_stateController.heavyAttackinput == 0)
                {
                    //Debug.Log("AttackState: CheckForAttack - HEAVY RELEASED Before Charging");
                    PressedHeavyAttack();
                    ReleaseHeavyAttack();
                }
                // ON PRESSED HEAVY (Called Once)
                else if (_stateController.heavyAttackinput == 1)
                {
                    //Debug.Log("AttackState: CheckForAttack - HEAVY PRESSED");
                    PressedHeavyAttack();
                }
                // ON PRESSED LIGHT (Called Once)
                else if (_stateController.lightAttackinput == 1)
                {
                    //Debug.Log("AttackState: CheckForAttack - LIGHT PRESSED");
                    PressedLightAttack();
                }
            }
        }
    }

    #region Pressed & Release
    private void PressedLightAttack()
    {
        //Debug.Log("AttackState: PressedLightAttack");
        SOAttack curAttack = _stateController._modelController.attacks[_numberOfClicks];
        float PreAttackTime = curAttack.transitionToTime + curAttack.holdStartPosTime;
        SetAttackValues(curAttack, PreAttackTime);

        _stateController._modelController.PlayAttack(_numberOfClicks, false, false);

        _numberOfClicks++;
        ClearInputs();
    }

    private void PressedHeavyAttack()
    {
        //Debug.Log("AttackState: PressedHeavyAttack");
        _stateController._modelController.PlayAttack(_numberOfClicks, true, true);

        chargeTimer = 0;
        _onHolding = true;
        ClearInputs();
    }

    private void ReleaseHeavyAttack()
    {
        //Debug.Log("AttackState: ReleaseHeavyAttack");
        SOAttack curAttack = _stateController._modelController.attacks[_numberOfClicks + 3];
        SetAttackValues(curAttack); // TODO Add charging mult to hitbox

        // TODO send through how long attack was charged for and use that to know how fast the attack should move.
        _stateController._modelController.DoneChargingAttack();

        _onHolding = false;
        _numberOfClicks++;
        ClearInputs();
    }
    #endregion

    #region Set & Clear
    private void SetAttackValues(SOAttack pAttackVars, float pPreAttackTime = 0)
    {
        // Timings
        _exitStateTime = Time.time + pAttackVars.attackTime + pPreAttackTime + pAttackVars.holdEndPosTime;
        _addForceTime = Time.time + pAttackVars.forceForwardTime + pPreAttackTime;
        _stopForceTime = Time.time + pAttackVars.stopForceForwardTime + pPreAttackTime;
        _addForceAmount = pAttackVars.forceForwardAmount;

        // Hitbox
        _hitbox = _stateController.hitboxes[pAttackVars.hitboxIndex];
        Vector3 knockVector = _stateController._modelController.transform.forward * pAttackVars.HitboxKnockback + Vector3.up * pAttackVars.HitboxKnockup;
        _hitbox.SetDamage(pAttackVars.HitboxDamage, knockVector);

        _enableHitboxTime = Time.time + pAttackVars.enableHitboxTime + pPreAttackTime;
        _disableHitboxTime = Time.time + pAttackVars.disableHitboxTime + pPreAttackTime;
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