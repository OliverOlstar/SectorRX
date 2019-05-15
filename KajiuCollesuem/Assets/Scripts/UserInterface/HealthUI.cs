using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : MonoBehaviour {

    private Transform bar;

    public void Start () {
        bar = transform.Find("Healthbar");
	}
	
	public void SetSize (float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }
}
