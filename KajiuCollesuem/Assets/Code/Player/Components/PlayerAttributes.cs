using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

/*
Programmer: Robert Fowley
Additional Programmers: Oliver Loescher, Kavian Kermani
Description: Managing player attributes such as health, shield, and power.
*/

public class PlayerAttributes : MonoBehaviour, IAttributes
{
    private PlayerStateController _stateController;
    public SliderController sliderControl;

    [Header("Maxes")]
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _maxShield = 100;
    [SerializeField] private int _maxPower = 100;

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

    [Header("HUD")]
    //[SerializeField] private Slider _healthSlider;
    //[SerializeField] private Slider _shieldSlider;
    //[SerializeField] private Slider _powerSlider;

    private RectTransform healthRect;
    private RectTransform shieldRect;
    private RectTransform powerRect;

    //const int BAR_HEIGHT = 20;
    //public float barLengthMultiplier = 1.5f;

    public bool IsDead() { return false; }

    void Awake()
    {
        _stateController = GetComponent<PlayerStateController>();

        _health = _maxHealth;
        _shield = _maxShield;
        _power = 0;
        modifyPower(0);

        //Set the length of the bars to their respective maxes
        //if (_healthSlider)
        //{
        //    healthRect = _healthSlider.gameObject.GetComponent<RectTransform>();
        //    healthRect.sizeDelta = new Vector2(_maxHealth * barLengthMultiplier, BAR_HEIGHT);
        //    _healthSlider.maxValue = _maxHealth;
        //}

        //if (_shieldSlider)
        //{
        //    shieldRect = _shieldSlider.gameObject.GetComponent<RectTransform>();
        //    shieldRect.sizeDelta = new Vector2(_maxShield * barLengthMultiplier, BAR_HEIGHT);
        //    _shieldSlider.maxValue = _maxShield;
        //}

        //if (_powerSlider)
        //{
        //    powerRect = _powerSlider.gameObject.GetComponent<RectTransform>();
        //    powerRect.sizeDelta = new Vector2(_maxPower * barLengthMultiplier, BAR_HEIGHT);
        //    _powerSlider.maxValue = _maxPower;
        //}

        sliderControl.SetBars(0, _maxHealth);
        sliderControl.SetBars(1, _maxShield);
        sliderControl.SetBars(2, _maxPower);
    }

    public void Respawn()
    {
        setHealth(_maxHealth);
        setShield(_maxShield);
        setPower(0);
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
    public void modifyHealth(int x)
    {
        //Changing Value
        _health += x;
        _health = Mathf.Clamp(_health, 0, _maxHealth);

        //Changing Visuals
        if (sliderControl.RegSlider[0])
        {
            sliderControl.RegSlider[0].value = _health;
            sliderControl.UpdateBars(0, _health);
        }
    }

    public void modifyShield(int x)
    {
        _shield += x;
        _shield = Mathf.Clamp(_shield, 0, _maxShield);

        //Changing Visuals
        if (sliderControl.RegSlider[1])
        {
            sliderControl.RegSlider[1].value = _shield;
            sliderControl.UpdateBars(1, _shield);
        }
    }

    public void modifyPower(int x)
    {
        //Changing Value
        _power += x;
        _power = Mathf.Clamp(_power, 0, _maxPower);

        //Changing Visuals
        if (sliderControl.RegSlider[2])
        {
            sliderControl.RegSlider[2].value = _power;
            sliderControl.UpdateBars(2, _power);
        }
    }

    //MODIFY MAXES
    public void setMaxHealth(int pMaxHealth)
    {
        //Change Value
        _maxHealth = pMaxHealth;

        //Change respective bar length
        sliderControl.SetBars(0, pMaxHealth);
        modifyHealth(_maxHealth);
    }

    public void setMaxDefense(int pMaxShield)
    {
        //Change Value
        _maxShield = pMaxShield;

        //Change respective bar length
        sliderControl.SetBars(1, pMaxShield);
        modifyShield(_maxShield);
    }

    public void setMaxPower(int pMaxPowerGuage)
    {
        //Change Value
        _maxPower = pMaxPowerGuage;

        //Change respective bar length
        sliderControl.SetBars(2, pMaxPowerGuage);
        sliderControl.RegSlider[2].maxValue = _maxPower;
    }
    #endregion

    #region General Functions
    //GENERAL FUNCTIONS ///////////////////////////////////////////////////////////////////////////////////////////
    public bool TakeDamage(int pAmount, Vector3 pKnockback, bool pReact)
    {
        Debug.Log("PlayerAttributes: TakeDamage");

        // Return if already dead
        if (_health <= 0)
            return true;

        Debug.Log("Damaging Player " + pAmount);

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
            modifyHealth(-pAmount);
            Debug.Log(pAmount);
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
        _stateController._Rb.AddForce(pKnockback, ForceMode.Impulse);

        //if (pReact)
        //    _anim.Stunned(Random.value < 0.5f);

        // Return If Dead or Not
        if (_health <= 0)
        {
            //Camera Shake
            CameraShaker.Instance.ShakeOnce(20, 4, 0.4f, 0.3f);
            connectedPlayers.playersConnected--;

            return true;
        }

        // Camera Shake
        //CameraShaker.Instance.ShakeOnce(1, 2, 0.2f, 0.1f);

        return false;
    }

    public void RecivePower(int pPower)
    {
        modifyPower(pPower);
        //Debug.Log("Power Recieved: " + pPower + ", " + _power);

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
