using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandRefill : MonoBehaviour

{
    public Shader _SandFillShader;
    private Material _SandFillMat;
    private MeshRenderer _meshRenderer;
    [Range(0.001f, 0.1f)]
    public float _flakeAmount;
    [Range(0f, 1f)]
    public float _flakeOppacity;


    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _SandFillMat = new Material(_SandFillShader);
            
    }

    // Update is called once per frame
    void Update()
    {
        _SandFillMat.SetFloat("_flakeAmount", _flakeAmount);
        _SandFillMat.SetFloat("_flakeOppacity", _flakeOppacity);
        RenderTexture sand = (RenderTexture)_meshRenderer.material.GetTexture("_Splat");
        RenderTexture temp = RenderTexture.GetTemporary(sand.width, sand.height, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(sand, temp, _SandFillMat);
        Graphics.Blit(temp, sand);
        _meshRenderer.material.SetTexture("_Splat", sand);
        RenderTexture.ReleaseTemporary(temp);

    }
}
