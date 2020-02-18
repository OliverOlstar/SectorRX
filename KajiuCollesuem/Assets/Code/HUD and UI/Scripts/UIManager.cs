using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using DG.Tweening;

/*
Programmer: 
Additional Programmers: Kavian Kermani, Scott Watman
Description: Managing UI, loading, starting level, etc.
*/

public class UIManager : MonoBehaviour
{
    public RectTransform mainMenu, playerInputMenu, loadingScreen;
    public GameObject targetUI;
    public Slider loadingProgress;

    public void Start()
    {
        Time.timeScale = 1;
        mainMenu.DOAnchorPos(new Vector2(44, 0), 0.4f);
        BackToMainMenu(targetUI);
    }

    private void Update()
    {
        if (targetUI != null)
        {
            EventSystem.current.SetSelectedGameObject(targetUI);
            targetUI = null;
        }
    }

    public void SetTarget(GameObject pTarget)
    {
        targetUI = pTarget;
    }

    public void BackToMainMenu(GameObject pTarget)
    {
        mainMenu.DOAnchorPos(new Vector2(44, 0), 0.4f);
        playerInputMenu.DOAnchorPos(new Vector2(71, 823), 0.4f);
        targetUI = pTarget;
    }

    public void GoToPlayer(GameObject pTarget)
    {
        playerInputMenu.DOAnchorPos(new Vector2(71, 0), 0.4f);
        mainMenu.DOAnchorPos(new Vector2(44, -755), 0.4f);
        targetUI = pTarget;
    }

    public void LoadLevel(int sceneIndex)
    {
        playerInputMenu.DOAnchorPos(new Vector2(71, -823), 0.4f);
        loadingScreen.DOAnchorPos(new Vector2(44, 0), 0.4f);
        StartCoroutine(LoadAsyncLevel(sceneIndex));
    }

    IEnumerator LoadAsyncLevel(int sceneIndex)
    {
        yield return new WaitForSeconds(0.8f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            //show logo loader
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingProgress.value = progress;
            yield return null;
        }

        if (operation.isDone)
        {
            //show broken logo
        }
    }
}
