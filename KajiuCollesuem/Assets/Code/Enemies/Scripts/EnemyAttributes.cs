using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
Programmer: 
Additional Programmers: Kavian Kermani, Oliver Loescher
Description: Managing enemy attributes such as damage done and damage taken.
*/

public class EnemyAttributes : MonoBehaviour, IAttributes
{
    [SerializeField] private int startHealth = 100;
    private int _health;

    [SerializeField] private float _weight = 2;
    
    [SerializeField] private float healthDisplayLength = 2f;
    [SerializeField] private float _weight = 0;
    [SerializeField] private GameObject enemyHealthBar;
    private TargetManagement _tm;

    private Rigidbody _rb;

    private bool isDead;

    private Decision _decision;
    private HUDManager _playerHUD;
    public SliderController sliderControl;
    private IState _deadState;
    private IState _stunnedState;

    public bool IsDead() { return isDead; }

    void Start()
    {
        if (sliderControl != null)
            sliderControl.SetBar(0, startHealth);

        _health = startHealth;
        
        if (enemyHealthBar)
        {
            enemyHealthBar.SetActive(false);
        }

        _decision = GetComponent<Decision>();
        _deadState = GetComponent<Dead>();
        _stunnedState = GetComponent<Stunned>();
        _rb = GetComponent<Rigidbody>();
        _tm = GetComponent<TargetManagement>();
        //_playerHUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
    }

    public bool TakeDamage(int pAmount, Vector3 pKnockback, GameObject pAttacker)
    {
        _health -= pAmount;

        pKnockback.y = pKnockback.y * 2;

        if (sliderControl != null)
            sliderControl.UpdateBars(0, pAmount);

        Debug.Log("Disabled Hellhound Healthbar");
        //StopCoroutine("ShowHealthbar");
        //StartCoroutine("ShowHealthbar");

        if (_health <= 0 && !isDead)
        {
            Death();
            _rb.AddForce(pKnockback / _weight, ForceMode.Impulse);
            return true;
        }
        _tm.playerAttributes = pAttacker.GetComponent<PlayerAttributes>();

        _decision.UpdateTarget(pAttacker.transform);

        if (_decision != null)
        {
            _decision.ForceStateSwitch(_stunnedState);
            _rb.AddForce(pKnockback / _weight, ForceMode.Impulse);
        }
        
        return false;
    }

    IEnumerator ShowHealthbar()
    {
        enemyHealthBar.SetActive(true);
        yield return new WaitForSeconds(healthDisplayLength);
        enemyHealthBar.SetActive(false);
    }

    void Death()
    {
        isDead = true;
        if (_decision != null)
            _decision.ForceStateSwitch(_deadState);
    }
}
