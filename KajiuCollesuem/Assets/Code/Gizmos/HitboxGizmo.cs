using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxGizmo : MonoBehaviour
{
    [SerializeField] private Color _color;
    [SerializeField] private float _radius = 0.5f;

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
