using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    [SerializeField] private string _Name = "Untitled Progress";
    [SerializeField] private AnimationCurve _Graph = new AnimationCurve();
    [SerializeField] private float _Speed = 0;
    [SerializeField] private bool _Looping = false;

    private float _Progress = 0;
    private Animator _Anim;

    private void Start()
    {
        _Anim = GetComponent<Animator>();
        if (_Anim == null)
            _Anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        _Progress = IncreaseProgress(_Progress, _Speed);
        _Anim.SetFloat(_Name, _Graph.Evaluate(_Progress));
    }

    private float IncreaseProgress(float pProgress, float pMult)
    {
        pProgress += Time.fixedDeltaTime * pMult;

        if (pProgress >= 1)
        {
            if (_Looping)
            {
                pProgress -= 1;
            }
            else
            {
                this.enabled = false;
            }
        }

        return pProgress;
    }
}
