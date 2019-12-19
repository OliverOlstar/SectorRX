using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    //public RectTransform pauseMenu, optionsMenu, powerMenu, skillMenu;
    public GameObject pause, /*option, ability, videoOP, audioOP gameplayOP,*/ cellUI, coreUI, powerUpgrade, statUpgrade, powerSelect;
    public Text cellCount, coreCount, upCoreCount, upCellCount;

    //Booleans to check if Cell UI or Power Core UI are already active when collecting other item
    public bool cellUIOn, coreUIOn;
    public int cellCounter;
    public int coreCounter;

    public PauseMenu pauseMenu;
    [SerializeField] private PlayerCamera mainCam;
    [SerializeField] private PlayerInputHandler input;

    private void Start()
    {
        //option.SetActive(false);
        //ability.SetActive(false);

        powerUpgrade.SetActive(false);
        statUpgrade.SetActive(false);

        cellUIOn = false;
        coreUIOn = false;
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
        yield return new WaitForSeconds(1.5f);
        cellUI.SetActive(false);
        cellUIOn = false;
    }

    IEnumerator CoreUIOff()
    {
        yield return new WaitForSeconds(1.5f);
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
        //option.SetActive(true);
        //ability.SetActive(false);
        //videoOP.SetActive(false);
        //audioOP.SetActive(false);
    }

    //public void GoToVideoOP()
    //{
    //    option.SetActive(false);
    //    videoOP.SetActive(true);
    //}

    //public void GoToAudioOP()
    //{
    //    option.SetActive(false);
    //    audioOP.SetActive(true);
    //}

    //public void GoToSkills()
    //{
    //    pause.SetActive(false);
    //    ability.SetActive(true);
    //}

    public void BackToPause()
    {
        pause.SetActive(true);
        //option.SetActive(false);
        //ability.SetActive(false);
        powerUpgrade.SetActive(false);
        statUpgrade.SetActive(false);
    }

    public void ResumeGame()
    {
        pause.SetActive(false);
        Time.timeScale = 1;
        pauseMenu.pause = !pauseMenu.pause;
        mainCam.CameraDisabled = !pause;
        input.inputDisabled = !pause;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
