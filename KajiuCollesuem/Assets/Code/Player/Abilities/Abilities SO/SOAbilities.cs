using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Abilities ScriptableObject", menuName = "Attacks/New Ability", order = 0)]
public class SOAbilities : ScriptableObject
{
    [Header("Attack itself")]
    //[Min(0)] public float abilityTime = 0.5f;
    [Min(0)] public float abilityAnimTime = 1.0f;
    [Min(0)] public float rotationDampening = 3.0f;
    public AnimationCurve abilitiesGraph;

    [Space]
    public int abilityRequired = 0;

    [Header("Post Attack")]
    [Min(0)] public float holdEndPosTime = 0.1f;

    [Header("Pre Attack")]
    [Min(0)] public float holdStartPosTime = 0.1f;

    [Header("Hitbox")]
    [Min(0)] public float hitBoxAppearTime = 0.1f;
    [Min(0)] public float hitBoxStayTime = 0.2f;
    [Min(0)] public float projectileSpeed = 1.0f;
}