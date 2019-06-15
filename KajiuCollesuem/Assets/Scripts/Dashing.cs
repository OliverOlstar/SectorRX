using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    [SerializeField] private float dashforce;
    [SerializeField] private float dashDuration;

    public float dashCooldown = 1.0f;
    public float newDashTime = 0.0f;

    public float dashBonus = 1.0f;

    
    // Update is called once per frame
    void Update()
    {
        if (Time.time > newDashTime)
        {
            if (Input.GetKey(KeyCode.C))
            {
                if (dashBonus < 2.0f)
                {
                    dashBonus += Time.deltaTime;
                }
            }

            if (Input.GetKeyUp(KeyCode.C))
            {
                StartCoroutine(Cast());
            }
        }
    }

    IEnumerator Cast()
    {
        GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * dashforce * dashBonus, ForceMode.VelocityChange);

        yield return new WaitForSeconds(dashDuration);

        GetComponent<Rigidbody>().velocity = Vector3.zero;

        newDashTime = Time.time + dashCooldown;

        dashBonus = 1.0f;
    }
}
