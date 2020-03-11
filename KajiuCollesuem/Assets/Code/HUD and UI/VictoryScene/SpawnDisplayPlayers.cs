using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDisplayPlayers : MonoBehaviour
{
    [SerializeField] private VictoryLizzy[] victoryLizzys = new VictoryLizzy[6];
    [SerializeField] private float displayLizzySpacing = 80f;
    [SerializeField] private float playerCountSpacingLizzyMult = 2f;

    [Space]
    [SerializeField] private DisplayStats[] victoryStats = new DisplayStats[6];
    [SerializeField] private float displayStatsSpacing = 80f;
    [SerializeField] private float playerCountSpacingStatsMult = 2f;

    // Start is called before the first frame update
    void Start()
    {
        SetStats();
        SetModels();
    }

    private void SetStats()
    {
        float spacing = Mathf.Lerp(playerCountSpacingStatsMult, 0, ((float)connectedPlayers.playersConnected) / 6) + displayStatsSpacing;
        float statsXOffset = ((float)connectedPlayers.playersConnected - 1) * -spacing;

        for (int i = 0; i < victoryStats.Length; i++)
        {
            if (i < connectedPlayers.playersConnected)
            {
                // Set Postion
                RectTransform statTransform = victoryStats[i].transform.parent.GetComponent<RectTransform>();
                Vector2 pos = statTransform.localPosition;
                statTransform.localPosition = new Vector2(statsXOffset + (i * 2 * spacing), pos.y);

                // Set Stats
                victoryStats[i].ShowStats();

            }
            else
            {
                // Make Visible
                victoryStats[i].gameObject.SetActive(false);
            }
        }
    }

    private void SetModels()
    {
        float spacing = Mathf.Lerp(playerCountSpacingLizzyMult, 0, (float)connectedPlayers.playersConnected / 6) + displayLizzySpacing;
        float modelsXOffset = ((float)connectedPlayers.playersConnected - 1) * -spacing;

        for (int i = 0; i < victoryLizzys.Length; i++)
        {
            if (i < connectedPlayers.playersConnected)
            {
                // Set Postion
                victoryLizzys[i].transform.localPosition = new Vector2(modelsXOffset + (i * 2 * spacing), victoryLizzys[i].transform.localPosition.y);

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
