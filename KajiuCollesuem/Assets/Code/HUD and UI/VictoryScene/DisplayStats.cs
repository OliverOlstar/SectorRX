using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayStats : MonoBehaviour
{
    [SerializeField] private int _PlayerIndex;
    private SliderController _Sliders;
    private RectTransform _Transform;

    [SerializeField] private float _OffScreenY = 1.0f;
    private float _OnScreenY = 1.0f;
    [SerializeField] private float _FallDampening = 2.0f;

    void Awake()
    {
        _Sliders = GetComponent<SliderController>();
        _Transform = transform.parent.GetComponent<RectTransform>();
        _OnScreenY = _Transform.localPosition.y;
    }
    
    public void ShowStats(float pDelay)
    {
        _Transform.localPosition = new Vector2(_Transform.localPosition.x, _OffScreenY);
        StartCoroutine(fallDelay(pDelay));
    }

    private IEnumerator fallDelay(float pDelay)
    {
        yield return new WaitForSeconds(pDelay);

        // Run if player is in index
        if (connectedPlayers.playerIndex.Count > _PlayerIndex)
        {
            _Sliders = GetComponent<SliderController>();
            float[] stats = connectedPlayers.playerIndex[_PlayerIndex].victoryScene.Stats;

            for (int i = 0; i < 5; i++)
            {
                float stat = stats[i];

                _Sliders.UpdateBars(i, stat);
            }
        }

        while (Mathf.Abs(_Transform.localPosition.y - _OnScreenY) > 0.001f)
        {
            _Transform.localPosition = Vector2.Lerp(_Transform.localPosition, new Vector2(_Transform.localPosition.x, _OnScreenY), Time.deltaTime * _FallDampening);
            yield return null;
        }
    }
}
