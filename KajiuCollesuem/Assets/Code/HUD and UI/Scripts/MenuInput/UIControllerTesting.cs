using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;

public class UIControllerTesting : MonoBehaviour
{
    [SerializeField] InputSystemUIInputModule _PInput;

    public GameObject firstButton;
    public GameObject secondButton;
    public GameObject thirdButton;
    public GameObject fourButton;

    private bool _SwitchOnePressed;
    private bool _SwitchTwoPressed;
    private bool _SwitchThreePressed;
    private bool _SwitchFourPressed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        EventSystem.current.SetSelectedGameObject(firstButton);
        secondButton.SetActive(false);
        fourButton.SetActive(false);
    }
    private void Update()
    {
        if(_SwitchOnePressed)
        {
            EventSystem.current.SetSelectedGameObject(secondButton);
            _SwitchOnePressed = false;
        }
        else if(_SwitchTwoPressed)
        {
            EventSystem.current.SetSelectedGameObject(firstButton);
            _SwitchTwoPressed = false;
        }
        else if (_SwitchThreePressed)
        {
            EventSystem.current.SetSelectedGameObject(fourButton);
            _SwitchThreePressed = false;
        }
        else if (_SwitchFourPressed)
        {
            EventSystem.current.SetSelectedGameObject(thirdButton);
            _SwitchFourPressed = false;
        }
    }

    public void SwitchButtonOne()
    {
        firstButton.SetActive(false);
        thirdButton.SetActive(false);

        secondButton.SetActive(true);
        fourButton.SetActive(true);

        _SwitchOnePressed = true;
    }

    public void SwitchButtonTwo()
    {
        firstButton.SetActive(true);
        thirdButton.SetActive(true);

        secondButton.SetActive(false);
        fourButton.SetActive(false);

        _SwitchTwoPressed = true;
    }

    public void SwitchButtonThree()
    {
        firstButton.SetActive(false);
        thirdButton.SetActive(false);

        secondButton.SetActive(true);
        fourButton.SetActive(true);

        _SwitchThreePressed = true;
    }

    public void SwitchButtonFour()
    {
        firstButton.SetActive(true);
        thirdButton.SetActive(true);

        secondButton.SetActive(false);
        fourButton.SetActive(false);

        _SwitchFourPressed = true;
    }
}
