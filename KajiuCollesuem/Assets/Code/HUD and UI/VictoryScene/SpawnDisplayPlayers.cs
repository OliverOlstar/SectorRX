using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnDisplayPlayers : MonoBehaviour
{
    [SerializeField] private VictoryLizzy[] victoryLizzys = new VictoryLizzy[6];
    [SerializeField] private VictoryLizzyPositioner[] victoryLizzysPostioners = new VictoryLizzyPositioner[6];
    [SerializeField] private float displayLizzyWidth = 80f;
    [SerializeField] private float displayLizzyHeight = 80f;

    [Space]
    [SerializeField] private DisplayStats[] victoryStats = new DisplayStats[6];
    [SerializeField] private Image[] victoryStatsPanel = new Image[6];
    [SerializeField] private float displayStatsWidth = 120f;

    void Start()
    {
        if (connectedPlayers.playersConnected <= 0)
        {
            connectedPlayers.playersConnected = 6;

            connectedPlayers.playerIndex.Clear();
            UsedDevices player = new UsedDevices();

            for (int i = 0; i < connectedPlayers.playersConnected; i++)
            {
                player.deviceUser = i;
                player.playerIndex = i;
                player.playerColorSet = new ColorSet();
                player.abilitySelected = Mathf.RoundToInt(Random.value * 2);

                player.victoryScene.Alive = (Random.value > 0.2f ? false : true);
                player.victoryScene.TimeOfDeath = Random.value;
                player.abilitySelected = Mathf.FloorToInt(Random.value * 3);
                player.playerColorSet.color = new Color(Random.value, Random.value, Random.value);

                float[] stats = new float[8];
                for (int z = 0; z < stats.Length; z++)
                    stats[z] = Mathf.Round(Random.value * 30);
                player.victoryScene.Stats = stats;

                connectedPlayers.playerIndex.Add(player);
            }
        }

        SetStats(connectedPlayers.playersConnected + 1.4f);
        SetModels();
        SetPositioners(2.4f);
    }

    private void SetStats(float pDelay)
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
                victoryStats[i].ShowStats(pDelay);

                // Set Color
                Color color = connectedPlayers.playerIndex[i].playerColorSet.color;
                color.a = victoryStatsPanel[i].color.a;
                victoryStatsPanel[i].color = color;
            }
            else
            {
                // Make Invisible
                victoryStats[i].transform.parent.gameObject.SetActive(false);
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

    private void SetPositioners(float pDelay)
    {
        List<int> indexOrder = new List<int>();
        
        for (int i = 0; i < connectedPlayers.playerIndex.Count; i++)
        {
            indexOrder.Add(i);
        }

        for (int i = 0; i < connectedPlayers.playerIndex.Count; i++)
        {
            float smallestValue = 999999999;
            int smallestIndex = 0;

            foreach (int index in indexOrder) 
            {
                float value = connectedPlayers.playerIndex[index].victoryScene.TimeOfDeath;

                if (value < smallestValue)
                {
                    smallestValue = value;
                    smallestIndex = index;
                }
            }

            indexOrder.Remove(smallestIndex);
            victoryLizzysPostioners[smallestIndex].SetPosition(pDelay, (i * 3.4f) + displayLizzyHeight, (i == connectedPlayers.playerIndex.Count - 1) ? true : false);
        }
    }
}
