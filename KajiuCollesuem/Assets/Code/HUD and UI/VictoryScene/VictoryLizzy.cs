using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryLizzy : MonoBehaviour
{
    private Animator _Anim;
    [SerializeField] private PlayAnimation _DeadAnimation;

    private ColorSetter _ColorSetter;

    private void Awake()
    {
        _Anim = GetComponentInChildren<Animator>();
        _ColorSetter = GetComponent<ColorSetter>();
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
        }
        // If Loser
        else
        {
            _Anim.SetFloat("Dead Direction", Random.value);
            _Anim.SetFloat("Pose", 0);
        }

        StartCoroutine("TransitionToPoseRoutine", pAlive);
    }

    private IEnumerator TransitionToPoseRoutine(bool pAlive)
    {
        yield return new WaitForSeconds(1.0f);

        float state = 0;

        while (state < 1)
        {
            state += Mathf.Lerp(state, 1, Time.deltaTime * 0.01f);
            _Anim.SetFloat("State", state);
            yield return null;
        }

        _Anim.SetFloat("State", 1);

        yield return new WaitForSeconds(0.5f);

        if (pAlive == false)
            _DeadAnimation.enabled = true;
    }
}
