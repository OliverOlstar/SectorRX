using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    //public RectTransform pauseMenu, optionsMenu, powerMenu, skillMenu;
    public GameObject pause, option, power, skill;

    private void Start()
    {
        option.SetActive(false);
        power.SetActive(false);
        skill.SetActive(false);
    }

    public void GoToOptions()
    {
        pause.SetActive(false);
        option.SetActive(true);
    }

    public void GoToPowers()
    {
        pause.SetActive(false);
        power.SetActive(true);
    }

    public void GoToSkills()
    {
        pause.SetActive(false);
        skill.SetActive(true);
    }

    public void BackToPause()
    {
        pause.SetActive(true);
        option.SetActive(false);
        power.SetActive(false);
        skill.SetActive(false);
    }
}
