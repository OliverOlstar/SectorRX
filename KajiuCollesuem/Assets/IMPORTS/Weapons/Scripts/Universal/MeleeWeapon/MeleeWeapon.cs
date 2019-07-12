using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [Header("Weapon Attributes")]
    public float _AttackPower = 10.0f;

    [Header("Weapon Parts")]
    public Animator _anim;

    

    // Start is called before the first frame update
    void Start()
    {
        //_anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }
    
    void Attack()
    {
        //Debug.Log("asdasdad");
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("Swing 1");
            _anim.SetTrigger("Swing1");
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            _anim.SetTrigger("Swing2");
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            _anim.SetTrigger("Swing3");
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            _anim.SetTrigger("Swing4");
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            _anim.SetTrigger("Swing5");
        }

    }




}
