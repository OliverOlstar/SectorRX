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
    [HideInInspector] public float AbilityStateReturnDelay = 0;

    // Inputs
    [HideInInspector] public float LastInputTime = 0;
    [HideInInspector] public Vector2 mouseInput;
    [HideInInspector] public Vector3 moveInput = new Vector3(0, 0, 0);
    [HideInInspector] public Vector2 moveRawInput = new Vector2(0, 0);
    [HideInInspector] public Vector2 LastMoveDirection = new Vector2(0, 0);
    // 0 - Tapped, 1 - Held
    [HideInInspector] public float dodgeInput = -1.0f;
    [HideInInspector] public float ability1input = -1.0f;
    [HideInInspector] public float ability2input = -1.0f;
    [HideInInspector] public float lightAttackinput = -1.0f;
    // 0 - Released, 1 - Pressed
    [HideInInspector] public float heavyAttackinput = -1.0f;

    [Header("State Components")]
    [HideInInspector] public PlayerStateMachine _stateMachine;
    public MovementComponent _movementComponent { get; private set; } // Player's movement component, access this to move and jump
    [HideInInspector] public DodgeComponent _dodgeComponent; // Player's dodge component, access this to
    [HideInInspector] public PlayerLockOnScript _lockOnComponent;
    [HideInInspector] public PlayerAbilitySelector _powerComponent;
    [HideInInspector] public PlayerHitbox _hitboxComponent;
    [HideInInspector] public PauseMenu _PauseMenu;

    [HideInInspector] public PlayerAttributes _playerAttributes;
    [HideInInspector] public ModelController _modelController;
    [HideInInspector] public PlayerCamera _playerCamera;
    public RagdollManager _ragdollManager;

    [HideInInspector] public Rigidbody _Rb;
    public Transform _Camera;
    public PlayerHitbox[] hitboxes = new PlayerHitbox[0];

    // Abilities
    [HideInInspector] public IAbility _AbilityScript1;
    [HideInInspector] public IAbility _AbilityScript2;
    [HideInInspector] public SOAbilities _AbilitySO1;
    [HideInInspector] public SOAbilities _AbilitySO2;

    // Used if it auto calls release when reached maxCharge time & next release input is to be ignored
    [HideInInspector] public bool ignoreNextHeavyAttackRelease = false;

    //[HideInInspector] public InputPlayer inputs;
    //[HideInInspector] public InputAction.CallbackContext ctx;

    void Awake()
    {
        _movementComponent = GetComponent<MovementComponent>();
        _dodgeComponent = GetComponent<DodgeComponent>();
        _powerComponent = GetComponent<PlayerAbilitySelector>();
        _lockOnComponent = GetComponent<PlayerLockOnScript>();
        _hitboxComponent = GetComponentInChildren<PlayerHitbox>();
        _PauseMenu = transform.parent.GetComponentInChildren<PauseMenu>();

        _playerAttributes = GetComponent<PlayerAttributes>();
        _modelController = GetComponentInChildren<ModelController>();

        _stateMachine = GetComponent<PlayerStateMachine>();
        InitializeStateMachine();

        _Rb = GetComponent<Rigidbody>();
        _playerCamera = _Camera.GetComponentInParent<PlayerCamera>();

        // Add Abilities
        GetComponent<PlayerAbilitySelector>().SetupAbilities(this);
    }

    #region Inputs
    // List for inputs
    private void OnCamera(InputValue ctx) => mouseInput = ctx.Get<Vector2>();
    private void OnMovement(InputValue ctx) => moveRawInput = ctx.Get<Vector2>();
    private void OnDodge(InputValue ctx) => dodgeInput = ctx.Get<float>();
    private void OnAbility1(InputValue ctx)
    {
        // AbilityState is on cooldown
        if (AbilityStateReturnDelay > Time.time)
            return;

        ability1input = ctx.Get<float>();
    }
    private void OnAbility2(InputValue ctx)
    {
        // AbilityState is on cooldown
        if (AbilityStateReturnDelay > Time.time)
            return;

        ability2input = ctx.Get<float>();
    }
    private void OnLightAttack(InputValue ctx)
    {
        // AttackState is on cooldown
        if (AttackStateReturnDelay > Time.time)
            return;

        lightAttackinput = ctx.Get<float>();
    }
    private void OnHeavyAttack(InputValue ctx)
    {
        // Already played heavy release so ignore the next one
        if (ignoreNextHeavyAttackRelease == true && heavyAttackinput == 0)
        {
            ignoreNextHeavyAttackRelease = false;
            return;
        }

        // AttackState is on cooldown
        if (AttackStateReturnDelay > Time.time)
            return;

        heavyAttackinput = ctx.Get<float>();
    }
    private void OnPause() => _PauseMenu.TogglePause();
    private void OnAnyInput() => LastInputTime = Time.time;
    #endregion

    public Type stunnedOrDeadCheck()
    {
        //Dead
        if (_playerAttributes.getHealth() <= 0)
        {
            return typeof(DeathState);
        }

        //Stunned
        if (Stunned)
        {
            return typeof(StunnedState);
        }

        return null;
    }

    private void Update()
    {
        RotateMoveInputToCamera();
    }

    private void FixedUpdate()
    {
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
            {typeof(AbilityState), new AbilityState(controller:this) },
            {typeof(DeathState), new DeathState(controller:this) }
        };

        _stateMachine.SetStates(states);
    }

    
}
