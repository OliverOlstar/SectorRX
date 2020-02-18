using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCameraTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerCamera otherCamera = other.transform.parent.GetComponentInChildren<PlayerCamera>();
        if (otherCamera)
            otherCamera.targetDead = true;
    }
}
