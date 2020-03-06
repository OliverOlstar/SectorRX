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

    public float weight = 1;

    [Header("Maxes")]
    private int _maxHealth = 100;
    private int _maxShield = 100;
    private int _maxPower = 100;

    private int _health;
    private int _shield;
    private int _power;

    [Header("Regen & Loss over time")]
    [SerializeField] private float _shieldRegenStartDelaySeconds = 6f;
    [SerializeField] private float _shieldRegenDelaySeconds = 1f;
    [SerializeField] private int _shieldRegenAmount = 4;

    [Space]
    [SerializeField] private float _powerLossStartDelaySeconds = 8f;
    [SerializeField] private float _powerLossDelaySeconds = 0.3f;
    [SerializeField] private int _powerLossAmount = 1;

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
        _power = 0;
        modifyPower(0);

        sliderControl.SetBars(0, _maxHealth);
        sliderControl.SetBars(1, _maxShield);
        sliderControl.SetBars(2, _maxPower);
    }

    #region Get & Sets
    //GET
    public int getHealth() { return _health; }
    public int getShield() { return _shield; }
    public int getPower() { return _power; }

    //SET
    public void setHealth(int pHealth) { modifyHealth(pHealth - _health); sliderControl.UpdateBars(0, pHealth); }
    public void setShield(int pShield) { modifyShield(pShield - _shield); sliderControl.UpdateBars(1, pShield); }
    public void setPower(int pPower) { modifyPower(pPower - _power); sliderControl.UpdateBars(2, pPower); }
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
            MatchManager.instance.ManagerEnd();
            _stateController._playerCamera.targetDead = true;
            return true;
        }
        return false;
    }

    public void modifyShield(int x)
    {
        _shield += x;
        _shield = Mathf.Clamp(_shield, 0, _maxShield);

        //Changing Visuals
        //if (sliderControl.RegSlider[1])
        //{
            sliderControl.UpdateBars(1, _shield);
        //}
    }

    public void modifyPower(int x)
    {
        //Changing Value
        _power += x;
        _power = Mathf.Clamp(_power, 0, _maxPower);

        //Changing Visuals
        //if (sliderControl.RegSlider[2])
        //{
            sliderControl.UpdateBars(2, _power);
        //}
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

    public void setMaxPower(int pMaxPowerGuage)
    {
        //Change Value
        _maxPower = pMaxPowerGuage;

        //Change respective bar length
        sliderControl.SetBars(2, pMaxPowerGuage);
    }
    #endregion

    #region General Functions
    //GENERAL FUNCTIONS ///////////////////////////////////////////////////////////////////////////////////////////
    public bool TakeDamage(int pAmount, Vector3 pKnockback, GameObject pAttacker, bool pIgnoreWeight = false)
    {
        //Debug.Log("PlayerAttributes: TakeDamage");

        // Return if already dead
        if (_health <= 0)
            return true;

        //Debug.Log("Damaging Player " + pAmount);
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

        // Restarting Power Loss over time
        if (_power > 0)
        {
            StopCoroutine("powerLoss");
            StopCoroutine("powerLossStartDelay");
            StartCoroutine("powerLossStartDelay");
        }

        // Add Knockback
        _stateController._Rb.AddForce(pKnockback / (pIgnoreWeight ? 1 : weight), ForceMode.Impulse);

        // Add Shake
        _stateController._CameraShake.PlayShake(pAmount / 4, 6.0f, 0.5f, 0.8f);

        // Sound
        if (died)
            _stateController._Sound.PlayerDeathSound(0.5f);
        else if (pAttacker != null)
            _stateController._Sound.HitTarSound(0.0f);
        else
            _stateController._Sound.HitByAttackSound(0.0f);

        if (died == false)
            _stateController._modelController.AddStunned(1, (Random.value - 0.5f) * 2, easeOutDelay, easeOut);
        else
            SpawnStatUps();

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

    public void RecivePower(int pPower)
    {
        modifyPower(pPower);

        // Restarting Power Loss over time
        if (_power > 0)
        {
            StopCoroutine("powerLoss");
            StopCoroutine("powerLossStartDelay");
            StartCoroutine("powerLossStartDelay");
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

    //Power
    private IEnumerator powerLossStartDelay()
    {
        yield return new WaitForSeconds(_powerLossStartDelaySeconds);
        StartCoroutine("powerLoss");
    }

    private IEnumerator powerLoss()
    {
        while (_power > 0)
        {
            //Debug.Log("Power lost: " + _power);
            modifyPower(-_powerLossAmount);
            yield return new WaitForSeconds(_powerLossDelaySeconds);
        }
    }
    #endregion
}
