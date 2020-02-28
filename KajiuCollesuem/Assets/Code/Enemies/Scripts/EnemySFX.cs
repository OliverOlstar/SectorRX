using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemySFX : MonoBehaviour
{
    public AudioSource enemySource;
    public AudioClip[] enemySFX = new AudioClip[4];

    public void AEAttackSound()
    {
        enemySource.clip = enemySFX[0];
        enemySource.volume = 0.6f;
        enemySource.Play();
    }

    public void AEShotSound()
    {
        enemySource.clip = enemySFX[1];
        enemySource.volume = 0.6f;
        enemySource.Play();
    }

    public void DeathSound()
    {
        enemySource.clip = enemySFX[Random.Range(2, 3)];
        enemySource.volume = 0.6f;
        enemySource.Play();
    }
}
