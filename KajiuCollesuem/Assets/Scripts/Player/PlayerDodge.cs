using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodge : MonoBehaviour
{
    //Oliver

    [HideInInspector] public bool doneDodge = false;

    public float dashCooldown = 1.0f;
    private float _dashDelay = 0.0f;

    [Space]
    [SerializeField] private float shortDodgeDistance = 6.0f;
    [SerializeField] private float shortDodgeDuration = 0.2f;

    [Space]
    [SerializeField] private float longDodgeDistance = 12.0f;
    [SerializeField] private float longDodgeDuration = 0.3f;

    private Rigidbody _Rb;
    private Transform _Camera;

    void Start()
    {
        _Rb = GetComponent<Rigidbody>();
        _Camera = Camera.main.transform;
    }

    public bool Dodge(bool pShortDodge, Vector3 pDirection)
    {
        //Cooldown
        if (Time.time > _dashDelay)
        {
            if (pShortDodge)
            {
                //Short Dodge
                StartCoroutine(DodgeRoutine(shortDodgeDistance, shortDodgeDuration, pDirection));
            }
            else
            {
                //Long Dodge
                StartCoroutine(DodgeRoutine(longDodgeDistance, longDodgeDuration, pDirection));
            }

            return true;
        }
        else
        {
            Debug.Log("Dodge on Cooldown");
            doneDodge = true;
            return false;
        }
    }

    IEnumerator DodgeRoutine(float pDistance, float pDuration, Vector3 pDirection)
    {
        //Setting Delay
        _dashDelay = Time.time + dashCooldown + pDuration;
        float dodgeEndTime = Time.time + pDuration;

        //Run Dodge Force
        while (Time.time <= dodgeEndTime)
        {
            _Rb.velocity = pDirection * (pDistance / pDuration);
            yield return null;
        }

        //Stop player
        //_Rb.velocity = _Rb.velocity.normalized;
        doneDodge = true;
    }
}
