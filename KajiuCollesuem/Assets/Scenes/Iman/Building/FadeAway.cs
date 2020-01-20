using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAway : MonoBehaviour
{
    [SerializeField] private int SecondsBeforeFade;

    private void OnEnable()
    {
        StartCoroutine("Fade");
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(2);
        for (float ft = 1f; ft >= 0; ft -= 0.01f)
        {
            Color c = GetComponent<Renderer>().material.color;
            c.a = ft;
            GetComponent<Renderer>().material.color = c;
            yield return null;
        }
        //add to 
    }
}
