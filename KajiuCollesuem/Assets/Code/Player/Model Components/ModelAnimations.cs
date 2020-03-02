using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelAnimations : MonoBehaviour
{
    private ModelController _modelController;
    private Rigidbody _rb;
    private Animator _anim;

    [Header("Animation Lengths (stepMult is overriden by playerCollectables")]
    public float stepMult = 1;
    [SerializeField] private float _idleMult = 1;
    [SerializeField] private float _fallMult = 1;
    [SerializeField] private float _deadMult = 1;

    private float _stepProgress = 0;
    private float _idleProgress = 0;
    private float _fallProgress = 0;
    private float _attackProgress = 0;
    private float _deadProgress = 0;

    [Header("Interpolation Graphs")]
    [SerializeField] private AnimationCurve _stepGraph;
    [SerializeField] private AnimationCurve _idleGraph;
    [SerializeField] private AnimationCurve _fallGraph;
    [SerializeField] private AnimationCurve _deadGraph;
    private AnimationCurve _attackGraph = new AnimationCurve(new Keyframe(0, 0, 0, 0), new Keyframe(1, 1, 0, 0));
    
    [Space]
    [SerializeField] private float _jumpingTransitionWidth = 1;

    //Attack
    private float _attackLength = 1;
    //private float _attackTransitionRate = 0;

    //private int _doneAttackTransition = 0;

    private float _SecondStep = 0.0f;

    public void Init(ModelController pController, Rigidbody pRb, Animator pAnim)
    {
        _modelController = pController;
        _rb = pRb;
        _anim = pAnim;
    }

    #region Attacking
    // True = go towards 1, False = towards 0
    public bool AttackingAnim()
    {
        // Return if still transtioning to next attack
        //if (_doneAttackTransition < 2)
        //    return false;

        // Increase progress value
        _attackProgress = Mathf.Min(1, _attackProgress + Time.fixedDeltaTime / _attackLength);

        // Set progress value
        _anim.SetFloat("Attacking Progress", _attackGraph.Evaluate(_attackProgress));

        // Return true if reached anim goal
        return _attackProgress == 1;
    }

    public void StartAttack(int pIndex)
    {
        // Reset attack progress
        _attackProgress = 0;
        _anim.SetFloat("Attacking Progress", 0);

        // Get SOAttack values
        SOAttack curAttack = _modelController.attacks[pIndex];
        _attackLength = curAttack.attackTime;
        _attackGraph = curAttack.attackGraph;

        // Snap to first attack
        _anim.SetFloat("Attacking Index", pIndex);
        //_doneAttackTransition = 2;
    }

    public void StartAbility(int pIndex)
    {
        // Reset attack progress
        _attackProgress = 0;
        _anim.SetFloat("Attacking Progress", 0);

        // Get SOAttack values
        SOAbilities curAbility = _modelController.abilities[pIndex];
        _attackLength = curAbility.abilityTime;
        _attackGraph = curAbility.abilitiesGraph;

        // Snap to first attack
        _anim.SetFloat("Attacking Index", pIndex + 3);
        //_doneAttackTransition = 2;
    }

    // Transition between attacks in combo
    //private IEnumerator AttackingIndexTransition(float pIndex)
    //{
    //    // Starting Value
    //    float curIndex = _anim.GetFloat("Attacking Index");

    //    // Transition to Target Value
    //    while (curIndex < pIndex)
    //    {
    //        curIndex += _attackTransitionRate * Time.deltaTime;
    //        _anim.SetFloat("Attacking Index", curIndex);
    //        yield return null;
    //    }

    //    // Snap to Target Value
    //    _anim.SetFloat("Attacking Index", pIndex);
    //    _doneAttackTransition++;
    //}

    //// Transition between heavy & light attacks
    //private IEnumerator AttackingTypeTransition(float pType)
    //{
    //    // Starting Value
    //    float curType = _anim.GetFloat("Attacking Type");

    //    // Transition to Target Value
    //    while ((curType < pType && pType == 1) || (curType > pType && pType == 0))
    //    {
    //        curType += _attackTransitionRate * Time.deltaTime * (pType == 0 ? -1 : 1);
    //        _anim.SetFloat("Attacking Type", curType);
    //        yield return null;
    //    }

    //    // Snap to Target Value
    //    _anim.SetFloat("Attacking Type", pType);
    //    _doneAttackTransition++;
    //}
    #endregion

    #region Jumping
    public void JumpingAnim()
    {
        // Jumping Speed
        _anim.SetFloat("Jumping Speed", _rb.velocity.y / _jumpingTransitionWidth);

        // Increase Falling Animation
        _fallProgress = increaseProgress(_fallProgress, _fallMult);
        _anim.SetFloat("Falling Progress", _fallGraph.Evaluate(_fallProgress));
    }
    #endregion

    #region Stepping
    public void SteppingAnim()
    {
        // Increase Stepping Animation
        float steppingSpeed = _modelController.horizontalVelocity.magnitude / GetComponentInParent<MovementComponent>().maxSpeed;
        _stepProgress += Time.fixedDeltaTime * stepMult;

        if (_stepProgress >= 1.0f)
        {
            _stepProgress -= 1.0f;
            _SecondStep = (_SecondStep == 0.0f) ? 0.5f : 0.0f;

            // Shake & Sound
            if (steppingSpeed != 0.0f)
                _modelController.TookStep(steppingSpeed);

        }

        // Set Anim Stepping values
        _anim.SetFloat("Stepping Progress", _stepGraph.Evaluate(_stepProgress) / 2 + _SecondStep);
        //_anim.SetFloat("Stepping Progress", (_stepGraph.Evaluate(time) / 2) + secondStep);
        _anim.SetFloat("Stepping Speed", (steppingSpeed > 1) ? 1 : steppingSpeed);

        // Set Anim Direction
        Vector3 relDirection = transform.rotation * new Vector3(-_modelController.horizontalVelocity.x, 0, _modelController.horizontalVelocity.z);
        relDirection.y = 0;
        relDirection.Normalize();

        _anim.SetFloat("Move Direction X", relDirection.x);
        _anim.SetFloat("Move Direction Z", relDirection.z);
    }

    public void IdleAnim()
    {
        // Increase Falling Animation
        _idleProgress = increaseProgress(_idleProgress, _idleMult);
        _anim.SetFloat("Idle Progress", _idleGraph.Evaluate(_idleProgress));
    }
    #endregion

    #region Dead
    public void DeadAnim()
    {
        // Increase Falling Animation
        _deadProgress = increaseProgress(_deadProgress, _deadMult, false);
        _anim.SetFloat("Dead Progress", _deadGraph.Evaluate(_deadProgress));
    }

    public void PlayDead()
    {
        _deadProgress = 0;
    }
    #endregion

    private float increaseProgress(float pProgress, float pMult, bool pLoop = true)
    {
        pProgress += Time.fixedDeltaTime * pMult;
        
        if (pProgress >= 1)
        {
            if (pLoop)
            {
                pProgress -= 1;
            }
            else
            {
                return 1;
            }
        }

        return pProgress;
    }
}
