using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats ScriptableObject", menuName = "Powers/New Stat", order = 0)]
public class SOStats : ScriptableObject
{
    public Sprite icon;
    public string dislayName = "Untitled";
    [TextArea] public string dislayDescription = "Description";

    [Space]
    public int[] values = new int[3];
    [TextArea] public string[] statDescriptions = new string[3];
    public int[] cost = new int[3];
}
