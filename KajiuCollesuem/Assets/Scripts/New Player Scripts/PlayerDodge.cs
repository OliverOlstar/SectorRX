using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodge : MonoBehaviour
{
    [HideInInspector] public bool doneDodge = false;

    public float dashCooldown = 1.0f;
    private float _dashDelay = 0.0f;

    [Space]
    [SerializeField] private float shortDodgeDistance = 6.0f;
    [SerializeField] private float shortDodgeDuration = 0.2f;

    [Space]
    [SerializeField] private float longDodgeDistance = 12.0f;
    [SerializeField] private float longDodgeDuration = 0.3f;

    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Dodge(bool pShortDodge)
    {
        //Cooldown
        if (Time.time > _dashDelay)
        {
            if (pShortDodge)
            {
                //Short Dodge
                Debug.Log("Short");
                StartCoroutine(DodgeRoutine(shortDodgeDistance, shortDodgeDuration));
            }
            else
            {
                //Long Dodge
                Debug.Log("Long");
                StartCoroutine(DodgeRoutine(longDodgeDistance, longDodgeDuration));
            }
        }
        else
        {
            Debug.Log("On Cooldown");
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
            //_rb.AddForce(Camera.main.transform.forward * (pDistance / pDuration), ForceMode.VelocityChange);
            _rb.velocity = Camera.main.transform.forward * (pDistance / pDuration);
            yield return null;
        }

        //Stop player
        _rb.velocity = Vector3.zero;
        doneDodge = true;
    }
}
