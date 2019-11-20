using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    //public RectTransform pauseMenu, optionsMenu, powerMenu, skillMenu;
    public GameObject pause, option, ability, videoOP, audioOP, gameplayOP, cellUI, coreUI, powerUpgrade, statUpgrade;
    public Text subtitleToggle, displayToggle, resToggle, cellCount, coreCount, upCoreCount, upCellCount, 
        coreNotficationOne, coreNotficationTwo, cellNotficationOne, cellNotficationTwo;
    public bool subtitleOn, isFullScreen, isWindowed;

    //Booleans to check if Cell UI or Power Core UI are already active when collecting other item
    public bool cellUIOn, coreUIOn;

    //Screen Resolutions
    public bool is1920, is1280, is2560, is1360, is1366;

    public PauseMenu pauseMenu;

    //Control Volume
    public Slider masterVolSlide;
    public AudioSource master;
    float masterVol;
    public int cellCounter;
    public int coreCounter;

    private void Start()
    {
        //option.SetActive(false);
        //ability.SetActive(false);
        powerUpgrade.SetActive(false);
        statUpgrade.SetActive(false);
        coreNotficationOne.gameObject.SetActive(false);
        coreNotficationTwo.gameObject.SetActive(false);
        cellNotficationOne.gameObject.SetActive(false);
        cellNotficationTwo.gameObject.SetActive(false);

        cellUIOn = false;
        coreUIOn = false;

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

    private void Update()
    {
        if(cellUIOn)
        {
            StartCoroutine("CellUIOff");
        }
        else if (cellUIOn == false)
        {
            cellUI.SetActive(false);
        }

        if(coreUIOn)
        {
            StartCoroutine("CoreUIOff");
        }
        else if (coreUIOn == false)
        {
            coreUI.SetActive(false);
        }
    }

    //Collectable UI Management
    public void SetCellCount()
    {
        cellCount.text = cellCounter.ToString();
    }
    public void SetCoreCount()
    {
        coreCount.text = coreCounter.ToString();
    }

    IEnumerator CellUIOff()
    {
        yield return new WaitForSeconds(3.5f);
        cellUI.SetActive(false);
        cellUIOn = false;
    }

    IEnumerator CoreUIOff()
    {
        yield return new WaitForSeconds(3.5f);
        coreUI.SetActive(false);
        coreUIOn = false;
    }

    //Navigate between upgrade Menus
    public void goPowUpgrade()
    {
        pause.SetActive(false);
        statUpgrade.SetActive(false);
        powerUpgrade.SetActive(true);
        coreCount.gameObject.SetActive(true);
        upCoreCount.text = coreCounter.ToString();
    }

    public void goStatUpgrade()
    {
        pause.SetActive(false);
        powerUpgrade.SetActive(false);
        statUpgrade.SetActive(true);
        cellCount.gameObject.SetActive(true);
        upCellCount.text = cellCounter.ToString();
    }

    public void PowerToStat()
    {
        powerUpgrade.SetActive(false);
        statUpgrade.SetActive(true);
    }

    public void StatToPower()
    {
        statUpgrade.SetActive(false);
        powerUpgrade.SetActive(true);
    }

    //Menus and Settings Management
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
        //pause.SetActive(true);
        //option.SetActive(false);
        //ability.SetActive(false);
        powerUpgrade.SetActive(false);
        statUpgrade.SetActive(false);
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
