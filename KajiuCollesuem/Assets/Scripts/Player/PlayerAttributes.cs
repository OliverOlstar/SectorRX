using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour, IAttributes
{
    [Header("Maxes")]
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _maxShield = 100;
    [SerializeField] private int _maxPower = 10;

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
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _shieldSlider;
    [SerializeField] private Slider _powerSlider;

    private RectTransform healthRect;
    private RectTransform shieldRect;
    private RectTransform powerRect;

    const int BAR_HEIGHT = 20;
    public float barLengthMultiplier = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        _health = _maxHealth;
        _shield = _maxShield;
        _power = 0;
        modifyPower(0);

        //Set the length of the bars to their respective maxes
        if (_healthSlider)
        {
            healthRect = _healthSlider.gameObject.GetComponent<RectTransform>();
            healthRect.sizeDelta = new Vector2(_maxHealth * barLengthMultiplier, BAR_HEIGHT);
        }

        if (_shieldSlider)
        {
            shieldRect = _shieldSlider.gameObject.GetComponent<RectTransform>();
            shieldRect.sizeDelta = new Vector2(_maxShield * barLengthMultiplier, BAR_HEIGHT);
        }

        if (_powerSlider)
        {
            powerRect = _powerSlider.gameObject.GetComponent<RectTransform>();
            powerRect.sizeDelta = new Vector2(_maxPower * barLengthMultiplier, BAR_HEIGHT);
        }
    }

    //GET
    public int getHealth() { return _health; }
    public int getShield() { return _shield; }
    public int getPower() { return _power; }

    //SET
    public void setHealth(int pHealth) { _health = pHealth; }
    public void setShield(int pShield) { _shield = pShield; }
    public void setPower(int pPower) { _power = pPower; }

    //MODIFY VARS ///////////////////////////////////////////////////////////////////////////////////////////
    public void modifyHealth(int x)
    {
        //Changing Value
        _health += x;
        _health = Mathf.Clamp(_health, 0, _maxHealth);

        //Changing Visuals
        if (_healthSlider)
            _healthSlider.value = _health;
    }

    public void modifyShield(int x)
    {
        _shield += x;
        _shield = Mathf.Clamp(_shield, 0, _maxShield);

        //Changing Visuals
        if (_shieldSlider)
            _shieldSlider.value = _shield;
    }

    public void modifyPower(int x)
    {
        //Changing Value
        _power += x;
        _power = Mathf.Clamp(_power, 0, _maxPower);

        //Changing Visuals
        if (_powerSlider)
            _powerSlider.value = _power;
    }

    //MODIFY MAXES
    public void modifyMaxHealth(int pMaxHealth)
    {
        //Change Value
        _maxHealth += pMaxHealth;

        //Change respective bar length
        healthRect.sizeDelta = new Vector2(_maxHealth * barLengthMultiplier, BAR_HEIGHT);
    }

    public void modifyMaxDefense(int pMaxShield)
    {
        //Change Value
        _maxShield += pMaxShield;

        //Change respective bar length
        shieldRect.sizeDelta = new Vector2(_maxShield * barLengthMultiplier, BAR_HEIGHT);
    }

    public void modifyMaxPower(int pMaxPowerGuage)
    {
        //Change Value
        _maxPower += pMaxPowerGuage;

        //Change respective bar length
        powerRect.sizeDelta = new Vector2(_maxPower * barLengthMultiplier, BAR_HEIGHT);
    }

    //GENERAL FUNCTIONS ///////////////////////////////////////////////////////////////////////////////////////////
    public void TakeDamage(int pAmount)
    {
        //Debug.Log("Damaging Player " + pAmount);

        if (_shield >= pAmount)
        {
            //Changing only Shield
            modifyShield(-pAmount);
        }
        else
        {
            //Changing Shield and getting the remainder
            pAmount -= _shield;
            modifyShield(-_shield);

            //Changing Health by remainder
            modifyHealth(-pAmount);
        }

        //Restarting Shield Regening
        if (_shield < _maxShield)
        {
            StopCoroutine("shieldRegen");
            StopCoroutine("shieldRegenStartDelay");
            StartCoroutine("shieldRegenStartDelay");
        }

        //Restarting Power Loss over time
        //if (_power > 0)
        //{
        //    StopCoroutine("powerLoss");
        //    StopCoroutine("powerLossStartDelay");
        //    StartCoroutine("powerLossStartDelay");
        //}
    }

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
            modifyPower(-_powerLossAmount);
            Debug.Log("Power lost: " + _power);
            yield return new WaitForSeconds(_powerLossDelaySeconds);
        }
    }
}
