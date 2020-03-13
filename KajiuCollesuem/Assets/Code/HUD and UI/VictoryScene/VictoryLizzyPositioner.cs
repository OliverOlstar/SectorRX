using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryLizzyPositioner : MonoBehaviour
{
    [SerializeField] private float _Speed = 1.0f;

    public void SetPosition(float pDelay, float pY)
    {
        StartCoroutine(MoveRoutine(pDelay, pY));
    }

    private IEnumerator MoveRoutine(float pDelay, float pY)
    {
        yield return new WaitForSeconds(pDelay);

        while (transform.localPosition.y < pY)
        {
            transform.localPosition += Vector3.up * _Speed * Time.deltaTime;
            //transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, pY, transform.localPosition.z), _Speed * Time.deltaTime);
            yield return null;
        }
    }
}
