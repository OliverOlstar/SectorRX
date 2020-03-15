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
Additional Programmers: Kavian Kermani, Scott Watman, Oliver Loescher
Description: Managing UI, loading, starting level, etc.
*/

public class UIManager : MonoBehaviour
{
    public static bool menuProperties;
    bool creditsRunning = false;
    bool onMainMenu = true;
    public RectTransform mainMenu, playerInputMenu, loadingScreen, logo, credits;
    public Animator logoAnim;
    public GameObject menuStartButton;
    public Button startButton;
    public VideoPlayer loadVideoPlayer;
    public VideoManager introVideoPlayer;
    private IEnumerator creditsCoroutine;
    [SerializeField] private MenuCamera _Camera;

    private Sequence logoSequence;
    private Sequence credSequence;

    private bool _LoadingScene = false;

    private int _PlayersReady = 0;

    public AudioClip[] combatant = new AudioClip[4];
    public AudioSource announceSource;

    public void Start()
    {
        logoSequence = DOTween.Sequence();
        credSequence = DOTween.Sequence();

        Time.timeScale = 1;
        logoAnim = logoAnim.GetComponent<Animator>();
        creditsCoroutine = RollCredits();

        if (menuProperties == true)
        {
            introVideoPlayer.background.gameObject.SetActive(false);
            introVideoPlayer.videoPlayer.gameObject.SetActive(false);
            playerInputMenu.DOAnchorPos(new Vector2(0, 0), 0.4f);
            EventSystem.current.SetSelectedGameObject(null);
            _Camera.ToggleCamera(1);
        }
        else
        {
            StartCoroutine(StartMenu());
        }
    }

    private void CancelCredits()
    {
        if (creditsRunning == true)
        {
            StopCoroutine(creditsCoroutine);
            logoSequence.Kill();
            credSequence.Kill();
            logo.DOKill();
            credits.DOKill();
            logo.DOAnchorPos(new Vector2(401, -2057), 0.01f);
            credits.DOAnchorPos(new Vector2(359, -1469), 0.01f);
            creditsRunning = false;
        }
    }

    public void RollTheCredits()
    {
        if (creditsRunning == false)
            StartCoroutine(RollCredits());
    }

    IEnumerator RollCredits()
    {
        if (onMainMenu == true)
        {
            logoSequence = DOTween.Sequence();
            credSequence = DOTween.Sequence();
            logoSequence.Append(logo.DOAnchorPos(new Vector2(401, 2857), 5.0f));
            credSequence.Append(credits.DOAnchorPos(new Vector2(359, 1469), 32.5f, false));
            logoSequence.Append(logo.DOAnchorPos(new Vector2(3119, 2857), 2.5f));
            logoSequence.Append(logo.DOAnchorPos(new Vector2(3119, -1943), 2.5f));
            logoSequence.Append(logo.DOAnchorPos(new Vector2(401, -1943), 12.5f));
            logoSequence.Append(logo.DOAnchorPos(new Vector2(401, 6), 0.4f));
            credSequence.Append(credits.DOAnchorPos(new Vector2(2059, 1469), 0.01f));
            credSequence.Append(credits.DOAnchorPos(new Vector2(2059, -1469), 0.01f));
            credSequence.Append(credits.DOAnchorPos(new Vector2(359, -1469), 0.01f));

            creditsRunning = true;
            yield return new WaitForSeconds(28.0f);
            creditsRunning = false;
        }
    }

    public void BackToMainMenu()
    {
        logo.DOAnchorPos(new Vector2(401, 6), 0.4f);
        mainMenu.DOAnchorPos(new Vector2(0, 21), 0.4f);
        onMainMenu = true;
        playerInputMenu.DOAnchorPos(new Vector2(0, 4120), 0.4f);
        EventSystem.current.SetSelectedGameObject(menuStartButton);
        _Camera.ToggleCamera(0);
    }

    public void GoToPlayer()
    {
        logo.DOAnchorPos(new Vector2(401, -2057), 0.4f);
        onMainMenu = false;
        CancelCredits();
        playerInputMenu.DOAnchorPos(new Vector2(0, 0), 0.4f);
        mainMenu.DOAnchorPos(new Vector2(0, -2060), 0.4f);
        menuProperties = true;
        EventSystem.current.SetSelectedGameObject(null);
        _Camera.ToggleCamera(1);
    }

    public void LoadLevel(int sceneIndex)
    {
        if (_LoadingScene == false && connectedPlayers.playersConnected <= _PlayersReady && _PlayersReady > 0)
        {
            loadVideoPlayer.Prepare();
            playerInputMenu.DOAnchorPos(new Vector2(0, -4120), 0.4f);
            loadingScreen.DOAnchorPos(new Vector2(0, 0), 0.4f);
            StartCoroutine(LoadAsyncLevel(sceneIndex));
        }
    }

    IEnumerator LoadAsyncLevel(int sceneIndex)
    {
        _LoadingScene = true;
        yield return new WaitForSeconds(0.5f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadVideoPlayer.Play();
        while (!operation.isDone)
        {
            if (loadVideoPlayer.isPlaying)
            {
                loadVideoPlayer.loopPointReached += EndReached;
                yield return new WaitForSeconds(2.0f);
            }

            yield return null;
        }
    }

    public void PlayerReadyToggle(bool pReady)
    {        
        //Announcer sound for player joining
        if(pReady)
        {
            announceSource.clip = combatant[_PlayersReady];
            announceSource.Play();
        }

        _PlayersReady += (pReady ? 1 : -1);
        PlayerReadyUpdateUI();
    }

    public void PlayerReadyUpdateUI()
    {
        startButton.interactable = (connectedPlayers.playersConnected <= _PlayersReady && _PlayersReady > 0);
    }

    void EndReached(UnityEngine.Video.VideoPlayer loadVideoPlayer)
    {
        StartCoroutine(CompleteLoadVisual());  
    }

    IEnumerator CompleteLoadVisual()
    {
        yield return new WaitForSeconds(0.75f);
        loadVideoPlayer.isLooping = false;
    }

    IEnumerator StartMenu()
    {
        yield return new WaitForSeconds(7.5f);
        logoAnim.enabled = false;
        mainMenu.DOAnchorPos(new Vector2(44, 21), 0.4f);
    }
 }
