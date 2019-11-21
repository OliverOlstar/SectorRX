using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptHover : MonoBehaviour
{
    PowerUpgrade powerRnk2;

    // Start is called before the first frame update
    void Start()
    {
        powerRnk2 = GetComponent<PowerUpgrade>();
    }

    public void OnMouseEnter()
    {
        powerRnk2.rankTwoDescript.gameObject.SetActive(true);
    }

    public void OnMouseExit()
    {
        powerRnk2.rankTwoDescript.gameObject.SetActive(false);
    }
}
