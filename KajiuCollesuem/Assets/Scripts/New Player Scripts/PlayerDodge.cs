using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodge : MonoBehaviour
{
    public float dashCooldown = 1.0f;
    private float _dashDelay = 0.0f;

    [SerializeField] private float dashDuration;
    [SerializeField] private float dashDuration;

    [SerializeField] private float longDodgeDistance = 1.0f;
    [SerializeField] private float shortDodgeDistance = 0.6f;

    public void Dodge(bool pShortDodge)
    {
        //Cooldown
        if (Time.time > _dashDelay)
        {
            StartCoroutine(DodgeRoutine());
        }
    }

    IEnumerator DodgeRoutine()
    {
        GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward, ForceMode.VelocityChange);
        yield return new WaitForSeconds(dashDuration);
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        //Setting Delay
        _dashDelay = Time.time + dashCooldown;
    }
}
