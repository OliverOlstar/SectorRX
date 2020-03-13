using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryLizzyPositioner : MonoBehaviour
{
    [SerializeField] private float _Speed = 1.0f;
    [SerializeField] private AudioSource displayAudio;

    public void SetPosition(float pDelay, float pY, bool pPlaySound)
    {
        StartCoroutine(MoveRoutine(pDelay, pY, pPlaySound));
    }

    private IEnumerator MoveRoutine(float pDelay, float pY, bool pPlaySound)
    {
        yield return new WaitForSeconds(pDelay);

        if (pPlaySound)
        {
            displayAudio.Play();
        }

        while (transform.localPosition.y < pY)
        {
            transform.localPosition += Vector3.up * _Speed * Time.deltaTime;
            //transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, pY, transform.localPosition.z), _Speed * Time.deltaTime);

            if (displayAudio.pitch < 1)
            {
                displayAudio.pitch += 0.275f * Time.deltaTime / connectedPlayers.playersConnected;
            }

            yield return null;
        }

        if (pPlaySound)
        {
            displayAudio.Stop();
        }
    }
}
