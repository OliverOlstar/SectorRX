using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TransitionDown : MonoBehaviour
{
    public RectTransform gameOverTransition;
    public RectTransform gamePausedTransition;

    public void Start()
    {
        Time.timeScale = 1;
    }

    public void GameOverTransition()
    {
        StartCoroutine(screenOverMove());
    }

    public void GamePausedTransition()
    {
        StartCoroutine(screenPausedMove());
    }

    IEnumerator screenOverMove()
    {
        gameOverTransition.DOAnchorPos(new Vector2(0, -1280), 0.4f);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
    }

    IEnumerator screenPausedMove()
    {
        gamePausedTransition.DOAnchorPos(new Vector2(0, 0), 0.4f);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
    }
}