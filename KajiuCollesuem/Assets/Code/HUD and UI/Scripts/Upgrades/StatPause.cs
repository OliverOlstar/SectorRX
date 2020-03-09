using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatPause : MonoBehaviour
{
    public GameObject[] statsMenu = new GameObject[7];

    private void Start()
    {
        foreach (GameObject stats in statsMenu)
        {
            stats.SetActive(false);
        }
    }

    public void ShowStatsOn()
    {
        foreach(GameObject stats in statsMenu)
        {
            stats.SetActive(true);
        }
    }

    public void ShowStatsOff()
    {
        foreach (GameObject stats in statsMenu)
        {
            stats.SetActive(false);
        }
    }
}
