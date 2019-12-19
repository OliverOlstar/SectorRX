using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool pause;
    public GameObject pauseScreen;
    public GameObject powUpgrade;
    public GameObject statUpgrade;
    [SerializeField] private PlayerCamera mainCam;
    [SerializeField] private PlayerInputHandler input;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        pause = false;
        pauseScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseGame();
            if(!pause)
            {
                powUpgrade.SetActive(false);
                statUpgrade.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

    }

    private void TogglePauseGame()
    {
        pause = !pause;

        Time.timeScale = pause ? 0 : 1;
        pauseScreen.SetActive(pause);
        Cursor.lockState = pause ? CursorLockMode.None : CursorLockMode.Locked;
        mainCam.CameraDisabled = pause;
        input.inputDisabled = pause;
    }
}
