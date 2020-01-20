using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public bool pause;
    public GameObject pauseScreen;
    public GameObject powUpgrade;
    public GameObject statUpgrade;
    [SerializeField] private PlayerCamera mainCam;
    public PlayerInput _PInput;
    
    // Use this for initialization
    void Start()
    {
        //Time.timeScale = 1;
        pause = false;
        pauseScreen.SetActive(false);
        _PInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }

    public void TogglePause()
    {
        pause = !pause;

        if (pause)
        {
            _PInput.SwitchCurrentActionMap("PauseScreen");

            if (Input.GetKey(KeyCode.Escape))
                Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            _PInput.SwitchCurrentActionMap("Player");

            if (Input.GetKey(KeyCode.Escape))
                Cursor.lockState = CursorLockMode.Locked;
        }


        pauseScreen.SetActive(pause);
        mainCam.CameraDisabled = pause;
    }
}