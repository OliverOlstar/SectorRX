using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
Programmer: Robert Fowley
Additional Programmers: Oliver Loescher, Kavian Kermani
Description: Managing player attributes such as health, shield, and power.
*/

public class PlayerAttributes : MonoBehaviour, IAttributes
{
    private PlayerStateController _stateController;
    public SliderController sliderControl;
    public CanvasGroup playerHUD;

    public float weight = 1;

    [Header("Maxes")]
    private int _maxHealth = 100;
    private int _maxShield = 100;
    [SerializeField] private int _maxAbility = 75;

    private int _health;
    private int _shield;
    private int _ability;

    [Header("Regen & Loss over time")]
    [SerializeField] private float _shieldRegenStartDelaySeconds = 6f;
    [SerializeField] private float _shieldRegenDelaySeconds = 1f;
    [SerializeField] private int _shieldRegenAmount = 4;

    [Header("Stunned Anim")]
    [SerializeField] [Range(0, 0.5f)] private float easeOut = 0.15f;
    [SerializeField] [Range(0, 0.5f)] private float easeOutDelay = 0.15f;

    [Header("Spawn Stats")]
    [SerializeField] private GameObject[] _itemPrefabs;
    [SerializeField] private int _cellSpawnCount = 5;

    public bool IsDead() { return _health == 0; }

    void Awake()
    {
        _stateController = GetComponent<PlayerStateController>();

        _health = _maxHealth;
        _shield = _maxShield;
        _ability = 0;
        modifyAbility(0);

        sliderControl.SetBars(0, _maxHealth);
        sliderControl.SetBars(1, _maxShield);
        sliderControl.SetBars(2, _maxAbility);
    }

    #region Get & Sets
    //GET
    public int getHealth() { return _health; }
    public int getShield() { return _shield; }
    public int getAbility() { return _ability; }

    //SET
    public void setHealth(int pHealth) { modifyHealth(pHealth - _health); sliderControl.UpdateBars(0, pHealth); }
    public void setShield(int pShield) { modifyShield(pShield - _shield); sliderControl.UpdateBars(1, pShield); }
    public void setAbility(int pAbility) { modifyAbility(pAbility - _ability); sliderControl.UpdateBars(2, pAbility); }
    #endregion

    #region Modify Vars
    //MODIFY VARS ///////////////////////////////////////////////////////////////////////////////////////////
    public bool modifyHealth(int x)
    {
        //Changing Value
        _health += x;
        _health = Mathf.Clamp(_health, 0, _maxHealth);

        //Changing Visuals
        sliderControl.UpdateBars(0, _health);

        // Return If Dead or Not
        if (_health <= 0)
        {
            return true;
        }
        return false;
    }

    public void modifyShield(int x)
    {
        _shield += x;
        _shield = Mathf.Clamp(_shield, 0, _maxShield);

        sliderControl.UpdateBars(1, _shield);
    }

    public void modifyAbility(int x)
    {
        //Changing Value
        _ability += x;
        _ability = Mathf.Clamp(_ability, 0, _maxAbility);

        sliderControl.UpdateBars(2, _ability);
    }

    //MODIFY MAXES
    public void setMaxHealth(int pMaxHealth)
    {
        int modifyAmount = pMaxHealth - _maxHealth;

        //Change Value
        _maxHealth = pMaxHealth;

        //Change respective bar length
        sliderControl.SetBars(0, pMaxHealth);
        modifyHealth(modifyAmount);
    }

    public void setMaxDefense(int pMaxShield)
    {
        int modifyAmount = pMaxShield - _maxShield;

        //Change Value
        _maxShield = pMaxShield;

        //Change respective bar length
        sliderControl.SetBars(1, pMaxShield);
        modifyShield(modifyAmount);
    }

    public void setMaxAbility(int pMaxAbilityGuage)
    {
        //Change Value
        _maxAbility = pMaxAbilityGuage;

        //Change respective bar length
        sliderControl.SetBars(2, pMaxAbilityGuage);
    }
    #endregion

