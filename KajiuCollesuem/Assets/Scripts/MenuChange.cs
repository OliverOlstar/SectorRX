using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuChange : MonoBehaviour
{
    public void SetMenuOff()
    {
        gameObject.SetActive(false);
    }

    public void SetMenuOn()
    {
        gameObject.SetActive(true);
    }
}
