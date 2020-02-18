using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TransitionDown : MonoBehaviour
{
    public RectTransform screenTransition;

    public void Start()
    {
        Time.timeScale = 1;
    }

    public void Transition()
    {
        StartCoroutine(screenMove());
    }

    IEnumerator screenMove()
    {
        screenTransition.DOAnchorPos(new Vector2(0, -1280), 0.4f);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
    }
}