using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public AudioClip[] lightAttack = new AudioClip[2]; 
    public AudioClip[] heavyAttack = new AudioClip[2];
    public AudioClip collectStat;
    public AudioSource sfxSource;

    public void LightAttackSound()
    {
        sfxSource.clip = lightAttack[Random.Range(0, 1)];
        sfxSource.Play();
    }

    public void HeavyAttackSound()
    {
        sfxSource.clip = lightAttack[Random.Range(0, 1)];
        sfxSource.pitch = 0.5f;
        sfxSource.Play();
    }

    public void StatUpSound()
    {
        sfxSource.clip = collectStat;
        sfxSource.pitch = Random.Range(1.5f, 2.5f);
        sfxSource.Play();
    }
}
