using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffects : MonoBehaviour
{
    public enum Statuses { Burn, Shock };

    private IAttributes attributes;

    void Start()
    {
        attributes = GetComponent<IAttributes>();
    }

    public void Effect(Statuses pStatus, float pSeconds)
    {
        switch (pStatus)
        {
            case Statuses.Burn:
                StartCoroutine("BurnRoutine", Mathf.RoundToInt(pSeconds + Time.time));
                break;

            case Statuses.Shock:
                
                break;
        }
    }

    IEnumerator BurnRoutine(int pTime)
    {
        //Burn the player every second
        while (Time.time < pTime)
        {
            attributes.TakeDamage(3, false);
            yield return new WaitForSeconds(1);
        }
    }
}
