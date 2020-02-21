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
        gameOverTransition.DOAnchorPos(new Vector2(0, 0), 0.4f);
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
        gameOverTransition.DOAnchorPos(new Vector2(0, -640), 0.4f);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync(0);

    }

    IEnumerator screenPausedMove()
    {
        Time.timeScale = 1;
        gamePausedTransition.DOAnchorPos(new Vector2(0, 0), 0.4f);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync(0);
    }
}