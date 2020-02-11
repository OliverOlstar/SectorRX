using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadCameraTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.transform.parent.GetComponentInChildren<PlayerCamera>().targetDead = true;
    }
}
