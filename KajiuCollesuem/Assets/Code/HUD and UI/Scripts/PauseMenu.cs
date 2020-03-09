using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{
    public bool pause;
    public bool hasPaused;
    public GameObject pauseScreen, controlScreen;
    public GameObject resumeButton, targetUI;
    [SerializeField] private PlayerCamera mainCam;
    private StatPause[] _PlayerHUDs = new StatPause[6];

    public void SetPlayerHUDs(StatPause[] pHUDs) => _PlayerHUDs = pHUDs;

    // Use this for initialization
    void Start()
    {
        pause = false;
        pauseScreen.SetActive(false);
        controlScreen.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void Update()
    {
        if (targetUI != null)
        {
            EventSystem.current.SetSelectedGameObject(targetUI);
            targetUI = null;
        }
    }

    public void TogglePause()
    {
        pause = !pause;

        if (pause)
        {
            Time.timeScale = 0;
            hasPaused = true;
            Cursor.lockState = CursorLockMode.None;
            targetUI = resumeButton;
        }
        else
        {
            Time.timeScale = 1;
            hasPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
            EventSystem.current.SetSelectedGameObject(null);
            controlScreen.SetActive(false);
        }

        pauseScreen.SetActive(pause);
    }

    public void ToControls()
    {
        pauseScreen.SetActive(false);
        controlScreen.SetActive(true);
    }

    public void BackToPause(GameObject pTarget)
    {
        pauseScreen.SetActive(true);
        controlScreen.SetActive(false);
        targetUI = pTarget;
    }
}