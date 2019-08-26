using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public RectTransform mainMenu, optionsMenu, audioMenu, gameplayMenu, videoMenu;

    public void Start()
    {
        Time.timeScale = 1;
        mainMenu.DOAnchorPos(new Vector2(44, 0), 0.4f);
        BackToMainMenu();
    }

    public void BackToMainMenu()
    {
        optionsMenu.DOAnchorPos(new Vector2(1225, 0), 0.4f);
        audioMenu.DOAnchorPos(new Vector2(1225, 755), 0.4f);
        videoMenu.DOAnchorPos(new Vector2(1225, -755), 0.4f);
        gameplayMenu.DOAnchorPos(new Vector2(2450, 0), 0.4f);
        mainMenu.DOAnchorPos(new Vector2(44, 0), 0.4f);
    }

    public void GoToOptions()
    {
        optionsMenu.DOAnchorPos(new Vector2(44, 0), 0.4f);
        audioMenu.DOAnchorPos(new Vector2(44, 755), 0.4f);
        videoMenu.DOAnchorPos(new Vector2(44, -755), 0.4f);
        gameplayMenu.DOAnchorPos(new Vector2(1225, 0), 0.4f);
        mainMenu.DOAnchorPos(new Vector2(-1225, 0), 0.4f);
    }

    public void GoToAudio()
    {
        optionsMenu.DOAnchorPos(new Vector2(44, -755), 0.4f);
        audioMenu.DOAnchorPos(new Vector2(44, 0), 0.4f);
        videoMenu.DOAnchorPos(new Vector2(44, -1510), 0.4f);
        gameplayMenu.DOAnchorPos(new Vector2(1225, -755), 0.4f);
        mainMenu.DOAnchorPos(new Vector2(-1225, -755), 0.4f);
    }

    public void GoToGameplay()
    {
        optionsMenu.DOAnchorPos(new Vector2(-1225, 0), 0.4f);
        audioMenu.DOAnchorPos(new Vector2(-1225, 755), 0.4f);
        videoMenu.DOAnchorPos(new Vector2(-1225, -755), 0.4f);
        gameplayMenu.DOAnchorPos(new Vector2(44, 0), 0.4f);
        mainMenu.DOAnchorPos(new Vector2(-2450, 0), 0.4f);
    }

    public void GoToVideo()
    {
        optionsMenu.DOAnchorPos(new Vector2(44, 755), 0.4f);
        audioMenu.DOAnchorPos(new Vector2(44, 1510), 0.4f);
        videoMenu.DOAnchorPos(new Vector2(44, 0), 0.4f);
        gameplayMenu.DOAnchorPos(new Vector2(1225, 755), 0.4f);
        mainMenu.DOAnchorPos(new Vector2(-1225, 755), 0.4f);
    }
}
