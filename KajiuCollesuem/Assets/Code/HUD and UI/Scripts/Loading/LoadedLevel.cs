using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadedLevel : MonoBehaviour
{
    public RectTransform screenTransition;
    public AudioClip readyGo;
    public AudioSource announceSource;
    
    void Start()
    {
        StartCoroutine(screenMove());
        announceSource.clip = readyGo;
        announceSource.PlayDelayed(3.0f);
    }

    IEnumerator screenMove()
    {
        yield return new WaitForSeconds(1);
        screenTransition.DOAnchorPos(new Vector2(0, 4120), 3.2f);
    }
}
