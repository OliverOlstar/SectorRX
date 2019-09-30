using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffects : MonoBehaviour
{
    public enum Statuses { Burn, Shock };

    void Start()
    {

    }
    
    void Update()
    {
        
    }

    public void Effect(Statuses pStatus, float pSeconds)
    {
        switch (Statuses)
        {
            case Statuses.Burn:
                StartCoroutine("BurnRoutine", pSeconds);
                break;

            case Statuses.Shock:

                break;
        }
    }

    IEnumerator BurnRoutine(float pTime)
    {
        yield return new waitForSeconds(pTime);
    }
}
