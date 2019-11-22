using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardGizmo : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        Vector3 lineEnd = transform.position + transform.forward;
        Debug.DrawLine(transform.position, lineEnd);
        Debug.DrawLine(lineEnd + transform.up / 6, lineEnd - transform.up / 6);
    }
}
