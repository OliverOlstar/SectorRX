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

    public void Dodge(bool pShortDodge)
    {
        //Cooldown
        if (Time.time > _dashDelay)
        {
            if (pShortDodge)
            {
                //Short Dodge
                StartCoroutine(DodgeRoutine(shortDodgeDistance, shortDodgeDuration));
            }
            else
            {
                //Long Dodge
                StartCoroutine(DodgeRoutine(longDodgeDistance, longDodgeDuration));
            }
        }
        else
        {
            Debug.Log("Dodge on Cooldown");
            doneDodge = true;
        }
    }

    IEnumerator DodgeRoutine(float pDistance, float pDuration)
    {
        //Setting Delay
        _dashDelay = Time.time + dashCooldown + pDuration;
        float dodgeEndTime = Time.time + pDuration;

        //Run Dodge Force
        while (Time.time <= dodgeEndTime)
        {
            // TODO Make dodge direction based on player movement direction rather than camera forward
            Vector3 forwardDirection = _Camera.forward;
            forwardDirection.y = 0;
            forwardDirection.Normalize();

            _Rb.velocity = forwardDirection * (pDistance / pDuration);
            yield return null;
        }

        //Stop player
        //_Rb.velocity = _Rb.velocity.normalized;
        doneDodge = true;
    }
}
