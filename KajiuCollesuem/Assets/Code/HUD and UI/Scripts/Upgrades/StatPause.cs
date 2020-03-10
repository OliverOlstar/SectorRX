using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatPause : MonoBehaviour
{
    [SerializeField] private GameObject[] _StatsMenu = new GameObject[7];
    [SerializeField] private RectTransform _StatParent;

    public void ShowStatsOn()
    {
        foreach (GameObject stats in _StatsMenu)
        {
            stats.SetActive(true);
        }
        
        if(connectedPlayers.playersConnected > 1)
        {
            _StatParent.anchorMin = new Vector2(0.5f, 0.5f);
            _StatParent.anchorMax = new Vector2(0.5f, 0.5f);
            _StatParent.localPosition = new Vector2(-125, 0);
        }
    }

    public void ShowStatsOff()
    {
        foreach (GameObject stats in _StatsMenu)
        {
            stats.SetActive(false);
        }

        if(connectedPlayers.playersConnected > 1)
        {
            _StatParent.anchorMin = new Vector2(0.0f, 0.5f);
            _StatParent.anchorMax = new Vector2(0.0f, 0.5f);
            _StatParent.localPosition = new Vector2(-434.7542f, 0);
        }
    }
}
