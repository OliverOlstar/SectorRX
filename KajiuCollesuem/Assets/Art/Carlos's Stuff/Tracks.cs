using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracks : MonoBehaviour
{
    private RenderTexture _splatmap;
    public Shader _drawShader;
    public GameObject _terrain;
    private Material _sandMaterial; 
    private Material _drawMaterial;
    public Transform[] _steps;
    RaycastHit _groundHit;
    int _layerMask;
    [Range(0, 2)]
    public float _brushSize;
    [Range(0, 1)]
    public float _brushStrenght;
    // Start is called before the first frame update
    void Start()
    {
        _layerMask = LayerMask.GetMask("Ground"); 
        _drawMaterial = new Material(_drawShader);
        _sandMaterial = _terrain.GetComponent<MeshRenderer>().material;
        _splatmap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        _sandMaterial.SetTexture("_Splat", _splatmap);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _steps.Length; i++)
        {
            if (Physics.Raycast(_steps[i].position, -Vector3.up, out _groundHit, 1f, _layerMask))
            {
                _drawMaterial.SetVector("_Coordinate", new Vector4(_groundHit.textureCoord.x, _groundHit.textureCoord.y, 0, 0));
                _drawMaterial.SetFloat("_Strenght", _brushStrenght);
                _drawMaterial.SetFloat("_Size", _brushSize);
                RenderTexture temp = RenderTexture.GetTemporary(_splatmap.width, _splatmap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(_splatmap, temp);
                Graphics.Blit(temp, _splatmap, _drawMaterial);
                RenderTexture.ReleaseTemporary(temp);


            }
        }
    }
}
