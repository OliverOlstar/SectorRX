using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class HellhoundSFX : MonoBehaviour
{
    public AudioClip[] deathSound = new AudioClip[2];
    public AudioClip biteAttack;
    public AudioSource sfxSource;

    public void AEDeathSound()
    {
        sfxSource.clip = deathSound[Random.Range(0, 1)];
        sfxSource.Play();
    }
}
