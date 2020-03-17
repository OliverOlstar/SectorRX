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
    [SerializeField] private Material _DefaultLizzyMaterial;
    [SerializeField] private Material _DefaultFeatherMaterial;
    [SerializeField] private Color _DefaultColor;

    [Space]
    [SerializeField] private Material[] _LizzyMaterials = new Material[10];
    [SerializeField] private Material[] _FeatherMaterials = new Material[10];
    [SerializeField] private Color[] _Colors = new Color[10];

    private ColorSet[] _ColorSets = new ColorSet[10];
    private ColorSet _DefaultColorSet = new ColorSet();

    private List<int> _OpenIndexs = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 } ;

    private void Awake()
    {
        _DefaultColorSet.lizzyMat = _DefaultLizzyMaterial;
        _DefaultColorSet.feathersMat = _DefaultFeatherMaterial;
        _DefaultColorSet.color = _DefaultColor;
        _DefaultColorSet.index = -1;

        for (int i = 0; i < 10; i++)
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

    public ColorSet ReturnColor(int pCurrentIndex)
    {
        _OpenIndexs.Add(pCurrentIndex);
        _OpenIndexs.Sort();
        return _DefaultColorSet;
    }

    public ColorSet GetDefaultColor()
    {
        return _DefaultColorSet;
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
                    int nextIndex = _OpenIndexs[i + 1];
                    _OpenIndexs.Remove(nextIndex);
                    return _ColorSets[nextIndex];
                }
                else
                {
                    int firstIndex = _OpenIndexs[0];
                    _OpenIndexs.Remove(firstIndex);
                    return _ColorSets[firstIndex];
                }
            }
        }

        // Fail safe
        return _ColorSets[0];
    }
}
