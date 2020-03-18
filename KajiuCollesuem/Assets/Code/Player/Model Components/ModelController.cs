using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    private Rigidbody _rb;
    private Animator _anim;

    private PlayerStateController _stateController;

    [HideInInspector] public bool onGround = false;
    [HideInInspector] public Vector3 acceleration;

    private ModelWeights _modelWeights;
    private ModelAnimations _modelAnimation;
    private ModelMovement _modelMovement;

    [HideInInspector] public Vector3 horizontalVelocity;
    // 0 - done attack, 1 - attacking, 2 - sitting on a delay between attacking
    private int _AttackingState;

    private bool _DontUpdateWeights;

    public SOAttack[] attacks = new SOAttack[3];
    public SOAbilities abilitySO;
    private float _doneAttackDelay = 0;
    
    void Start()
    {
        // Get Model Components
        _modelWeights = GetComponent<ModelWeights>();
        _modelAnimation = GetComponent<ModelAnimations>();
        _modelMovement = GetComponent<ModelMovement>();

        // Get Other Components
        _stateController = GetComponentInParent<PlayerStateController>();
        _rb = GetComponentInParent<Rigidbody>();
        _anim = GetComponent<Animator>();

        // Setup Components
        _modelWeights.Init(this, _anim);
        _modelAnimation.Init(this, _rb, _anim, _stateController._movementComponent);
        _modelMovement.Init(this);
    }

    void FixedUpdate()
    {
        horizontalVelocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);

        if (_AttackingState == 1)
        {
            if (_modelAnimation.AttackingAnim())
            {
                StartCoroutine("DoneAttackWithDelay");
            }
        }

        if (_DontUpdateWeights == false)
        {

            _modelWeights.UpdateWeights();
        }
        else
        {
            _modelAnimation.DeadAnim();
        }

        _modelWeights.LerpWeights();

        _modelMovement.TiltingParent();
        _modelMovement.FacingSelf();

        _modelAnimation.SteppingAnim();
        _modelAnimation.IdleAnim();
        _modelAnimation.JumpingAnim();
        _modelAnimation.TarJumpAnim();
    }

    #region Abilities
    public void TransitionToAbility()
    {
        _modelWeights.SetWeights(0, 0, 0, 0, 1);
        _DontUpdateWeights = true;
    }

    public void PlayAbility()
    {
        StartCoroutine("PlayAttackWithDelay", abilitySO.holdStartPosTime);

        _doneAttackDelay = 99999999;
        _modelAnimation.StartAbility();
    }

    public void DoneAbility()
    {
        StopCoroutine("PlayAttackWithDelay");
        _modelMovement.disableRotation = false;
        _modelWeights.SetWeights(0, 0, 0, 0, 0);
        _DontUpdateWeights = false;
    }
    #endregion

    #region Attacking
    public void PlayAttack(int pIndex, bool pChargable)
    {
        SOAttack curAttack = attacks[pIndex];

        StopCoroutine("DoneAttackWithDelay");
        StopCoroutine("PlayAttackWithDelay");

        _modelMovement.disableRotation = true;

        // Chargeable Attack - wait for done charging before starting attack
        if (pChargable == true)
        {
            _AttackingState = 2;
        }
        // Non-Chargable Attack
        else
        {
            StartCoroutine("PlayAttackWithDelay", curAttack.holdStartPosTime);
        }

        _doneAttackDelay = curAttack.holdEndPosTime;
        _modelWeights.SetUpperbodyWeight(1, 15);
        _modelAnimation.StartAttack(pIndex);
    }

    public void DoneAttack()
    {
        _AttackingState = 0;
        _modelMovement.disableRotation = false;
        _modelWeights.SetUpperbodyWeight(0, 5);
    }

    public void DoneChargingAttack()
    {
        // End Charging
        _AttackingState = 1;
        _modelMovement.disableRotation = true;
    }

    public void OverChargedAttack()
    {
        _AttackingState = 0;
        _modelMovement.disableRotation = false;
        _modelWeights.SetUpperbodyWeight(0.0f, 3.7f);
    }

    // Coroutines
    private IEnumerator PlayAttackWithDelay(float pDelay)
    {
        _AttackingState = 2;
        yield return new WaitForSeconds(pDelay);
        _modelMovement.disableRotation = true;
        _AttackingState = 1;
    }

    private IEnumerator DoneAttackWithDelay()
    {
        _AttackingState = 2;
        yield return new WaitForSeconds(_doneAttackDelay);
        DoneAttack();
    }

    // Set rotation
    public void SetInputDirection(Vector3 pInput)
    {
        _modelMovement.facingInput = pInput;
    }
    #endregion

    #region Locomotion
    public void TookStep(float pShakeForce, bool pLeft)
    {
        if (_stateController._movementComponent.disableMovement == false && _stateController.groundMaterial != -1 && pShakeForce > 0.5f)
        {
            _stateController._CameraShake.PlayShake(pShakeForce * 1.1f, 4.0f, 0.05f, 0.2f);
            _stateController._Sound.Walking(_stateController.groundMaterial);
            _stateController._Particles.TookStep(pLeft);
        }
    }

    public void PlayDodge(Vector2 pDirection, float pSpeed)
    {
        _DontUpdateWeights = true;
        _modelWeights.SetWeights(0, 0, 1);
        _modelMovement.PlayFlipParent(pDirection, pSpeed);
    }

    public void DoneDodge()
    {
        _DontUpdateWeights = false;
        _modelWeights.SetWeights(0, 0, 0);
    }
    #endregion

    #region Crouching
    public void AddCrouching(float pValue, float pGoingToLength, float pGoingAwayLength)
    {
        _modelWeights.AddCrouching(pValue, pGoingToLength, pGoingAwayLength);
    }
    #endregion

    #region Stunned
    public void AddStunned(float pValue, float pDirection, float pGoingAwayDelay, float pGoingAwayLength)
    {
        _modelWeights.AddStunned(pValue, pDirection, pGoingAwayDelay, pGoingAwayLength);
    }
    #endregion

    #region TarJump
    public void AddTarJump(float pValue, float pGoingToLength, float pGoingAwayDelay, float pGoingAwayLength)
    {
        _modelWeights.AddTarJump(pValue, pGoingToLength, pGoingAwayDelay, pGoingAwayLength);
    }
    #endregion

    #region LockOn
    public void SetLockOn(Transform pTarget)
    {
        _modelMovement.facingTarget = pTarget;
    }
    #endregion

    #region Dead
    public void PlayDead()
    {
        _DontUpdateWeights = true;
        _modelMovement.disableRotation = true;
        _modelWeights.SetWeights(0, 0, 0, 1);
        _modelAnimation.PlayDead();
    }
    #endregion
}
