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
        //Cursor.lockState = CursorLockMode.Locked;
        _PInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
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
                _PInput.enabled = _PInput.enabled;
            }
        }

    }

    private void TogglePauseGame()
    {
        pause = !pause;

        //Time.timeScale = pause ? 0 : 1;
        pauseScreen.SetActive(pause);
        Cursor.lockState = pause ? CursorLockMode.None : CursorLockMode.Locked;
        mainCam.CameraDisabled = pause;
        _PInput.enabled = !_PInput.enabled;
    }
}