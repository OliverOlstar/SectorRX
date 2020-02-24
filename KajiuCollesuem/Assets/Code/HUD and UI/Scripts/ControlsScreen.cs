using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlsScreen : MonoBehaviour
{
    public GameObject backButton;
    public GameObject targetUI;

    private void Update()
    {
        if (targetUI != null)
        {
            EventSystem.current.SetSelectedGameObject(targetUI);
            targetUI = null;
        }
        targetUI = backButton;
    }
}
