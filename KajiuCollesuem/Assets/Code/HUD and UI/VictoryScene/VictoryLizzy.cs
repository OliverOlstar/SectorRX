using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryLizzy : MonoBehaviour
{
    private Animator _Anim;
    private ColorSetter _ColorSetter;
    private Spin _Spin;

    [SerializeField] private PlayAnimation _DeadAnimation;

    private void Awake()
    {
        _Anim = GetComponentInChildren<Animator>();
        _ColorSetter = GetComponent<ColorSetter>();
        _Spin = GetComponent<Spin>();
    }

    public void SetLizzy(UsedDevices pPlayer)
    {
        _ColorSetter.SetColor(pPlayer.playerColorSet);
        _ColorSetter.SetAbility(pPlayer.abilitySelected);
        SetAnim(pPlayer.victoryScene.Alive);
    }

    public void SetAnim(bool pAlive)
    {
        // If Victor
        if (pAlive)
        {
            _Anim.SetFloat("Pose", Mathf.Round(Random.value * 4) + 1);
            _Spin.enabled = true;
        }
        // If Loser
        else
        {
            _Anim.SetFloat("Dead Direction", Random.value);
            _Anim.SetFloat("Pose", 0);
        }

        StartCoroutine(TransitionToPoseRoutine(pAlive));
    }

    private IEnumerator TransitionToPoseRoutine(bool pAlive)
    {
        yield return new WaitForSeconds(1.0f);

        float state = 0;
        float randomizedStart = 0;
        float defaultStart = 0.925f;

        if (pAlive == false)
            randomizedStart = (Random.value - 0.5f) / 10;
        else
            defaultStart = 0.999f;

        while (state < defaultStart - randomizedStart)
        {
            state = Mathf.Lerp(state, 1, Time.deltaTime * 2f);
            _Anim.SetFloat("State", state);
            yield return null;
        }

        _Anim.SetFloat("State", 1);

        if (pAlive == false)
            _DeadAnimation.enabled = true;
    }
}
