using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public bool Paused;
    public GameObject PauseScreen;

    void Start() {
        Time.timeScale = 1;
        Paused = false;
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Paused = !Paused;
        }

        if (Paused)
        {
            Time.timeScale = 0;
            PauseScreen.SetActive(true);
        }
        else if (!Paused)
        {
            Time.timeScale = 1;
            PauseScreen.SetActive(false);
        }
    }
}
