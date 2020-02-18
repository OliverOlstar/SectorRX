using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public bool pause;
    public bool hasPaused;
    public GameObject pauseScreen;
    public GameObject powUpgrade;
    public GameObject statUpgrade;
    public GameObject resumeButton;
    [SerializeField] private PlayerCamera mainCam;
    
    // Use this for initialization
    void Start()
    {
        pause = false;
        pauseScreen.SetActive(false);
        EventSystem.current.SetSelectedGameObject(resumeButton);
    }

    public void TogglePause()
    {
        pause = !pause;

        if (pause)
        {
            Time.timeScale = 0;
            hasPaused = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            hasPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        pauseScreen.SetActive(pause);
        //mainCam.CameraDisabled = pause;
    }
}