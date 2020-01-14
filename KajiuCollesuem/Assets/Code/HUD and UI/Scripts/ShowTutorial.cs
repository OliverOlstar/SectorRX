using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTutorial : MonoBehaviour
{
    public GameObject tutorialUI = null;
    public Text tutorialText;
    PauseMenu pauseMenu;

    [SerializeField] [TextArea] private string myText;

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
        if(other.tag == "Player")
        {
            tutorialUI.SetActive(true);
            tutorialText.text = myText;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
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
