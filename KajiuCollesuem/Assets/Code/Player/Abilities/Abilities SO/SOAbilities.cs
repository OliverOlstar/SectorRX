using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Abilities ScriptableObject", menuName = "Attacks/New Ability", order = 0)]
public class SOAbilities : ScriptableObject
{
    public PlayerAbilitySelector.abilities WhichPower;

    [Header("Attack itself")]
    [Min(0)] public float abilityTime = 0.5f;
    [Min(0)] public float abilityAnimTime = 1.0f;
    public AnimationCurve abilitiesGraph;

    [Space]
    public int powerRequired = 0;

    [Header("Post Attack")]
    [Min(0)] public float holdEndPosTime = 0.1f;

    [Header("Pre Attack")]
    [Min(0)] public float holdStartPosTime = 0.1f;
    [Min(0)] public float maxChargeTime = 0.1f;
    //[Min(0)] public float toggleEffectTime = 1.0f;
}