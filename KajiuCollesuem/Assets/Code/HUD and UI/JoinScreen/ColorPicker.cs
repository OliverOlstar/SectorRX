using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ColorSet
{
    public Material lizzyMat;
    public Material feathersMat;
    public Color color;
    public int index;
}

public class ColorPicker : MonoBehaviour
{
    [SerializeField] private Material[] _LizzyMaterials = new Material[8];
    [SerializeField] private Material[] _FeatherMaterials = new Material[8];
    [SerializeField] private Color[] _Colors = new Color[8];

    private ColorSet[] _ColorSets = new ColorSet[8];
    [SerializeField] private List<int> _OpenIndexs = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 } ;

    private void Awake()
    {
        for (int i = 0; i < 8; i++)
        {
            _ColorSets[i].lizzyMat = _LizzyMaterials[i];
            _ColorSets[i].feathersMat = _FeatherMaterials[i];
            _ColorSets[i].color = _Colors[i];
            _ColorSets[i].index = i;
        }
    }

    public ColorSet StartingColor()
    {
        int index = _OpenIndexs[0];
        _OpenIndexs.Remove(index);
        return _ColorSets[index];
    }

    public ColorSet SwitchColor(int pCurrentIndex)
    {
        _OpenIndexs.Add(pCurrentIndex);
        _OpenIndexs.Sort();

        // Find the index I just entered
        for (int i = 0; i < _OpenIndexs.Count; i++)
        {
            // If found return the next value in the list
            if (_OpenIndexs[i] == pCurrentIndex)
            {
                if (i + 1 < _OpenIndexs.Count)
                {
                    Debug.Log("Return next one");
                    int nextIndex = _OpenIndexs[i + 1];
                    _OpenIndexs.Remove(nextIndex);
                    return _ColorSets[nextIndex];
                }
                else
                {
                    Debug.Log("Return first one");
                    int firstIndex = _OpenIndexs[0];
                    _OpenIndexs.Remove(firstIndex);
                    return _ColorSets[firstIndex];
                }
            }
        }

        // Fail safe
        Debug.Log("Return fail safe");
        return _ColorSets[0];
    }
}
