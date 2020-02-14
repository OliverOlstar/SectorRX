using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    public GameObject[] _Buttons;
    public int currentButton;
    public PlayerInput pInput;

    private void OnEnable()
    {
        currentButton = 0;
        pInput = transform.parent.GetComponentInChildren<PlayerInput>();
    }

    private void Update()
    {
        return;

        CheckSelection();
        ChangeSelect();
        PreventChange();
    }

    public void CheckSelection()
    {
        //Set selected game object depending on current button value
        if (currentButton == 0)
        {
            EventSystem.current.SetSelectedGameObject(_Buttons[0]);
        }
        if (currentButton == 1)
        {
            EventSystem.current.SetSelectedGameObject(_Buttons[1]);
        }
    }

    public void ChangeSelect()
    {
        //Change current button based on keyboard input
        if (pInput.currentControlScheme == "Keyboard&Mouse")
        {
            if (Input.GetAxis("Vertical") < 0 && currentButton == 0)
            {
                currentButton = 1;
            }

            if (Input.GetAxis("Vertical") > 0 && currentButton == 1)
            {
                currentButton = 0;
            }
        }

        //Change current button based on keyboard input
        if (pInput.currentControlScheme == "Gamepad")
        {
            if (Input.GetAxis("Vertical") < 0 && currentButton == 0)
            {
                currentButton = 1;
            }

            if (Input.GetAxis("Vertical") > 0 && currentButton == 1)
            {
                currentButton = 0;
            }
        }
    }

    public void PreventChange()
    {
        //Prevent keyboard navigation if at start or end of array
        if (pInput.currentControlScheme == "Keyboard&Mouse")
        {
            if (Input.GetAxis("Vertical") < 0 && currentButton == 0)
            {
                currentButton = 0;
            }

            if (Input.GetAxis("Vertical") > 0 && currentButton == 1)
            {
                currentButton = 1;
            }
        }

        //Prevent keyboard navigation if at start or end of array
        if (pInput.currentControlScheme == "Gamepad")
        {
            if (Input.GetAxis("Vertical") < 0 && currentButton == 0)
            {
                currentButton = 0;
            }

            if (Input.GetAxis("Vertical") > 0 && currentButton == 1)
            {
                currentButton = 1;
            }
        }
    }
}