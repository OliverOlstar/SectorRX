using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneGizmo : MonoBehaviour
{
    [SerializeField] private Vector3 _Size;
    [SerializeField] private Color _Color;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = _Color;
        Gizmos.DrawCube(transform.position, _Size);
    }
}
