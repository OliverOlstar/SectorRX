using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Video;
using DG.Tweening;

/*
Programmer: 
Additional Programmers: Kavian Kermani, Scott Watman
Description: Managing UI, loading, starting level, etc.
*/

public class UIManager : MonoBehaviour
{
    public static bool menuProperties;
    public RectTransform mainMenu, playerInputMenu, loadingScreen, logo;
    public Animator logoAnim;
    public GameObject targetUI, backButton;
    public VideoPlayer videoPlayer;
    public bool panelCheck = false;

    public void Start()
    {
        logoAnim = logoAnim.GetComponent<Animator>();
        Time.timeScale = 1;
        if (menuProperties == true)
        {
            playerInputMenu.DOAnchorPos(new Vector2(69, -2), 0.4f);
            targetUI = backButton;
        }
        else
        {
            StartCoroutine(StartMenu());
        }
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
        panelCheck = true;
        logo.DOAnchorPos(new Vector2(401, 6), 0.4f);
        mainMenu.DOAnchorPos(new Vector2(44, 21), 0.4f);
        playerInputMenu.DOAnchorPos(new Vector2(69, 4120), 0.4f);
        menuProperties = false;
        targetUI = pTarget;
    }

    public void GoToPlayer(GameObject pTarget)
    {
        panelCheck = false;
        logo.DOAnchorPos(new Vector2(401, -2057), 0.4f);
        playerInputMenu.DOAnchorPos(new Vector2(69, -2), 0.4f);
        mainMenu.DOAnchorPos(new Vector2(44, -2060), 0.4f);
        menuProperties = true;
        targetUI = pTarget;
    }

    public void LoadLevel(int sceneIndex)
    {
        videoPlayer.Prepare();
        playerInputMenu.DOAnchorPos(new Vector2(71, -4120), 0.4f);
        loadingScreen.DOAnchorPos(new Vector2(0, 0), 0.4f);
        StartCoroutine(LoadAsyncLevel(sceneIndex));
    }

    IEnumerator LoadAsyncLevel(int sceneIndex)
    {
        yield return new WaitForSeconds(0.5f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        videoPlayer.Play();
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            if (videoPlayer.isPlaying)
            {
                videoPlayer.loopPointReached += EndReached;
                yield return new WaitForSeconds(2.0f);
            }

            yield return null;
        }
    }

    void EndReached(UnityEngine.Video.VideoPlayer videoPlayer)
    {
        StartCoroutine(CompleteLoadVisual());  
    }

    IEnumerator CompleteLoadVisual()
    {
        yield return new WaitForSeconds(0.75f);
        videoPlayer.isLooping = false;
    }

    IEnumerator StartMenu()
    {
        yield return new WaitForSeconds(7.5f);
        logoAnim.enabled = false;
        mainMenu.DOAnchorPos(new Vector2(44, 21), 0.4f);
    }
 }
