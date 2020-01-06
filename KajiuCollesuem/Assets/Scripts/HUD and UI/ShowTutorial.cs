using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTutorial : MonoBehaviour
{
    public GameObject tutorialUI = null;
    public Text tutorialText;
    PauseMenu pauseMenu;

    private void Start()
    {
        tutorialUI.SetActive(false);
        pauseMenu = GameObject.FindGameObjectWithTag("HUD").GetComponent<PauseMenu>();
    }

    private void Update()
    {
        if(pauseMenu.pause)
        {
            tutorialUI.SetActive(false);
            tutorialText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DashTutorial")
        {
            tutorialUI.SetActive(true);
            tutorialText.text = "Press Space or 'B' to Dash";
        }
        if(other.tag == "LockOnTutorial")
        {
            tutorialUI.SetActive(true);
            tutorialText.text = "Right Click or Press the Right Stick to lock on to enemies";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "DashTutorial")
        {
            tutorialUI.SetActive(false);
        }
    }

    private bool isPanelActive
    {
        get
        {
            return tutorialUI.activeInHierarchy;
        }
    }
}
