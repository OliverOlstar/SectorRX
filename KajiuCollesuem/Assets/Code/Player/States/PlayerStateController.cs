using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerStateController : MonoBehaviour
{
    //THIS IS THE STATE MANAGER - Danish, Oliver

         /*
         This script is to change the player's active state 
         depending on the inputs received. 
         */

    // On Ground
    [HideInInspector] public bool onGround = false;
    [HideInInspector] public bool Stunned = false;
    [HideInInspector] public float AttackStateReturnDelay = 0;

    // Inputs
    [HideInInspector] public float LastInputTime = 0;
    [HideInInspector] public Vector2 mouseInput;
    [HideInInspector] public Vector3 moveInput = new Vector3(0, 0, 0);
    [HideInInspector] public Vector2 moveRawInput = new Vector2(0, 0);
    [HideInInspector] public Vector2 LastMoveDirection = new Vector2(0, 0);
    // 0 - Tapped, 1 - Held
    [HideInInspector] public float dodgeInput = -1.0f;
    [HideInInspector] public float lightAttackinput = -1.0f;
    // 0 - Released, 1 - Pressed
    [HideInInspector] public float heavyAttackinput = -1.0f;

    [Header("State Components")]
    [HideInInspector] public PlayerStateMachine _stateMachine;
    public MovementComponent _movementComponent { get; private set; } // Player's movement component, access this to move and jump
    [HideInInspector] public DodgeComponent _dodgeComponent; // Player's dodge component, access this to
    //private PlayerLockOnScript _lockOnComponent;
    [HideInInspector] public PlayerPowerHandler _powerComponent;
    [HideInInspector] public PlayerHitbox _hitboxComponent;
    [HideInInspector] public PauseMenu _PauseMenu;
    [HideInInspector] public HUDManager _Upgrade;

    [HideInInspector] public PlayerAttributes _playerAttributes;
    [HideInInspector] public ModelController _modelController;
    [HideInInspector] public PlayerCamera _playerCamera;

    [HideInInspector] public Rigidbody _Rb;
    public Transform _Camera;
    public PlayerHitbox[] hitboxes = new PlayerHitbox[0];


    // Used if it auto calls release when reached maxCharge time & next release input is to be ignored
    [HideInInspector] public bool ignoreNextHeavyAttackRelease = false;

    //[HideInInspector] public InputPlayer inputs;
    //[HideInInspector] public InputAction.CallbackContext ctx;

    void Awake()
    {
        _movementComponent = GetComponent<MovementComponent>();
        _dodgeComponent = GetComponent<DodgeComponent>();
        //_lockOnComponent = GetComponent<PlayerLockOnScript>();
        _powerComponent = GetComponent<PlayerPowerHandler>();
        _hitboxComponent = GetComponentInChildren<PlayerHitbox>();
        _PauseMenu = transform.parent.GetComponentInChildren<PauseMenu>();
        _Upgrade = transform.parent.GetComponentInChildren<HUDManager>();
        //_hitboxComponent.gameObject.SetActive(false);
        //_modelController = GetComponentInChildren<ModelMovement>();

        //_respawnComponent = GetComponent<PlayerRespawn>();
        _playerAttributes = GetComponent<PlayerAttributes>();
        _modelController = GetComponentInChildren<ModelController>();

        _stateMachine = GetComponent<PlayerStateMachine>();
        InitializeStateMachine();

        _Rb = GetComponent<Rigidbody>();
        _playerCamera = _Camera.GetComponentInParent<PlayerCamera>();
    }

    // List for inputs
    private void OnCamera(InputValue ctx) => mouseInput = ctx.Get<Vector2>();
    private void OnMovement(InputValue ctx) => moveRawInput = ctx.Get<Vector2>();
    private void OnDodge(InputValue ctx) => dodgeInput = ctx.Get<float>();
    private void OnLightAttack(InputValue ctx) => lightAttackinput = ctx.Get<float>();
    private void OnHeavyAttack(InputValue ctx)
    {
        heavyAttackinput = ctx.Get<float>();

        if (ignoreNextHeavyAttackRelease == true && heavyAttackinput == 0)
        {
            heavyAttackinput = -1.0f;
            ignoreNextHeavyAttackRelease = false;
        }
    }
    private void OnPause() => _PauseMenu.TogglePause();
    private void OnAnyInput() => LastInputTime = Time.time;

    private void OnUpgrade() => _Upgrade.OpenUpgrade();

    private void FixedUpdate()
    {
        RotateMoveInputToCamera();

        if (moveInput.magnitude != 0 || mouseInput.magnitude != 0)
        {
            LastInputTime = Time.time;
        }
    }

    public void RotateMoveInputToCamera()
    {
        moveInput = new Vector3(moveRawInput.x, 0, moveRawInput.y);
        moveInput = _Camera.TransformDirection(moveInput);
        moveInput.y = 0;
        moveInput.Normalize();
    }

    void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            {typeof(IdleState), new IdleState(controller:this) },
            {typeof(MovementState), new MovementState(controller:this) },
            {typeof(DodgeState), new DodgeState(controller:this) },
            {typeof(StunnedState), new StunnedState(controller:this) },
            {typeof(AttackState), new AttackState(controller:this) },
            {typeof(DeathState), new DeathState(controller:this) }
        };

        _stateMachine.SetStates(states);
    }

    
}
