using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    private GameObject[] _Buttons;
    [SerializeField] private int _CurrentButton;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(_Buttons[0]);   
    }
}