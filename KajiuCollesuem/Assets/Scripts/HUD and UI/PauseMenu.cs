using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool pause;
    public GameObject pauseScreen;
    [SerializeField] PlayerCamera mainCam;
    [SerializeField] PlayerInputHandler input;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        pause = false;
        pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
        }

        if (pause)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            mainCam.CameraDisabled = true;
            input.inputDisabled = true;
        }
        else if(!pause)
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            mainCam.CameraDisabled = false;
            input.inputDisabled = false;
        }
    }
}
