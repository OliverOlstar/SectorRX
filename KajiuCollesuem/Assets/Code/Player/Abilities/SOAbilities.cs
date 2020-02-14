using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Abilities ScriptableObject", menuName = "Abilities/New Ability Vars", order = 0)]
public class SOAbilities : ScriptableObject
{
    public Sprite icon;
    public string dislayName = "Untitled";
    [TextArea] public string dislayDescription = "Description";

    [Space]
    public PlayerAbilitySelector.abilities WhichPower;

    [Space]
    public float abilityTime = 6;
    public int powerRequired = 0;
    public int damage = 0;

    [Header("Upgrades")]
    [TextArea] public string[] powerDescriptions = new string[2];
    public int[] cost = new int[0];
}
