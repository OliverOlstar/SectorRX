using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MatchInputHandler : MonoBehaviour
{
    public PlayerStateController playerStateController;

    #region Inputs
    public void OnCamera(InputValue ctx) => playerStateController.OnCamera(ctx);
    public void OnMovement(InputValue ctx) => playerStateController.OnMovement(ctx);
    public void OnDodge(InputValue ctx) => playerStateController.OnDodge(ctx);
    public void OnAbility1(InputValue ctx) => playerStateController.OnAbility1(ctx);
    public void OnAbility2(InputValue ctx) => playerStateController.OnAbility2(ctx);
    public void OnLightAttack(InputValue ctx) => playerStateController.OnLightAttack(ctx);
    public void OnHeavyAttack(InputValue ctx) => playerStateController.OnHeavyAttack(ctx);
    public void OnJump() => playerStateController.OnJump();
    public void OnLockOn() => playerStateController.OnLockOn();
    public void OnPause() => playerStateController.OnPause();
    public void OnAnyInput() => playerStateController.OnAnyInput();
    #endregion
}
