using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDisplayPlayers : MonoBehaviour
{
    [SerializeField] private VictoryLizzy[] victoryLizzys = new VictoryLizzy[6];
    [SerializeField] private float displayLizzyWidth = 80f;

    [Space]
    [SerializeField] private DisplayStats[] victoryStats = new DisplayStats[6];
    [SerializeField] private float displayStatsWidth = 120f;

    void Start()
    {
        SetStats();
        SetModels();
    }

    private void SetStats()
    {
        float spacing = 1 / ((float)connectedPlayers.playersConnected + 1);

        for (int i = 0; i < victoryStats.Length; i++)
        {
            if (i < connectedPlayers.playersConnected)
            {
                // Set Postion
                RectTransform statTransform = victoryStats[i].transform.parent.GetComponent<RectTransform>();
                float x = Mathf.Lerp(-displayStatsWidth, displayStatsWidth, spacing * (i + 1));
                statTransform.localPosition = new Vector2(x, statTransform.localPosition.y);

                // Set Stats
                victoryStats[i].ShowStats();
            }
            else
            {
                // Make Invisible
                victoryStats[i].gameObject.SetActive(false);
            }
        }
    }

    private void SetModels()
    {
        float spacing = 1 / ((float)connectedPlayers.playersConnected + 1);

        for (int i = 0; i < victoryLizzys.Length; i++)
        {
            if (i < connectedPlayers.playersConnected)
            {
                // Set Postion
                float x = Mathf.Lerp(-displayLizzyWidth, displayLizzyWidth, spacing * (i + 1));
                victoryLizzys[i].transform.localPosition = new Vector2(x, victoryLizzys[i].transform.localPosition.y);

                // Set Visuals
                victoryLizzys[i].SetLizzy(connectedPlayers.playerIndex[i]);
            }
            else
            {
                // Play not in so don't show
                victoryLizzys[i].gameObject.SetActive(false);
            }
        }
    }
}
