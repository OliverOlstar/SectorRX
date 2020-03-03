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
    public Text playerPanels;
    private int stateValue = 0;
    [SerializeField] private MenuLizzy _myLizzy;
    [HideInInspector] public ColorSet myColorSet;

    [HideInInspector] public DeviceHandler myDevice = null;

    public Sprite[] abilityIcons;
    public SpriteRenderer[] ability = new SpriteRenderer[2];
    public RectTransform abilityOneRect, abilityTwoRect;
    public RectTransform dPadLeftRect, dPadRightRect;
    private int presetNumber = 0;
    public bool setOne;
    public bool setTwo;
    public bool abilityLocked;
    public Animator animShield;
    public Animator animMask;
    private SpriteRenderer _animMaskRenderer;

    public AudioClip[] lockedIn = new AudioClip[4];
    public AudioSource sfxSource;

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
                    sfxSource.clip = lockedIn[Random.Range(0, 3)];
                    sfxSource.volume = Random.Range(0.6f, 0.8f);
                    sfxSource.PlayDelayed(0.25f);
                    playerPanels.text = "READY!";
                    RemoveAbilitiesUI();
                    abilityLocked = true;
                    stateValue = 1;
                    _myLizzy.ChangeWeights(MenuLizzy.menuLizzyStates.LockedIn);

                    animShield.SetBool("hasJoined", true);
                    animMask.SetBool("maskJoined", true);
                }
                break;
            
            case 1:
                // Player Enters to Start Match
                if (connectedPlayers.playersConnected >= 2 && abilityLocked)
                {
                    _AddPlayer.SetPlayerOrder();
                    SceneManager.LoadSceneAsync(1);
                }
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
            _myLizzy.SetAbilities(presetNumber);
        }
    }

    public void OnRight()
    {
        if (stateValue == 0)
        {
            ChangeIcons(1);
            _myLizzy.SetAbilities(presetNumber);
        }
    }

    public void ChangeIcons(int pDirection)
    {
        presetNumber += pDirection;

        // Check if outside bounds
        if(presetNumber < 0)
        {
            presetNumber = abilityIcons.Length / 2 - 1;
        }
        else if(presetNumber >= abilityIcons.Length / 2)
        {
            presetNumber = 0;
        }

        UpdateIcons();
    }

    private void UpdateIcons()
    {
        ability[0].GetComponent<SpriteRenderer>().sprite = abilityIcons[presetNumber * 2];
        ability[1].GetComponent<SpriteRenderer>().sprite = abilityIcons[presetNumber * 2 + 1];
    }

    // Player Enters To Join
    public void PlayerJoined(DeviceHandler pDevice)
    {
        myDevice = pDevice;

        playerPanels.text = " ";

        // Abilities
        ShowAbilitiesUI();

        _myLizzy.ChangeWeights(MenuLizzy.menuLizzyStates.Joined);
    }

    // Player Enters to Leave Match
    public int PlayerLeft()
    {
        myDevice = null;

        playerPanels.text = "Press 'Space'\n or \n'Start' to Join";
        stateValue = 0;

        _myLizzy.ChangeWeights(MenuLizzy.menuLizzyStates.NotJoined);

        RemoveAbilitiesUI();

        animShield.SetBool("hasJoined", false);
        animMask.SetBool("maskJoined", false);

        return playerNumber - 1;
    }

    private void ShowAbilitiesUI()
    {
        CancelPreviousAbilitiesTweens();
        abilityOneRect.DOAnchorPos(new Vector2(0, 34), 0.4f);
        dPadLeftRect.DOAnchorPos(new Vector2(-157, -201), 0.4f);
        abilityTwoRect.DOAnchorPos(new Vector2(0, -145), 0.4f);
        dPadRightRect.DOAnchorPos(new Vector2(157, -201), 0.4f);
    }

    private void RemoveAbilitiesUI()
    {
        CancelPreviousAbilitiesTweens();
        abilityOneRect.DOAnchorPos(new Vector2(0, -1930), 1.6f);
        dPadLeftRect.DOAnchorPos(new Vector2(-157, -2131), 1.6f);
        abilityTwoRect.DOAnchorPos(new Vector2(0, -2110), 1.6f);
        dPadRightRect.DOAnchorPos(new Vector2(157, -2131), 1.6f);
    }

    private void CancelPreviousAbilitiesTweens()
    {
        abilityOneRect.DOKill();
        dPadLeftRect.DOKill();
        abilityTwoRect.DOKill();
        dPadRightRect.DOKill();
    }
}