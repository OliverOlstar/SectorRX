using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Abilities ScriptableObject", menuName = "Attacks/New Ability", order = 0)]
public class SOAbilities : ScriptableObject
{
    public PlayerAbilitySelector.abilities WhichPower;

    [Header("Attack itself")]
    [Min(0)] public float abilityTime = 0.5f;
    public SOGraph attackGraph;

    [Space]
    public int powerRequired = 0;

    [Header("Post Attack")]
    [Min(0)] public float holdEndPosTime = 0.1f;

    [Header("Pre Attack")]
    [Min(0)] public float holdStartPosTime = 0.1f;
    [Min(0)] public float maxChargeTime = 0.1f;

    [Header("Transtion Dampenings")]
    public float transitionInDampening = 5.0f;
    public float transitionOutDampening = 15.0f;
}
