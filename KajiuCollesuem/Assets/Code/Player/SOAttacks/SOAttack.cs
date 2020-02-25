using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack ScriptableObject", menuName = "Attacks/New Attack", order = 0)]
public class SOAttack : ScriptableObject
{
    [Header("Attack itself")]
    [Min(0)] public float attackTime = 0.5f;
    public SOGraph attackGraph;
    
    [Header("Post Attack")]
    [Min(0)] public float holdEndPosTime = 0.1f;

    [Header("Pre Attack")]
    [Min(0)] public float holdStartPosTime = 0.1f;

    [Header("During Attack - Hitbox")]
    public int hitboxIndex = 0;

    [Space]
    public float enableHitboxTime = 0.2f;
    public float disableHitboxTime = 0.4f;

    [Space]
    public float HitboxKnockup = 10.0f;
    public float HitboxKnockback = 20.0f;
    public int HitboxDamage = 20;
}