    #region General Functions
    //GENERAL FUNCTIONS ///////////////////////////////////////////////////////////////////////////////////////////
    public bool TakeDamage(int pAmount, Vector3 pKnockback, GameObject pAttacker, string pTag, bool pIgnoreWeight = false)
    {
        // Return if already dead
        if (_health <= 0)
            return true;

        bool died = false;

        if (_shield >= pAmount)
        {
            // Changing only Shield
            modifyShield(-pAmount);
        }
        else
        {
            // Changing Shield and getting the remainder
            pAmount -= _shield;
            modifyShield(-_shield);

            // Changing Health by remainder
            died = modifyHealth(-pAmount);
        }

        // Restarting Shield Regening
        if (_shield < _maxShield)
        {
            StopCoroutine("shieldRegen");
            StopCoroutine("shieldRegenStartDelay");
            StartCoroutine("shieldRegenStartDelay");
        }

        // Add Knockback
        _stateController._Rb.AddForce(pKnockback / (pIgnoreWeight ? 1 : weight), ForceMode.Impulse);

        if (died)
        {
            // Killed
            Death(pTag);
        }
        else if (pAttacker == null)
        {
            // Hit by tar
            _stateController._Sound.HitTarSound();
            _stateController._modelController.AddTarJump(1, 0.2f, 1.1f, 0.65f);
            _stateController._modelController.AddCrouching(0.4f, 0.2f, 0.2f);
            _stateController._CameraShake.PlayShake(6.0f, 8.0f, 0.6f, 0.6f);
        }
        else
        {
            // Hit by attack
            _stateController._Sound.HitByAttackSound();
            _stateController._modelController.AddStunned(1, (Random.value - 0.5f) * 2, easeOutDelay, easeOut);
            _stateController._CameraShake.PlayShake(pAmount / 4, 6.0f, 0.5f, 0.8f);
        }

        // If usingAbility, Cancel it
        _stateController.usingAbility = false;

        // Return If Dead or Not
        return died;
    }

    private void SpawnStatUps()
    {
        // Can't Collect them myself
        Destroy(GetComponent<PlayerCollectibles>());

        StartCoroutine(SpawnStatUpsDelay());
    }

    private IEnumerator SpawnStatUpsDelay()
    {
        yield return new WaitForSeconds(0.5f);

        // Coins disperse
        for (int i = 0; i < _cellSpawnCount; ++i)
        {
            GameObject tmp = Instantiate(_itemPrefabs[Random.Range(0, _itemPrefabs.Length)]);
            tmp.transform.position = transform.position + Vector3.up * 0.1f;
        }
    }

    private void Death(string pAttackerTag)
    {
        StartCoroutine("HUDAlphaDown");

        bool matchNotOver = MatchManager.instance.ManagerEnd();
        _stateController._Sound.PlayerDeathSound();
        _stateController._lockOnComponent.SwitchToDeadCamera();
        if (matchNotOver) SpawnStatUps();

        // Announcer
        switch (pAttackerTag)
        {
            case "Tar":
                Announcer._Instance.TarKO();
                break;

            case "Player":
                Announcer._Instance.NormalKO();
                break;

            case "Ability":
                Announcer._Instance.AbilityKO();
                break;

            case "Wolf":
                Announcer._Instance.WolfKO();
                break;

            case "Drill":
                Announcer._Instance.DrillKO();
                break;
        }
    }
    #endregion

    #region Coroutines
    //COROUTINES ///////////////////////////////////////////////////////////////////////////////////////////
    //Shield
    private IEnumerator shieldRegenStartDelay()
    {
        yield return new WaitForSeconds(_shieldRegenStartDelaySeconds);
        StartCoroutine("shieldRegen");
    }

    private IEnumerator shieldRegen()
    {
        while (_shield < _maxShield)
        {
            modifyShield(_shieldRegenAmount);
            yield return new WaitForSeconds(_shieldRegenDelaySeconds);
        }
    }

    private IEnumerator HUDAlphaDown()
    {
        while(playerHUD.alpha > 0)
        {
            playerHUD.alpha -= 0.3f * Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
    }
    #endregion
}