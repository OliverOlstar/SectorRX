using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/New Power Data", order = 1)]
public class PowerData : ScriptableObject
{
    public int damage = 5;
    public int requiredPower = 5;
    public Sprite icon;
}

