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
    [SerializeField] private UIManager _Manager;

    // 0 - Joinable, 1 - Select Abilities, 2 - Locked In
    private int stateValue = 0;
    [HideInInspector] public DeviceHandler myDevice = null;

    [Header("Abilities")]
    [SerializeField] private Sprite[] abilityIcons;
    [SerializeField] private SpriteRenderer ability;
    private RectTransform _abilityTransform;
    [HideInInspector] public int abilityNumber = 0;

    [Header("Visuals")]
    [SerializeField] private GameObject joinText;
    [SerializeField] private GameObject readyText;

    [Space]
    [SerializeField] private Animator animShield;
    [SerializeField] private Animator animMask;
    private SpriteRenderer _animMaskRenderer;

    [Header("Model")]
    [SerializeField] private MenuLizzy _myLizzy;
    [SerializeField] private ColorPicker _ColorPicker;
    private int _CurrentColorIndex = 0;
    [HideInInspector] public ColorSet myColorSet;

    [Header("Sound")]
    [SerializeField] private AudioClip[] lockedIn = new AudioClip[4];
    [SerializeField] private AudioSource sfxSource;

    private void Start()
    {
        _abilityTransform = ability.GetComponent<RectTransform>();
        _animMaskRenderer = animMask.GetComponent<SpriteRenderer>();
        
        UpdateIcons();
        RemoveAbilitiesUI();

        ColorSet set = _ColorPicker.StartingColor();
        SetColors(set);
        _myLizzy.SetAbilities(0);
    }

    public void OnStart()
    {
        // Player Enters to Start Match
        _AddPlayer.SetPlayerOrder();
        _Manager.LoadLevel(1);
    }

    public void OnForward()
    {
        switch (stateValue)
        {
            case 0:
                // Player Connected
                stateValue = 1;
                ExitJoinable();
                EnterSelectAbilities();
                break;

            case 1:
                // Player Locked In
                stateValue = 2;
                ExitSelectAbilities();
                EnterReady();
                break;
        }
    }

    public bool OnBackward()
    {
        switch (stateValue)
        {
            case 1:
                // Player Disconnected
                stateValue = 0;
                ExitSelectAbilities();
                EnterJoinable();
                return true;

            case 2:
                // Player Left Locked In
                stateValue = 1;
                ExitReady();
                EnterSelectAbilities();
                break;
        }

        return false;
    }

    // Player Enters To Join
    public void PlayerJoined(DeviceHandler pDevice)
    {
        myDevice = pDevice;
        OnForward();
    }

    // Player Enters to Leave Match
    public int PlayerLeft()
    {
        myDevice = null;

        if (stateValue == 2)
        {
            ExitReady();
        }
        else if (stateValue == 1)
        {
            ExitSelectAbilities();
            _Manager.PlayerReadyUpdateUI();
        }

        EnterJoinable();
        stateValue = 0;

        return playerNumber - 1;
    }

    public int GetPlayerIndex()
    {
        return playerNumber - 1;
    }

    #region Enter & Exit States
    private void EnterJoinable()
    {
        joinText.SetActive(true);
        _myLizzy.ChangeWeights(MenuLizzy.menuLizzyStates.NotJoined);
    }

    private void ExitJoinable()
    {
        joinText.SetActive(false);
    }

    private void EnterSelectAbilities()
    {
        ShowAbilitiesUI();
        _myLizzy.ChangeWeights(MenuLizzy.menuLizzyStates.Joined);
    }

    private void ExitSelectAbilities()
    {
        StartCoroutine(RemoveAbilitiesUI());
    }

    private void EnterReady()
    {
        // Visuals
        _myLizzy.ChangeWeights(MenuLizzy.menuLizzyStates.LockedIn);
        animShield.SetBool("hasJoined", true);
        animMask.SetBool("maskJoined", true);
        readyText.SetActive(true);

        // Sound
        sfxSource.clip = lockedIn[Random.Range(0, 3)];
        sfxSource.volume = Random.Range(0.6f, 0.8f);
        sfxSource.PlayDelayed(0.25f);

        _Manager.PlayerReadyToggle(true);
    }
    private void ExitReady()
    {
        // Visuals
        animShield.SetBool("hasJoined", false);
        animMask.SetBool("maskJoined", false);
        readyText.SetActive(false);

        _Manager.PlayerReadyToggle(false);
    }
#endregion

    #region Colors
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
    #endregion

    #region Abilities
    public void OnLeft()
    {
        if (stateValue == 1)
        {
            ChangeIcons(-1);
            _myLizzy.SetAbilities(abilityNumber);
        }
    }

    public void OnRight()
    {
        if (stateValue == 1)
        {
            ChangeIcons(1);
            _myLizzy.SetAbilities(abilityNumber);
        }
    }

    public void ChangeIcons(int pDirection)
    {
        abilityNumber += pDirection;

        // Check if outside bounds
        if (abilityNumber < 0)
        {
            abilityNumber = abilityIcons.Length - 1;
        }
        else if (abilityNumber >= abilityIcons.Length)
        {
            abilityNumber = 0;
        }

        UpdateIcons();
    }

    private void UpdateIcons()
    {
        ability.sprite = abilityIcons[abilityNumber];
    }

    private void ShowAbilitiesUI()
    {
        StopAllCoroutines();
        _abilityTransform.DOKill();

        _abilityTransform.DOAnchorPos(new Vector2(0, -36), 0.4f);
        _abilityTransform.gameObject.SetActive(true);
    }

    IEnumerator RemoveAbilitiesUI()
    {
        _abilityTransform.DOKill();
        _abilityTransform.DOAnchorPos(new Vector2(0, -1930), 1.6f);

        yield return new WaitForSeconds(0.25f);
        _abilityTransform.gameObject.SetActive(false);
    }
    #endregion
}