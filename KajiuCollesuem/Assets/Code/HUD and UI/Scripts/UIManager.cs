using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public RectTransform mainMenu, optionsMenu, audioMenu, gameplayMenu, videoMenu, playerInputMenu;
    public GameObject targetUI;

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
        optionsMenu.DOAnchorPos(new Vector2(1225, 0), 0.4f);
        audioMenu.DOAnchorPos(new Vector2(1225, 755), 0.4f);
        videoMenu.DOAnchorPos(new Vector2(1225, -755), 0.4f);
        gameplayMenu.DOAnchorPos(new Vector2(2450, 0), 0.4f);
        mainMenu.DOAnchorPos(new Vector2(44, 0), 0.4f);
        playerInputMenu.DOAnchorPos(new Vector2(44, 755), 0.4f);
        targetUI = pTarget;
    }

    public void GoToOptions(GameObject pTarget)
    {
        optionsMenu.DOAnchorPos(new Vector2(44, 0), 0.4f);
        audioMenu.DOAnchorPos(new Vector2(44, 755), 0.4f);
        videoMenu.DOAnchorPos(new Vector2(44, -755), 0.4f);
        gameplayMenu.DOAnchorPos(new Vector2(1225, 0), 0.4f);
        mainMenu.DOAnchorPos(new Vector2(-1225, 0), 0.4f);
        targetUI = pTarget;
    }

    public void GoToAudio(GameObject pTarget)
    {
        optionsMenu.DOAnchorPos(new Vector2(44, -755), 0.4f);
        audioMenu.DOAnchorPos(new Vector2(44, 0), 0.4f);
        videoMenu.DOAnchorPos(new Vector2(44, -1510), 0.4f);
        gameplayMenu.DOAnchorPos(new Vector2(1225, -755), 0.4f);
        mainMenu.DOAnchorPos(new Vector2(-1225, -755), 0.4f);
        targetUI = pTarget;
    }

    public void GoToGameplay(GameObject pTarget)
    {
        optionsMenu.DOAnchorPos(new Vector2(-1225, 0), 0.4f);
        audioMenu.DOAnchorPos(new Vector2(-1225, 755), 0.4f);
        videoMenu.DOAnchorPos(new Vector2(-1225, -755), 0.4f);
        gameplayMenu.DOAnchorPos(new Vector2(44, 0), 0.4f);
        mainMenu.DOAnchorPos(new Vector2(-2450, 0), 0.4f);
        targetUI = pTarget;
    }

    public void GoToVideo(GameObject pTarget)
    {
        optionsMenu.DOAnchorPos(new Vector2(44, 755), 0.4f);
        audioMenu.DOAnchorPos(new Vector2(44, 1510), 0.4f);
        videoMenu.DOAnchorPos(new Vector2(44, 0), 0.4f);
        gameplayMenu.DOAnchorPos(new Vector2(1225, 755), 0.4f);
        mainMenu.DOAnchorPos(new Vector2(-1225, 755), 0.4f);
        targetUI = pTarget;
    }

    public void GoToPlayer(GameObject pTarget)
    {
        playerInputMenu.DOAnchorPos(new Vector2(44, 0), 0.4f);
        mainMenu.DOAnchorPos(new Vector2(44, -755), 0.4f);
        targetUI = pTarget;
    }
}
