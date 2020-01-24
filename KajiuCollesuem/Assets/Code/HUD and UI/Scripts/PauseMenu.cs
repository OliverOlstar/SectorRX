using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public bool pause;
    public bool hasPaused;
    public GameObject pauseScreen;
    public GameObject powUpgrade;
    public GameObject statUpgrade;
    public GameObject resumeButton;
    public PlayerInput pInput;
    [SerializeField] private PlayerCamera mainCam;
    
    // Use this for initialization
    void Start()
    {
        //Time.timeScale = 1;
        pause = false;
        pauseScreen.SetActive(false);
        pInput = transform.parent.GetComponentInChildren<PlayerInput>();
        EventSystem.current.SetSelectedGameObject(resumeButton);
        Debug.Log(pInput.currentControlScheme);
    }

    public void TogglePause()
    {
        pause = !pause;

        if (pause)
        {
            hasPaused = true;
            pInput.SwitchCurrentActionMap("PauseScreen");
            Debug.Log(pInput.currentActionMap);

            //if (pInput.currentControlScheme == "Keyboard&Mouse")
                //Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            pInput.SwitchCurrentActionMap("Player");
            Debug.Log(pInput.currentActionMap);
            hasPaused = false;
            //if (pInput.currentControlScheme == "Keyboard&Mouse")
                //Cursor.lockState = CursorLockMode.Locked;
        }

        pauseScreen.SetActive(pause);
        //mainCam.CameraDisabled = pause;
    }
}