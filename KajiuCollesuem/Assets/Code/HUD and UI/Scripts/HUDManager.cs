using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;

public class HUDManager : MonoBehaviour
{
    //public RectTransform pauseMenu, optionsMenu, powerMenu, skillMenu;
    public GameObject pause, /*option, ability, videoOP, audioOP gameplayOP,*/ cellUI, powerSelect;
    public GameObject resumeButton;
    public GameObject targetUI;
    public Slider cellExp;
    public Text cellCount, canUpgrade, upCellCount;

    [SerializeField] private PlayerCamera mainCam;

    private void Start()
    {
        //option.SetActive(false);
        //ability.SetActive(false);
    }

    private void Update()
    {
        if (targetUI != null)
        {
            EventSystem.current.SetSelectedGameObject(targetUI);
            targetUI = null;
        }
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
        targetUI = pTarget;
    }

    public void goStatUpgrade(GameObject pTarget)
    {
        pause.SetActive(false);
        cellCount.gameObject.SetActive(true);
        targetUI = pTarget;
    }

    public void PowerToStat(GameObject pTarget)
    {
        targetUI = pTarget;
    }

    public void StatToPower(GameObject pTarget)
    {
        targetUI = pTarget;
    }

    public void BackToPause(GameObject pTarget)
    {
        pause.SetActive(true);
        //option.SetActive(false);
        //ability.SetActive(false);
        targetUI = pTarget;
    }

    public void ResumeGame()
    {
        //pauseMenu.TogglePause();
    }
}
