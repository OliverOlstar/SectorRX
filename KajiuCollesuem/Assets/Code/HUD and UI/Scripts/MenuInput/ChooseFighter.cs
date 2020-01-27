using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChooseFighter : MonoBehaviour
{
    public List<CharacterSelect> characters = new List<CharacterSelect>();
    public GameObject charCellPrefab;

    // Start is called before the first frame update
    void Start()
    {
        foreach(CharacterSelect character in characters)
        {
            CreateCharacterCell(character);
        }
    }

    private void CreateCharacterCell(CharacterSelect character)
    {
        GameObject charCell = Instantiate(charCellPrefab, transform);

        Image artwork = charCell.transform.Find("Artwork").GetComponent<Image>();
        Text charName = charCell.transform.Find("CharacterName").GetComponentInChildren<Text>();

        artwork.sprite = character.characterSprite;
        charName.text = character.characterName;
    }
}
