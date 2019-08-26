using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    //public RectTransform pauseMenu, optionsMenu, powerMenu, skillMenu;
    public GameObject pause, option, ability, videoOP, audioOP, gameplayOP;
    public Text subtitleToggle;
    public bool subtitleOn;
    public PauseMenu pauseMenu;

    private void Start()
    {
        option.SetActive(false);
        ability.SetActive(false);
        subtitleOn = false;
        subtitleToggle.text = "OFF";
    }

    public void GoToOptions()
    {
        pause.SetActive(false);
        option.SetActive(true);
        ability.SetActive(false);
        videoOP.SetActive(false);
        audioOP.SetActive(false);
    }

    public void GoToVideoOP()
    {
        option.SetActive(false);
        videoOP.SetActive(true);
    }

    public void GoToAudioOP()
    {
        option.SetActive(false);
        audioOP.SetActive(true);
    }

    public void GoToSkills()
    {
        pause.SetActive(false);
        ability.SetActive(true);
    }

    public void BackToPause()
    {
        pause.SetActive(true);
        option.SetActive(false);
        ability.SetActive(false);
    }

    public void ResumeGame()
    {
        //pause.SetActive(false);
        //Time.timeScale = 1;
        pauseMenu.pause = !pauseMenu.pause;
    }

    public void ToggleSubtitle()
    {
        if(!subtitleOn)
        {
            subtitleOn = true;
            subtitleToggle.text = "ON";
        }
        else if (subtitleOn)
        {
            subtitleOn = false;
            subtitleToggle.text = "OFF";
        }
    }
}
