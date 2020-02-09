using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource mainAudio;
    public List<AudioClip> allMusic = new List<AudioClip>();

    [SerializeField] private bool _playMusic = false;

    private void Start()
    {
        if (_playMusic)
        {
            mainAudio.clip = allMusic[0];
            mainAudio.Play();
        }
    }
}
