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
    
    [SerializeField] private float healthDisplayLength = 2f;
    [SerializeField] private GameObject enemyHealthBar;

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
        //_playerHUD = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUDManager>();
    }

    public bool TakeDamage(int pAmount, Vector3 pKnockback, bool pReact, GameObject pAttacker)
    {
        //_health -= pAmount;

        if (sliderControl != null)
            sliderControl.UpdateBars(0, pAmount);

        if (_health <= 0 && !isDead)
            Death();

        StopCoroutine("ShowHealthbar");
        StartCoroutine("ShowHealthbar");

        //Return If Dead or Not
        if (_health <= 0)
        {
            return true;
        }

        _decision.UpdateTarget(pAttacker.transform);

        if (pReact && _decision != null)
        {
            _decision.ForceStateSwitch(_stunnedState);
            _rb.AddForce(pKnockback, ForceMode.Impulse);
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
