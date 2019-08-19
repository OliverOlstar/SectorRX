using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Power ScriptableObject", menuName = "Powers/New Power Vars", order = 0)]
public class SOPowers : ScriptableObject
{
    public string dislayName = "Untitled";
    public Sprite icon;
    public int powerRequired = 0;
    public int damage = 0;
}
