using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MatchInputHandler : MonoBehaviour
{
    public PlayerStateController playerStateController;
    [SerializeField] private PauseMenu _PauseMenu;

    #region Inputs
    public void OnCamera(InputValue ctx) { if (playerStateController != null) playerStateController.OnCamera(ctx); }
    public void OnMovement(InputValue ctx) { if (playerStateController != null) playerStateController.OnMovement(ctx); }
    public void OnDodge(InputValue ctx) { if (playerStateController != null) playerStateController.OnDodge(ctx);}
    public void OnAbility1(InputValue ctx) { if (playerStateController != null) playerStateController.OnAbility1(ctx);}
    public void OnAbility2(InputValue ctx) { if (playerStateController != null) playerStateController.OnAbility2(ctx);}
    public void OnLightAttack(InputValue ctx) { if (playerStateController != null) playerStateController.OnLightAttack(ctx);}
    public void OnHeavyAttack(InputValue ctx) { if (playerStateController != null) playerStateController.OnHeavyAttack(ctx);}
    public void OnJump() { if (playerStateController != null) playerStateController.OnJump();}
    public void OnLockOn() { if (playerStateController != null) playerStateController.OnLockOn();}
    public void OnPause() { if (_PauseMenu != null && playerStateController != null) _PauseMenu.TogglePause();}
    public void OnAnyInput() { if (playerStateController != null) playerStateController.OnAnyInput();}
    #endregion
}
