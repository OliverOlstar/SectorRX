using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptHoverTwo : MonoBehaviour
{
    PowerUpgrade powerRnk3;

    // Start is called before the first frame update
    void Start()
    {
        powerRnk3 = GetComponent<PowerUpgrade>();
    }

    public void OnMouseEnter()
    {
        powerRnk3.rankThreeDescript.gameObject.SetActive(true);
    }

    public void OnMouseExit()
    {
        powerRnk3.rankThreeDescript.gameObject.SetActive(false);
    }
}
