using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Programmer: Scott Watman 
 Additional Programmer(s): Rhoniel Von Possel
 Description: Combat Audio for Lizzy Scales(Godzilla character)*/

//What it requires.
[RequireComponent(typeof(AudioSource))]

public class Combat_Audio : MonoBehaviour
{
    //Access the Audio source.
    AudioSource MyAudioSource;

    //List of Audio clips for singular actions
    public AudioClip S_LightAttack;
    public AudioClip S_Dash;
    public AudioClip S_Jump;

    public AudioClip S_PlayerDMGReceived;
    public AudioClip S_LandingImpact;

    //List of Audio clips for charge based attacks
    public AudioClip S_BeamCharge;
    public AudioClip S_OnReleaseBeamCharge;

    void Start()
    {      
        //Access audio source.
        MyAudioSource = GetComponent<AudioSource>();   
    }

    void Update()
    {
        //On key press play the Audio sound attach.
        //Use this for Hold attack.

        //Beam charge.
        if (Input.GetKeyDown(KeyCode.K))
        {
            MyAudioSource.Stop();
            MyAudioSource.clip = S_BeamCharge;
            MyAudioSource.Play();
        }
            
        //Beam attack on release.
        if (Input.GetKeyUp(KeyCode.K))
        {
            MyAudioSource.Stop();
            MyAudioSource.PlayOneShot(S_OnReleaseBeamCharge, 1f);
        }

        //Light Attack.
        if (Input.GetKeyDown(KeyCode.L))
        {
            MyAudioSource.Stop();
            MyAudioSource.PlayOneShot(S_LightAttack, 1f);
        }

        //When player Dash.
        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.W))
        {
            MyAudioSource.Stop();
            MyAudioSource.PlayOneShot(S_Dash, 1f);
        }

        //When player Jump.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MyAudioSource.Stop();
            MyAudioSource.PlayOneShot(S_Jump, 1f);
        }
    }


    //Impact section
    private void OnTriggerEnter(Collider other)
    {
        //When player lands on ground.
        if (other.gameObject.tag.Contains("Land"))
        {
            MyAudioSource.Stop();
            MyAudioSource.PlayOneShot(S_LandingImpact, 1f);
        }

        //When player gets damaged.
        if (other.gameObject.tag.Contains("DMGSource"))
        {
            MyAudioSource.Stop();
            MyAudioSource.PlayOneShot(S_PlayerDMGReceived, 1f);
        }
    }
}
