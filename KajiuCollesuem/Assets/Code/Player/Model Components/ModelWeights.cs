using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelWeights : MonoBehaviour
{
    private ModelController _modelController;
    private Animator _anim;

    [SerializeField] [Range(0, 1)] private float stepWeight = 0;
    [SerializeField] [Range(0, 1)] private float jumpWeight = 0;
    [SerializeField] [Range(0, 1)] private float crouchWeight = 0;
    [SerializeField] [Range(0, 1)] private float upperbodyWeight = 0;
    [Range(0, 1)] private float upperbodyCurrentWeight = 0;
    [SerializeField] [Range(0, 1)] private float dodgeWeight = 0;
    [SerializeField] [Range(0, 1)] private float stunnedWeight = 0;
    [SerializeField] [Range(0, 1)] private float stunnedDirection = 0;
    [SerializeField] [Range(0, 1)] private float deadWeight = 0;
    [SerializeField] [Range(0, 1)] private float abilityWeight = 0;
    [SerializeField] [Range(0, 1)] private float tarJumpWeight = 0;

    [Space]
    [SerializeField] private float _weightChangeDampening = 10;
    [SerializeField] private float _weightChangeDeadzone = 0.01f;
    [SerializeField] private float _upperbodyWeightDampening = 10;

    private Coroutine crouchRoutineStored;
    private Coroutine stunnedRoutineStored;
    private Coroutine tarJumpRoutineStored;

    public void Init(ModelController pController, Animator pAnim)
    {
        _modelController = pController;
        _anim = pAnim;
    }

    public void UpdateWeights()
    {
        if (_modelController.onGround)
        {
            stepWeight = 1f;
            jumpWeight = 0;
        }
        else
        {
            stepWeight = 0;
            jumpWeight = 1f;
        }
    }

    // Lerp weight values
    public void LerpWeights()
    {
        LerpWeight("Stepping Weight", stepWeight - stunnedWeight);
        LerpWeight("Jumping Weight", jumpWeight - stunnedWeight);
        LerpWeight("Crouching Weight", crouchWeight - stunnedWeight);
        LerpUpperbodyWeight();
        LerpWeight("Dodge Weight", dodgeWeight);
        LerpWeight("Stunned Weight", stunnedWeight);
        LerpWeight("Stunned Direction", stunnedDirection);
        LerpWeight("Dead Weight", deadWeight);
        LerpWeight("Abilities Weight", abilityWeight);
        LerpWeight("TarJump Weight", tarJumpWeight);
    }

    public void SetUpperbodyWeight(float pWeight, float pDampening)
    {
        upperbodyWeight = pWeight;
        _upperbodyWeightDampening = pDampening;
    }

    private void LerpUpperbodyWeight()
    {
        upperbodyCurrentWeight = Mathf.Lerp(upperbodyCurrentWeight, upperbodyWeight, Time.deltaTime * _upperbodyWeightDampening);
        _anim.SetLayerWeight(1, upperbodyCurrentWeight);
    }

    private void LerpWeight(string pWeight, float pTargetValue, float pMultLerp = 1)
    {
        // Get current weight value
        float currentValue = _anim.GetFloat(pWeight);

        // Return if value is already at target
        if (currentValue == pTargetValue) return;

        // Lerp value towards target
        currentValue = Mathf.Lerp(currentValue, pTargetValue, _weightChangeDampening * Time.deltaTime * pMultLerp);

        // If in deadzone just snap to value
        if (Mathf.Abs(currentValue - pTargetValue) < _weightChangeDeadzone)
            currentValue = pTargetValue;

        // Update Anim
        _anim.SetFloat(pWeight, currentValue);
    }

    public void SetWeights(float pStepWeight, float pJumpWeight, float pDodgeWeight, float pDeadWeight, float pAbilityWeight = 0)
    {
        stepWeight = pStepWeight;
        jumpWeight = pJumpWeight;
        //crouchWeight = pCrouchWeight;
        //attackWeight = pAttackWeight;
        dodgeWeight = pDodgeWeight;
        deadWeight = pDeadWeight;
        abilityWeight = pAbilityWeight;
    }

    #region Crouch
    public void AddCrouching(float pValue, float pGoingToLength, float pGoingAwayLength)
    {
        if (crouchRoutineStored != null)
            StopCoroutine(crouchRoutineStored);
        crouchRoutineStored = StartCoroutine(crouchRoutine(pValue, pGoingToLength, pGoingAwayLength));
    }

    IEnumerator crouchRoutine(float pValue, float pGoingToLength, float pGoingAwayLength)
    {
        // Increase Value
        while (crouchWeight < pValue)
        {
            crouchWeight += Time.deltaTime * (1 / pGoingToLength);
            yield return null;
        }

        // Snap to target value for a frame
        crouchWeight = pValue;
        yield return null;

        // Decrease Value
        while (crouchWeight > 0)
        {
            crouchWeight -= Time.deltaTime * (1 / pGoingAwayLength);
            yield return null;
        }

        // Snap back to zero
        crouchWeight = 0;
    }
    #endregion

    #region Stun
    public void AddStunned(float pValue, float pDirection, float pGoingAwayDelay, float pGoingAwayLength)
    {
        stunnedDirection = pDirection;

        if (stunnedRoutineStored != null)
            StopCoroutine(stunnedRoutineStored);
        stunnedRoutineStored = StartCoroutine(stunnedRoutine(pValue, pGoingAwayDelay, pGoingAwayLength));
    }

    IEnumerator stunnedRoutine(float pValue, float pGoingAwayDelay, float pGoingAwayLength)
    {
        // Snap to target value for a delay
        stunnedWeight = pValue;
        yield return new WaitForSeconds(pGoingAwayDelay);

        // Decrease Value
        while (stunnedWeight > 0)
        {
            stunnedWeight -= Time.deltaTime * (1 / pGoingAwayLength);
            yield return null;
        }

        // Snap back to zero
        stunnedWeight = 0;
    }
    #endregion

    #region TarJump
    public void AddTarJump(float pValue, float pGoingToLength, float pGoingAwayDelay, float pGoingAwayLength)
    {
        if (tarJumpRoutineStored != null)
            StopCoroutine(tarJumpRoutineStored);
        tarJumpRoutineStored = StartCoroutine(tarJumpRoutine(pValue, pGoingToLength, pGoingAwayDelay, pGoingAwayLength));
    }

    IEnumerator tarJumpRoutine(float pValue, float pGoingToLength, float pGoingAwayDelay, float pGoingAwayLength)
    {
        // Increase Value
        while (tarJumpWeight < pValue)
        {
            tarJumpWeight += Time.deltaTime * (1 / pGoingToLength);
            yield return null;
        }

        // Snap to target value for a frame
        tarJumpWeight = pValue;
        yield return new WaitForSeconds(pGoingAwayDelay);

        // Decrease Value
        while (tarJumpWeight > 0)
        {
            tarJumpWeight -= Time.deltaTime * (1 / pGoingAwayLength);
            yield return null;
        }

        // Snap back to zero
        tarJumpWeight = 0;
    }
    #endregion
}
