using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
Programmer: Kavian Kermani
Additional Programmers: Oliver Loescher
Description: Managing sliders to contain a mask and lower, conveying visual cue of damage done to players
*/

public class SliderController : MonoBehaviour
{
    const int BAR_HEIGHT = 20;
    public float barLengthMultiplier = 1.5f;

    [SerializeField] private RectTransform[] BarRect;
    [SerializeField] private RectTransform[] MaskBarRect;

    public Slider[] RegSlider;
    public Slider[] MaskSlider;

    private Coroutine[] _lerpCoroutineArray = new Coroutine[4];

    public float lerpTimer = 1;

    public bool isLerping;

    public int lerpSpeed = 1;
    private int sliderIndex;

    public void SetBar(int pIndex, float pValue)
    {
        RegSlider[pIndex].maxValue = pValue;
        MaskSlider[pIndex].maxValue = pValue;
    }

    public void SetBars(int pIndex, float pValue)
    {
        BarRect[pIndex] = BarRect[pIndex].gameObject.GetComponent<RectTransform>();
        BarRect[pIndex].sizeDelta = new Vector2(pValue * barLengthMultiplier, BAR_HEIGHT);
        MaskBarRect[pIndex] = MaskBarRect[pIndex].gameObject.GetComponent<RectTransform>();
        MaskBarRect[pIndex].sizeDelta = new Vector2(pValue * barLengthMultiplier, BAR_HEIGHT);
        SetBar(pIndex, pValue);
    }

    public void UpdateBars(int pIndex, float pValue)
    {
        RegSlider[pIndex].value = pValue;

        if (_lerpCoroutineArray[pIndex] != null)
        {
            StopCoroutine(_lerpCoroutineArray[pIndex]);
        }
        _lerpCoroutineArray[pIndex] = StartCoroutine(LerpMaskSlider(MaskSlider[pIndex], pValue));
    }

    IEnumerator LerpMaskSlider(Slider pBar, float pValue)
    {
        yield return new WaitForSeconds(lerpTimer);

        float curValue = pBar.value;

        while (curValue >= pValue + 0.001f || curValue <= pValue - 0.001f)
        {
            curValue = Mathf.Lerp(curValue, pValue, Time.deltaTime * lerpSpeed);
            pBar.value = curValue;
            yield return null;
        }

        pBar.value = pValue;
    }
}
