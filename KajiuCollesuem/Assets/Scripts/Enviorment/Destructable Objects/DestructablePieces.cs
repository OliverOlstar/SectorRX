using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructablePieces : MonoBehaviour
{
    public float surviveTime = 5f;
    public float fadeTime = 1f;

    void Start()
    {
        StartCoroutine(surviveRoutine());
    }

    private IEnumerator surviveRoutine()
    {
        //Time before starting fade out
        yield return new WaitForSeconds(surviveTime);
        StartCoroutine(fadeRoutine());
    }

    private IEnumerator fadeRoutine()
    {
        Renderer myRenderer = GetComponent<MeshRenderer>();
        Color color = myRenderer.material.color;
        float curAlpha = 1f;

        //Code to change material to render mode to Fade /////////////
        myRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        myRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        myRenderer.material.SetInt("_ZWrite", 0);
        myRenderer.material.DisableKeyword("_ALPHATEST_ON");
        myRenderer.material.EnableKeyword("_ALPHABLEND_ON");
        myRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        myRenderer.material.renderQueue = 3000;
        //////////////////////////////////////////////////////////////

        //Loop to change Alpha
        while (myRenderer.material.color.a > 0.01f)
        {
            curAlpha -= fadeTime * Time.deltaTime;
            myRenderer.material.color = new Color(color.r, color.g, color.b, curAlpha);
            yield return null;
        }

        //Decative the object
        gameObject.SetActive(false);
    }
}
