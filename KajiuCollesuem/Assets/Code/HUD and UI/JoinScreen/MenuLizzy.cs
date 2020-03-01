using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLizzy : MonoBehaviour
{
    private Animator _Anim;

    private float _State = 0.0f;
    private Coroutine _Routine;
    private Coroutine _LockedRoutine;

    [Header("Crouched")]
    [SerializeField] private AnimationCurve _CrouchGraph;
    [SerializeField] private float _CrouchMult = 1.0f;
    private float _CrouchProgress = 0.0f;

    [Header("Idle")]
    [SerializeField] private AnimationCurve _IdleGraph;
    [SerializeField] private float _IdleMult = 1.0f;
    private float _IdleProgress = 0.0f;

    [Header("Locked In")]
    [SerializeField] private AnimationCurve _LockedInGraph;
    [SerializeField] private float _LockedInMult = 1.0f;
    private float _LockedInProgress = 0.0f;


    public enum menuLizzyStates
    {
        NotJoined,
        Joined,
        LockedIn
    }

    void Start()
    {
        _Anim = GetComponentInChildren<Animator>();
    }

    public void ChangeWeights(menuLizzyStates pState)
    {
        switch (pState)
        {
            case menuLizzyStates.NotJoined:
                if (_Routine != null)
                    StopCoroutine(_Routine);
                _Routine = StartCoroutine(weightChangeRoutine(0, 2));

                if (_LockedInProgress > 0)
                {
                    if (_LockedRoutine != null)
                        StopCoroutine(_LockedRoutine);
                    _LockedRoutine = StartCoroutine(LockInAnimRoutine(-1, 8));
                }
                break;

            case menuLizzyStates.Joined:
                if (_Routine != null)
                    StopCoroutine(_Routine);
                _Routine = StartCoroutine(weightChangeRoutine(1, 2));

                if (_LockedInProgress > 0)
                {
                    if (_LockedRoutine != null)
                        StopCoroutine(_LockedRoutine);
                    _LockedRoutine = StartCoroutine(LockInAnimRoutine(-1, 8));
                }
                break;

            case menuLizzyStates.LockedIn:
                if (_Routine != null)
                    StopCoroutine(_Routine);
                _Routine = StartCoroutine(weightChangeRoutine(2, 3));

                if (_LockedRoutine != null)
                    StopCoroutine(_LockedRoutine);
                _LockedRoutine = StartCoroutine(LockInAnimRoutine(1, 3));
                break;
        }
    }

    private IEnumerator weightChangeRoutine(float pWeight, float pDampening)
    {
        while(Mathf.Abs(_State - pWeight) > 0.001f)
        {
            _State = Mathf.Lerp(_State, pWeight, Time.deltaTime * pDampening);
            _Anim.SetFloat("State", _State);
            yield return null;
        }

        _State = pWeight;
        _Anim.SetFloat("State", _State);
    }

    private IEnumerator LockInAnimRoutine(float pDirection, float pDampening)
    {
        while ((_LockedInProgress < 0.999f && pDirection == 1) || (_LockedInProgress > 0.001f && pDirection == -1))
        {
            _LockedInProgress += Time.fixedDeltaTime * _LockedInMult * pDirection;
            _Anim.SetFloat("LockedIn Progress", _LockedInGraph.Evaluate(_LockedInProgress));
            yield return null;
        }

        _LockedInProgress = pDirection == 1 ? 1 : 0;
        _Anim.SetFloat("LockedIn Progress", _LockedInProgress);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeWeights(menuLizzyStates.NotJoined);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeWeights(menuLizzyStates.Joined);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeWeights(menuLizzyStates.LockedIn);

        _CrouchProgress = IncreaseProgress(_CrouchProgress, _CrouchMult);
        _Anim.SetFloat("Crouch Progress", _CrouchGraph.Evaluate(_CrouchProgress));

        _IdleProgress = IncreaseProgress(_IdleProgress, _IdleMult);
        _Anim.SetFloat("Idle Progress", _IdleGraph.Evaluate(_IdleProgress));

        //_LockedInProgress = IncreaseProgress(_LockedInProgress, _LockedInMult, true);
        //_Anim.SetFloat("LockedIn Progress", _LockedInGraph.Evaluate(_LockedInProgress));
    }

    private float IncreaseProgress(float pProgress, float pMult)
    {
        pProgress += Time.fixedDeltaTime * pMult;

        if (pProgress >= 1)
            pProgress -= 1;

        return pProgress;
    }
}
