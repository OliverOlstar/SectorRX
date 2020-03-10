using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Panels : MonoBehaviour
{
    [SerializeField] int playerNumber;
    [SerializeField] private connectedPlayers _AddPlayer;
    [SerializeField] private UIManager _CanPlayChecker;
    [SerializeField] private Text playerPanels;
    private int stateValue = 0;
    [SerializeField] private MenuLizzy _myLizzy;
    [HideInInspector] public ColorSet myColorSet;

    [HideInInspector] public DeviceHandler myDevice = null;

    [SerializeField] private Sprite[] abilityIcons;
    [SerializeField] private SpriteRenderer ability;
    [SerializeField] private RectTransform abilityOneRect, abilityTwoRect;
    [SerializeField] private RectTransform dPadLeftRect, dPadRightRect;
    [HideInInspector] public int abilityNumber = 0;
    [SerializeField] private Animator animShield;
    [SerializeField] private Animator animMask;
    private SpriteRenderer _animMaskRenderer;

    [SerializeField] private AudioClip[] lockedIn = new AudioClip[4];
    [SerializeField] private AudioClip combatantHere;
    [SerializeField] private AudioSource sfxSource;


    [SerializeField] private ColorPicker _ColorPicker;
    private int _CurrentColorIndex = 0;

    private void Start()
    {
        _animMaskRenderer = animMask.GetComponent<SpriteRenderer>();
        
        UpdateIcons();

        RemoveAbilitiesUI();

        ColorSet set = _ColorPicker.StartingColor();
        SetColors(set);
        _myLizzy.SetAbilities(0);
    }

    public void OnJoining()
    {
        switch (stateValue)
        {
            case 0:
                // Player Locked In
                if(stateValue == 0)
                {
                    stateValue = 1;

                    sfxSource.clip = lockedIn[Random.Range(0, 3)];
                    sfxSource.volume = Random.Range(0.6f, 0.8f);
                    sfxSource.PlayDelayed(0.25f);
                    playerPanels.text = "READY!";
                    StartCoroutine(RemoveAbilitiesUI());
                    _myLizzy.ChangeWeights(MenuLizzy.menuLizzyStates.LockedIn);

                    animShield.SetBool("hasJoined", true);
                    animMask.SetBool("maskJoined", true);

                    _CanPlayChecker.PlayerReadyToggle(true);
                }
                break;
            
            case 1:
                // Player Enters to Start Match
                _AddPlayer.SetPlayerOrder();
                SceneManager.LoadSceneAsync(1);
                break;
        }
    }

    public void OnColorPicking()
    {
        ColorSet set = _ColorPicker.SwitchColor(_CurrentColorIndex);
        SetColors(set);
    }

    private void SetColors(ColorSet pSet)
    {
        _CurrentColorIndex = pSet.index;
        myColorSet = pSet;
        _myLizzy.SetColors(pSet);
        _animMaskRenderer.color = pSet.color;
    }

    public void OnLeft()
    {
        if(stateValue == 0)
        {
            ChangeIcons(-1);
            _myLizzy.SetAbilities(abilityNumber);
        }
    }

    public void OnRight()
    {
        if (stateValue == 0)
        {
            ChangeIcons(1);
            _myLizzy.SetAbilities(abilityNumber);
        }
    }

    public void ChangeIcons(int pDirection)
    {
        abilityNumber += pDirection;

        // Check if outside bounds
        if(abilityNumber < 0)
        {
            abilityNumber = abilityIcons.Length - 1;
        }
        else if(abilityNumber >= abilityIcons.Length)
        {
            abilityNumber = 0;
        }

        UpdateIcons();
    }

    private void UpdateIcons()
    {
        ability.sprite = abilityIcons[abilityNumber];
    }

    // Player Enters To Join
    public void PlayerJoined(DeviceHandler pDevice)
    {
        myDevice = pDevice;

        playerPanels.text = " ";

        // Abilities
        ShowAbilitiesUI();

        _myLizzy.ChangeWeights(MenuLizzy.menuLizzyStates.Joined);

        _CanPlayChecker.PlayerReadyUpdateUI();
    }

    // Player Enters to Leave Match
    public int PlayerLeft()
    {
        myDevice = null;

        playerPanels.text = "Press 'Space'\n or \n'Start' to Join";
        stateValue = 0;

        _myLizzy.ChangeWeights(MenuLizzy.menuLizzyStates.NotJoined);

        StartCoroutine(RemoveAbilitiesUI());

        animShield.SetBool("hasJoined", false);
        animMask.SetBool("maskJoined", false);

        if (stateValue == 0)
            _CanPlayChecker.PlayerReadyToggle(false);

        return playerNumber - 1;
    }

    private void ShowAbilitiesUI()
    {
        StopAllCoroutines();
        abilityOneRect.gameObject.SetActive(true);
        dPadLeftRect.gameObject.SetActive(true);
        //abilityTwoRect.gameObject.SetActive(true);
        dPadRightRect.gameObject.SetActive(true);
        CancelPreviousAbilitiesTweens();
        abilityOneRect.DOAnchorPos(new Vector2(0, -30), 0.4f);
        dPadLeftRect.DOAnchorPos(new Vector2(-157, -201), 0.4f);
        //abilityTwoRect.DOAnchorPos(new Vector2(0, -145), 0.4f);
        dPadRightRect.DOAnchorPos(new Vector2(157, -201), 0.4f);
    }

    IEnumerator RemoveAbilitiesUI()
    {
        CancelPreviousAbilitiesTweens();
        abilityOneRect.DOAnchorPos(new Vector2(0, -1930), 1.6f);
        dPadLeftRect.DOAnchorPos(new Vector2(-157, -2131), 1.6f);
        //abilityTwoRect.DOAnchorPos(new Vector2(0, -2110), 1.6f);
        dPadRightRect.DOAnchorPos(new Vector2(157, -2131), 1.6f);
        yield return new WaitForSeconds(1.6f);
        abilityOneRect.gameObject.SetActive(false);
        dPadLeftRect.gameObject.SetActive(false);
        //abilityTwoRect.gameObject.SetActive(false);
        dPadRightRect.gameObject.SetActive(false);
    }

    private void CancelPreviousAbilitiesTweens()
    {
        abilityOneRect.DOKill();
        dPadLeftRect.DOKill();
        //abilityTwoRect.DOKill();
        dPadRightRect.DOKill();
    }
}