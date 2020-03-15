using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBallHitbox : MonoBehaviour
{
    private float _AttackMult = 1;
    [SerializeField] private int _damage;
    [SerializeField] private float _knockForce;
    [SerializeField] private float _explosionRadius;

    private GameObject _Attacker;
    private IAttributes _playerIAttributes;

    private List<IAttributes> hitAttributes = new List<IAttributes>();

    public void Init(IAttributes pPlayerAttributes, GameObject pAttacker, float pAttackMult, float pLifeTime)
    {
        _playerIAttributes = pPlayerAttributes;
        _Attacker = pAttacker;
        _AttackMult = pAttackMult;

        transform.GetChild(0).localScale = Vector3.one * _explosionRadius;

        StartCoroutine(explodeDelay(pLifeTime));
    }

    IEnumerator explodeDelay(float pDelay)
    {
        yield return new WaitForSeconds(pDelay);
        Explode();
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Collectable") || other.CompareTag("Fireball"))
            return;

        //Check if collided with an Attributes Script
        IAttributes otherAttributes = other.GetComponent<IAttributes>();
        if (otherAttributes == null)
            otherAttributes = other.GetComponentInParent<IAttributes>();

        //Damage other
        if (otherAttributes != _playerIAttributes)
        {
            if (otherAttributes != null && otherAttributes.IsDead() == false)
            {
                otherAttributes.TakeDamage(Mathf.FloorToInt(_damage * _AttackMult * 0.5f), transform.forward * _knockForce, _Attacker, "Ability");
            }

            // Explode if the thing I hit wasn't myself
            Explode();
        }
    }

    private void Explode()
    {
        // Visual
        transform.GetChild(0).gameObject.SetActive(true);
        // Sound
        transform.GetChild(1).gameObject.SetActive(true);
        StartCoroutine("destroyDelay");

        foreach (Collider other in Physics.OverlapSphere(transform.position, _explosionRadius))
        {
            IAttributes otherAttributes = other.GetComponent<IAttributes>();
            if (otherAttributes == null)
                otherAttributes = other.GetComponentInParent<IAttributes>();

            // Don't hit the same thing twice
            if (hitAttributes.Contains(otherAttributes) || other.CompareTag("Fireball"))
                continue;

            //Damage other
            if (otherAttributes != null && otherAttributes.IsDead() == false)
            {
                otherAttributes.TakeDamage(Mathf.FloorToInt(_damage * _AttackMult), (other.transform.position - transform.position).normalized * _knockForce, _Attacker, "Ability");
                hitAttributes.Add(otherAttributes);
            }
        }
    }

    IEnumerator destroyDelay()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.45f);
        Destroy(gameObject);
    }
}
