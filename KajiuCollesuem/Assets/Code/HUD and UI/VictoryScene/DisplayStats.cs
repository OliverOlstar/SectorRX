using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayStats : MonoBehaviour
{
    [SerializeField] private int _PlayerIndex;
    private SliderController _Sliders;

    void Awake()
    {
        _Sliders = GetComponent<SliderController>();
    }
    
    public void ShowStats()
    {
        // Run if player is in index
        if (connectedPlayers.playerIndex.Count > _PlayerIndex)
        {
            _Sliders = GetComponent<SliderController>();
            float[] stats = connectedPlayers.playerIndex[_PlayerIndex].victoryScene.Stats;

            Debug.Log(_Sliders == null);
            for (int i = 0; i < 5; i++)
            {
                float stat = stats[i];

                _Sliders.UpdateBars(i, stat);
            }
        }
    }
}
