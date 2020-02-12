using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderGizmo : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private bool fill;
    [SerializeField] private bool selected;

    private void OnDrawGizmos()
    {
        if (selected) return;
        DrawCube();
    }

    private void OnDrawGizmosSelected()
    {
        if (!selected) return;
        DrawCube();
    }

    private void DrawCube()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();

        Gizmos.color = color;
        if (fill)
        {
            Gizmos.DrawCube(transform.position + boxCollider.center, new Vector3(boxCollider.size.x * transform.localScale.x, boxCollider.size.y * transform.localScale.y, boxCollider.size.z * transform.localScale.z));
        }
        else
        {
            Gizmos.DrawWireCube(transform.position + boxCollider.center, new Vector3(boxCollider.size.x * transform.localScale.x, boxCollider.size.y * transform.localScale.y, boxCollider.size.z * transform.localScale.z));
        }
    }
}
