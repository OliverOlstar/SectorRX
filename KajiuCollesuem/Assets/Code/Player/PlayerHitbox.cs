using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerHitbox : MonoBehaviour
{
    [HideInInspector] public float attackMult = 1;
    
    private int _damage;
    private Vector3 _knockback;

    private int _powerRecivedOnHit = 25;

    [SerializeField] private GameObject _Attacker;

    private PlayerAttributes _playerAttributes;
    private IAttributes _playerIAttributes;
    private PlayerLockOnScript _lockOnScript;

    private List<IAttributes> hitAttributes = new List<IAttributes>();

    private void OnEnable()
    {
        // Clear list
        hitAttributes = new List<IAttributes>();
    }

    private void Start()
    {
        _playerAttributes = GetComponentInParent<PlayerAttributes>();
        _playerIAttributes = _playerAttributes.GetComponent<IAttributes>();
        _lockOnScript = _playerAttributes.GetComponent<PlayerLockOnScript>();
    }

    private void OnTriggerEnter (Collider other)
    {
        //Check if collided with an Attributes Script
        IAttributes otherAttributes = other.GetComponent<IAttributes>();
        if (otherAttributes == null)
            otherAttributes = other.GetComponentInParent<IAttributes>();

        // Don't hit the same thing twice
        foreach (IAttributes previousAttributes in hitAttributes)
        {
            if (previousAttributes == otherAttributes)
                return;
        }

        // Add to list so we can't hit it twice
        hitAttributes.Add(otherAttributes);

        if (otherAttributes != null && otherAttributes.IsDead() == false && otherAttributes != _playerIAttributes)
        {
            //Damage other
            /*if (*/otherAttributes.TakeDamage(Mathf.FloorToInt(_damage * attackMult), _knockback, true, _Attacker);//)
                //If other died and is lockOn target return camera to default
                //_lockOnScript.TargetDead(other.transform);

            //Recieve Power
            _playerAttributes.RecivePower(_powerRecivedOnHit);

            // TODO Camera Shake
        }
    }

    public void SetDamage(int pDamage, Vector3 pKnockback)
    {
        _damage = pDamage;
        _knockback = pKnockback;
    }

    //public void SetDamageMultiplier(float pMult) => damageMultiplier = pMult;
}
