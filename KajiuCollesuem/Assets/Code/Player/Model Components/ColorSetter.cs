using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSetter : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer[] _meshRenderers = new SkinnedMeshRenderer[4];
    [SerializeField] private SkinnedMeshRenderer _feathersRenderer;

    [SerializeField] private GameObject[] _armour = new GameObject[2];

    public void SetColor(ColorSet pSet)
    {
        if (pSet.lizzyMat == null) 
            return;

        foreach(SkinnedMeshRenderer renderer in _meshRenderers)
        {
            renderer.material = pSet.lizzyMat;
        }

        if (_feathersRenderer != null)
            _feathersRenderer.material = pSet.feathersMat;
    }

    public void SetAbility(int pAbility)
    {
        switch (pAbility)
        {
            case 0:
                _feathersRenderer.enabled = false;
                _armour[0].SetActive(false);
                _armour[1].SetActive(false);
                break;

            case 1:
                _feathersRenderer.enabled = false;
                _armour[0].SetActive(true);
                _armour[1].SetActive(true);
                break;

            case 2:
                _feathersRenderer.enabled = true;
                _armour[0].SetActive(false);
                _armour[1].SetActive(false);
                break;
        }
    }
}
