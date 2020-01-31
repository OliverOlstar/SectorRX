using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;

public class HUDManager : MonoBehaviour
{
    //public RectTransform pauseMenu, optionsMenu, powerMenu, skillMenu;
    public GameObject pause, /*option, ability, videoOP, audioOP gameplayOP,*/ cellUI, coreUI, powerUpgrade, statUpgrade, powerSelect;
    public GameObject resumeButton;
    public GameObject targetUI;
    public Text cellCount, coreCount, upCoreCount, upCellCount;

    //Booleans to check if Cell UI or Power Core UI are already active when collecting other item
    public bool cellUIOn, coreUIOn;
    public int cellCounter;
    public int coreCounter;

    public PauseMenu pauseMenu;
    [SerializeField] private PlayerCamera mainCam;

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
        if (cellUIOn)
        {
            StartCoroutine("CellUIOff");
        }
        else if (cellUIOn == false)
        {
            cellUI.SetActive(false);
        }

        if (coreUIOn)
        {
            StartCoroutine("CoreUIOff");
        }
        else if (coreUIOn == false)
        {
            coreUI.SetActive(false);
        }

        if (targetUI != null)
        {
            EventSystem.current.SetSelectedGameObject(targetUI);
            targetUI = null;
        }

        ////Controller and Keyboard Input with UI Module
        if (pauseMenu.hasPaused)
        {
            EventSystem.current.SetSelectedGameObject(resumeButton);
            pauseMenu.hasPaused = false;
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
    public void goPowUpgrade(GameObject pTarget)
    {
        pause.SetActive(false);
        statUpgrade.SetActive(false);
        powerUpgrade.SetActive(true);
        coreCount.gameObject.SetActive(true);
        upCoreCount.text = coreCounter.ToString();
        targetUI = pTarget;
    }

    public void goStatUpgrade(GameObject pTarget)
    {
        pause.SetActive(false);
        powerUpgrade.SetActive(false);
        statUpgrade.SetActive(true);
        cellCount.gameObject.SetActive(true);
        upCellCount.text = cellCounter.ToString();
        targetUI = pTarget;
    }

    public void PowerToStat(GameObject pTarget)
    {
        powerUpgrade.SetActive(false);
        statUpgrade.SetActive(true);
        targetUI = pTarget;
    }

    public void StatToPower(GameObject pTarget)
    {
        statUpgrade.SetActive(false);
        powerUpgrade.SetActive(true);
        targetUI = pTarget;
    }

    public void BackToPause(GameObject pTarget)
    {
        pause.SetActive(true);
        //option.SetActive(false);
        //ability.SetActive(false);
        powerUpgrade.SetActive(false);
        statUpgrade.SetActive(false);
        targetUI = pTarget;
    }

    public void ResumeGame()
    {
        pauseMenu.TogglePause();
    }
}
