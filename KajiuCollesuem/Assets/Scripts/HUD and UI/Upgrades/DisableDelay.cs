using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableDelay : MonoBehaviour
{
    [SerializeField] private float delay = 1;

    void Awake()
    {
        StartCoroutine("TurnOffRoutine");
    }

    IEnumerator TurnOffRoutine()
    {
        yield return new WaitForSecondsRealtime(delay);
        gameObject.SetActive(false);
    }
}
