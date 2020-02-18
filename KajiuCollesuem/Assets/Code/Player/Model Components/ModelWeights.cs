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
    [SerializeField] [Range(0, 1)] private float attackWeight = 0;
    [SerializeField] [Range(0, 1)] private float dodgeWeight = 0;
    [SerializeField] [Range(0, 1)] private float deadWeight = 0;

    [Space]
    [SerializeField] private float _weightChangeDampening = 10;
    [SerializeField] private float _weightChangeDeadzone = 0.01f;

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
        LerpWeight("Stepping Weight", stepWeight);
        LerpWeight("Jumping Weight", jumpWeight);
        LerpWeight("Crouching Weight", crouchWeight);
        LerpWeight("Attacking Weight", attackWeight);
        LerpWeight("Dodge Weight", dodgeWeight);
        LerpWeight("Dead Weight", deadWeight);
    }

    private void LerpWeight(string pWeight, float pTargetValue)
    {
        // Get current weight value
        float currentValue = _anim.GetFloat(pWeight);

        // Return if value is already at target
        if (currentValue == pTargetValue) return;

        // Lerp value towards target
        currentValue = Mathf.Lerp(currentValue, pTargetValue, _weightChangeDampening * Time.deltaTime);

        // If in deadzone just snap to value
        if (Mathf.Abs(currentValue - pTargetValue) < _weightChangeDeadzone)
            currentValue = pTargetValue;

        // Update Anim
        _anim.SetFloat(pWeight, currentValue);
    }

    public void SetWeights(float pStepWeight, float pJumpWeight, float pAttackWeight, float pDodgeWeight, float pDeadWeight)
    {
        stepWeight = pStepWeight;
        jumpWeight = pJumpWeight;
        //crouchWeight = pCrouchWeight;
        attackWeight = pAttackWeight;
        dodgeWeight = pDodgeWeight;
        deadWeight = pDeadWeight;
    }

    public void AddCrouching(float pValue, float pGoingToLength, float pGoingAwayLength)
    {
        StopAllCoroutines();
        StartCoroutine(crouchRoutine(pValue, pGoingToLength, pGoingAwayLength));
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
}
