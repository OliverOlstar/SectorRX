using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TommyGunShotAnimationAndSound : MonoBehaviour {

    public AudioSource Gunfire;
    void Start()
    {
        Gunfire = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Gunfire.Play();
            GetComponent<Animation>().Play("GunShot");
        }
    }
}
