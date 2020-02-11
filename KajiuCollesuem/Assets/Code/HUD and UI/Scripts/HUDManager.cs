using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;

public class HUDManager : MonoBehaviour
{
    //public RectTransform pauseMenu, optionsMenu, powerMenu, skillMenu;
    public GameObject pause, /*option, ability, videoOP, audioOP gameplayOP,*/ cellUI, powerUpgrade, statUpgrade, powerSelect;
    public GameObject resumeButton;
    public GameObject targetUI;
    public Slider cellExp;
    public bool canUpgrade;
    public Text cellCount, upgradeReady, upCellCount;

    //Booleans to check if Cell UI or Power Core UI are already active when collecting other item
    public bool cellUIOn;
    public int cellCounter;

    public PauseMenu pauseMenu;
    [SerializeField] private PlayerCamera mainCam;

    private void Start()
    {
        //option.SetActive(false);
        //ability.SetActive(false);

        powerUpgrade.SetActive(false);
        statUpgrade.SetActive(false);

        cellUIOn = true;
    }

    private void Update()
    {
        if(cellExp.value >= 100)
        {
            upgradeReady.gameObject.SetActive(true);
            canUpgrade = true;
        }
        else
        {
            canUpgrade = false;
            upgradeReady.gameObject.SetActive(false);
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

    public void OpenUpgrade()
    {
        if(canUpgrade)
        {
            statUpgrade.gameObject.SetActive(true);
            cellExp.gameObject.SetActive(false);
            upgradeReady.gameObject.SetActive(false);
        }
        else
        {
            cellExp.gameObject.SetActive(true);
            upgradeReady.gameObject.SetActive(true);
            statUpgrade.gameObject.SetActive(false);
        }
    }

    //Collectable UI Management
    public void SetCellCount()
    {
        cellCount.text = cellCounter.ToString();
    }

    //IEnumerator CellUIOff()
    //{
    //    yield return new WaitForSeconds(1.5f);
    //    cellUI.SetActive(false);
    //    cellUIOn = false;
    //}

    //Navigate between upgrade Menus
    public void goPowUpgrade(GameObject pTarget)
    {
        pause.SetActive(false);
        statUpgrade.SetActive(false);
        powerUpgrade.SetActive(true);
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
