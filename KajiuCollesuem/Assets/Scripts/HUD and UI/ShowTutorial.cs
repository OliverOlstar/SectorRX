using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTutorial : MonoBehaviour
{
    public GameObject tutorialUI = null;

    private void Start()
    {
        tutorialUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            tutorialUI.SetActive(true);
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
