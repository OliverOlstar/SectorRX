using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    //public RectTransform pauseMenu, optionsMenu, powerMenu, skillMenu;
    public GameObject pause, option, ability, videoOP, audioOP, gameplayOP;
    public Text subtitleToggle;
    public Text displayToggle;
    public Text resToggle;
    public bool subtitleOn;
    public bool isFullScreen;
    public bool isWindowed;
    
    //Screen Resolutions
    public bool is1920;
    public bool is1280;
    public bool is2560;
    public bool is1360;
    public bool is1366;

    public PauseMenu pauseMenu;

    //Control Volume
    public Slider masterVolSlide;
    public AudioSource master;
    float masterVol;

    private void Start()
    {
        option.SetActive(false);
        ability.SetActive(false);
        
        //Default subtitles are set to off
        subtitleOn = false;
        subtitleToggle.text = "OFF";

        //Default display is set to fullscreen
        isFullScreen = true;
        displayToggle.text = "FULLSCREEN";
        isWindowed = false;

        //Default resolution is 1920 by 1080
        is1920 = true;
        resToggle.text = "1920 X 1080";
        is1280 = false;
        is2560 = false;

        //Set Master Volume to Slider value
        masterVol = masterVolSlide.value;
        master.volume = masterVol;
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

    public void AdjustMasterVolume(float newVolume)
    {
        newVolume = masterVolSlide.value;
        master.volume = newVolume;
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

    public void ToggleDisplay()
    {
        if(isFullScreen)
        {
            isFullScreen = false;
            isWindowed = true;
            displayToggle.text = "WINDOWED";
            if(is1920)
            {
                Screen.SetResolution(1920, 1080, false);
            }
            else if(is2560)
            {
                Screen.SetResolution(2560, 1440, false);
            }
            else if(is1280)
            {
                Screen.SetResolution(1280, 720, false);
            }
            else if(is1360)
            {
                Screen.SetResolution(1360, 768, false);
            }
            else if(is1366)
            {
                Screen.SetResolution(1366, 768, false);
            }
        }
        else if(isWindowed)
        {
            isWindowed = false;
            isFullScreen = true;
            displayToggle.text = "FULLSCREEN";
            if(is1920)
            {
                Screen.SetResolution(1920, 1080, true);
            }
            else if(is2560)
            {
                Screen.SetResolution(2560, 1440, true);
            }
            else if(is1280)
            {
                Screen.SetResolution(1280, 720, true);
            }
            else if(is1360)
            {
                Screen.SetResolution(1360, 768, true);
            }
            else if(is1366)
            {
                Screen.SetResolution(1366, 768, true);
            }
        }
    }

    public void ToggleRes()
    {
        if(is1920)
        {
            is1920 = false;
            is2560 = true;
            resToggle.text = "2560 X 1440";
            if (isFullScreen)
            {
                Screen.SetResolution(2560, 480, true);
            }
            else if (isWindowed)
            {
                Screen.SetResolution(2560, 480, false);
            }
        }
        else if(is2560)
        {
            is2560 = false;
            is1280 = true;
            resToggle.text = "1280 X 720";
            if (isFullScreen)
            {
                Screen.SetResolution(1280, 720, true);
            }
            else if (isWindowed)
            {
                Screen.SetResolution(1280, 720, false);
            }
        }
        else if(is1280)
        {
            is1280 = false;
            is1360 = true;
            resToggle.text = "1360 X 768";
            if (isFullScreen)
            {
                Screen.SetResolution(1360, 768, true);
            }
            else if (isWindowed)
            {
                Screen.SetResolution(1360, 768, false);
            }
        }
        else if(is1360)
        {
            is1360 = false;
            is1366 = true;
            resToggle.text = "1366 X 768";
            if (isFullScreen)
            {
                Screen.SetResolution(1366, 768, true);
            }
            else if (isWindowed)
            {
                Screen.SetResolution(1366, 768, false);
            }
        }
        else if(is1366)
        {
            is1366 = false;
            is1920 = true;
            resToggle.text = "1920 X 1080";
            if (isFullScreen)
            {
                Screen.SetResolution(1920, 1080, true);
            }
            else if (isWindowed)
            {
                Screen.SetResolution(1920, 1080, true);
            }
        }
    }
}
