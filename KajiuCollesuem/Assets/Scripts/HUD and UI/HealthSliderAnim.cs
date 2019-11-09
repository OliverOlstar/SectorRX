using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSliderAnim : MonoBehaviour
{
    public Slider healthSlider;

    //Have Health value adjust smoothly when value changes
    public void AnimateHP()
    {
        healthSlider.value = Mathf.MoveTowards(healthSlider.value, 0.0f, 0.1f);
    }
}
