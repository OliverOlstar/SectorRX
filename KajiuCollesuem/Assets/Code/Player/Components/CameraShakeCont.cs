using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class CameraShakeCont : MonoBehaviour
{
    [SerializeField] private CameraShaker _Shaker;

    public void PlayShake(float pMagnitude, float pRoughness, float pFadeInTime, float pFadeOutTime, float pDelay = 0.0f)
    {
        if (pDelay > 0)
        {
            StartCoroutine(ShakeRoutine(pMagnitude, pRoughness, pFadeInTime, pFadeOutTime, pDelay));
        }
        else
        {
            _Shaker.ShakeOnce(pMagnitude, pRoughness, pFadeInTime, pFadeOutTime);
        }
    }

    private IEnumerator ShakeRoutine(float pMagnitude, float pRoughness, float pFadeInTime, float pFadeOutTime, float pDelay = 0.0f)
    {
        yield return new WaitForSeconds(pDelay);
        _Shaker.ShakeOnce(pMagnitude, pRoughness, pFadeInTime, pFadeOutTime);
    }
}
