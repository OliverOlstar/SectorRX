using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Character", menuName = "Character")]

/*Programmer: Scott Watman
 Description: Scriptable Object to allow multiple characters to be created*/

public class CharacterSelect : ScriptableObject
{
    public string characterName;
    public Sprite characterSprite;
    public GameObject characterPrefab;
}
