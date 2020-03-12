using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuLizzy : MonoBehaviour
{
    private Animator _Anim;

    [SerializeField] private SkinnedMeshRenderer[] _Renderers;
    [SerializeField] private SkinnedMeshRenderer _FeathersRender;
    [SerializeField] private GameObject[] _LizzyArmour;

    private float _State = 0.0f;
    private Coroutine _Routine;
    private Coroutine _LockedRoutine;

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

    void Awake()
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

    public void SetColors(ColorSet pSet)
    {
        foreach (SkinnedMeshRenderer renderer in _Renderers)
        {
            renderer.material = pSet.lizzyMat;
        }

        if (_FeathersRender != null)
            _FeathersRender.material = pSet.feathersMat;
    }

    public void SetAbilities(int pAbilities)
    {
        switch (pAbilities)
        {
            case 0:
                _LizzyArmour[0].SetActive(false);
                _LizzyArmour[1].SetActive(false);
                _FeathersRender.enabled = false;
                break;

            case 1:
                _LizzyArmour[0].SetActive(true);
                _LizzyArmour[1].SetActive(true);
                _FeathersRender.enabled = false;
                break;

            case 2:
                _LizzyArmour[0].SetActive(false);
                _LizzyArmour[1].SetActive(false);
                _FeathersRender.enabled = true;
                break;
        }
    }
}
