using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSetter : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer[] _meshRenderers = new SkinnedMeshRenderer[4];
    [SerializeField] private SkinnedMeshRenderer _feathersRenderer;

    public void SetColor(ColorSet pSet)
    {
        foreach(SkinnedMeshRenderer renderer in _meshRenderers)
        {
            renderer.material = pSet.lizzyMat;
        }

        if (_feathersRenderer != null)
            _feathersRenderer.material = pSet.feathersMat;
    }
}
